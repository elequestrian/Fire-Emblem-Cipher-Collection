using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SakuraStudios.FECipherCollection
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private Button packButton;


        #region Unity Callbacks

        // Start is called before the first frame update
        void Start()
        {
            
            List<CipherCardData> cardDataList = new List<CipherCardData>();

            foreach (CipherData.CardID cardID in CipherData.CardID.GetValues(typeof(CipherData.CardID)))
            {
                CipherCardData cardData = LoadCardData(cardID);
                
                Debug.Log(cardData.ToString() + " Rarity: " + cardData.cardRarity.ToString());
                //cardDataList.Add(cardData);
            }
            
            
            
            
            //CipherCardData[] cardDataArray = Resources.FindObjectsOfTypeAll<CipherCardData>();

            //Debug.Log(cardDataArray.Length);

            /*
            foreach (CipherCardData cardData in Resources.FindObjectsOfTypeAll<CipherCardData>())
            {
                Debug.Log(cardData.ToString());
            }
            */
            //PullPack();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Public Methods

        public void PullPack()
        {
            //Turn off the button
            packButton.interactable = false;


            //Load cards
            LoadCard(CipherData.CardID.B01N001p, new Vector3(-7, -2, 0));
            LoadCard(CipherData.CardID.B01N001, new Vector3(-3.5f, -2, 0));
            LoadCard(CipherData.CardID.B01N002, new Vector3(0, -2, 0));
            LoadCard(CipherData.CardID.B01N056, new Vector3(3.5f, -2, 0));
            LoadCard(CipherData.CardID.S01N001, new Vector3(7, -2, 0));
            LoadCard(CipherData.CardID.P01N003, new Vector3(-7, 2, 0));
            LoadCard(CipherData.CardID.B01N003p, new Vector3(-3.5f, 2, 0));
            LoadCard(CipherData.CardID.S01N001p, new Vector3(0, 2, 0));
            LoadCard(CipherData.CardID.P01N012, new Vector3(3.5f, 2, 0));
            LoadCard(CipherData.CardID.P01N013, new Vector3(7, 2, 0));
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


            //BasicCard cardToAdd = loadedObject.GetComponent<BasicCard>();
            //deck.Add(cardToAdd);
            //Debug.Log(cardToAdd.ToString() + " has been added to the deck.");
        }

        /// <summary>
        /// This method creates a new card in the scene at the origin by loading it from the Resource folder.
        /// </summary>
        /// <param name="cardID">The number of the card to be loaded; must match the name of a prefab in the referenced folder.</param>
        private BasicCard LoadCard(CipherData.CardID cardID)
        {
            return LoadCard(cardID, Vector3.zero);
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
