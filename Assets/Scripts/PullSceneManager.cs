using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SakuraStudios.FECipherCollection
{
    public class PullSceneManager : MonoBehaviour
    {
        [SerializeField] private Button packButton;
        [SerializeField] private Transform[] cardLocationArray = new Transform[10];
        List<CipherCardData> cardDataList = new List<CipherCardData>();
        List<CipherData.CardID> lowerTierCards = new List<CipherData.CardID>();
        List<CipherData.CardID> upperTierCards = new List<CipherData.CardID>();
        bool pulledOnce = false;

        #region Unity Callbacks

        // Start is called before the first frame update
        void Start()
        {

            // Fill up the card data list
            foreach (CipherData.CardID cardID in CipherData.CardID.GetValues(typeof(CipherData.CardID)))
            {
                CipherCardData cardData = LoadCardData(cardID);
                
                Debug.Log(cardData.ToString() + " Rarity: " + cardData.cardRarity.ToString());
                
                //is this necessary?
                cardDataList.Add(cardData);

                //Divide up the cards into categories
                if (cardData.cardRarity == CipherData.CardRarity.N || cardData.cardRarity == CipherData.CardRarity.HN 
                    || cardData.cardRarity == CipherData.CardRarity.PR || cardData.cardRarity == CipherData.CardRarity.ST)
                {
                    lowerTierCards.Add(cardID);
                }
                // cardRarity = R, Rp, SR, SRp, pX, HR, PRp, STp
                else
                    upperTierCards.Add(cardID);
            }

            

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Public Methods

        public void PullPack()
        {
            if (pulledOnce)
            {
                RemoveCards();
            }
            
            //Turn off the button
            packButton.interactable = false;

            //Randomize the pack
            List<CipherData.CardID> packCards = new List<CipherData.CardID>(9);

            for (int i = 0; i <= 9; i++)
            {
                //choose 5 lower tier cards, then 5 higher tier cards
                if (i <= 4)
                    packCards.Add(lowerTierCards[UnityEngine.Random.Range(0, lowerTierCards.Count - 1)]);
                else
                    packCards.Add(upperTierCards[UnityEngine.Random.Range(0, upperTierCards.Count - 1)]);
            }

            //Load cards
            for (int i = 0; i < packCards.Count; i++)
            {
                if (i < cardLocationArray.Length)
                    LoadCard(packCards[i], cardLocationArray[i]);
                else
                    Debug.LogError("More cards in packCards than locations in cardLocationArray.  Check sizes."); 
            }

            /*
            LoadCard(packCards[0], new Vector3(-7, -2, 0));
            LoadCard(packCards[1], new Vector3(-3.5f, -2, 0));
            LoadCard(packCards[2], new Vector3(0, -2, 0));
            LoadCard(packCards[3], new Vector3(3.5f, -2, 0));
            LoadCard(packCards[4], new Vector3(7, -2, 0));
            LoadCard(packCards[5], new Vector3(-7, 2, 0));
            LoadCard(packCards[6], new Vector3(-3.5f, 2, 0));
            LoadCard(packCards[7], new Vector3(0, 2, 0));
            LoadCard(packCards[8], new Vector3(3.5f, 2, 0));
            LoadCard(packCards[9], new Vector3(7, 2, 0));
            */
            
            pulledOnce = true;

            //Turn the button back on
            packButton.interactable = true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method creates a new card in the scene at a given location by loading it from the Resource folder.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="loadPosition">The position where the card object should be instantiated.</param>
        private BasicCard LoadCard(CipherData.CardID cardID, Vector3 loadPosition)
        {
            
            //Quaternion is set up for a default 2D project. 
            //Debug.Log("Trying to load " + cardNumber + " from Resources.");
            GameObject loadedObject = Instantiate(Resources.Load("Sample Card", typeof(GameObject)), loadPosition, Quaternion.Euler(-90, 0, 0)) as GameObject;
            
            //Debug.Log(loadedObject + " has been successfully loaded.");

            //Check if the load was successful.  Errors might be thrown earlier.
            if (loadedObject == null)
            {
                Debug.LogError(cardID.ToString() + " was not loaded by LoadCard().  Check the Resources folder for the prefab.");
                return null;
            }

            //Set up the card including its correct face texture.
            BasicCard loadedCard = loadedObject.GetComponent<BasicCard>();
            loadedCard.SetUp(cardID);
            return loadedCard;
        }

        /// <summary>
        /// This method creates a new card in the scene at the origin by loading it from the Resource folder.
        /// </summary>
        /// <param name="cardID">The number of the card to be loaded; must match the name of a prefab in the referenced folder.</param>
        private BasicCard LoadCard(CipherData.CardID cardID)
        {
            return LoadCard(cardID, Vector3.zero);
        }

        /// <summary>
        /// This method creates a new card in the scene as a child to a given parent.  The card will be positioned relative to the parent transform not world space.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="parentTransform">The transform of the parent for the card object.</param>
        private BasicCard LoadCard(CipherData.CardID cardID, Transform parentTransform)
        {
            //Debug.Log("Trying to load " + cardNumber + " from Resources.");
            GameObject loadedObject = Instantiate(Resources.Load("Sample Card", typeof(GameObject)), parentTransform, false) as GameObject;

            //Debug.Log(loadedObject + " has been successfully loaded.");

            //Check if the load was successful.  Errors might be thrown earlier.
            if (loadedObject == null)
            {
                Debug.LogError(cardID.ToString() + " was not loaded by LoadCard().  Check the Resources folder for the prefab.");
                return null;
            }

            //Set up the card including its correct face texture.
            BasicCard loadedCard = loadedObject.GetComponent<BasicCard>();
            loadedCard.SetUp(cardID);
            return loadedCard;
        }

        /// <summary>
        /// This method removes the card objects from the scene to enable another pack pull.
        /// </summary>
        private void RemoveCards()
        {
            foreach (Transform cardLocation in cardLocationArray)
            {
                BasicCard cardScript = cardLocation.GetComponentInChildren<BasicCard>();
                if (cardScript != null)
                {
                    Destroy(cardScript.gameObject);
                }
                else
                {
                    Debug.LogWarning("No BasicCard found under " + cardLocation.ToString());
                }
            }
        }


        private CipherCardData LoadCardData(CipherData.CardID cardID)
        {
             return Resources.Load<CipherCardData>("Card Data/" + cardID.ToString());
        }

        /*
                    //Debug.Log("Trying to load " + kvp.Key + " from Resources.");
                    GameObject loadedObject = Instantiate(Resources.Load(kvp.Key)) as GameObject;
                    //Debug.Log(loadedObject + " has been successfully loaded.");

                    BasicCard cardToAdd = loadedObject.GetComponent<BasicCard>();
                    deck.Add(cardToAdd);
                    //Debug.Log(cardToAdd.ToString() + " has been added to the deck.");
         */

        #endregion
    }
}
