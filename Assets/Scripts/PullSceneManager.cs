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
        [SerializeField] private CardObjectPool cardObjectPool;

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

                if (cardData != null)
                {
                    Debug.Log(cardData.ToString() + " Rarity: " + cardData.cardRarity.ToString());

                    //this is a list of actual cards that exist
                    cardDataList.Add(cardData);

                    //Divide up the cards into categories
                    
                    switch (cardData.cardRarity)
                    {
                        case CipherData.CardRarity.N:
                        case CipherData.CardRarity.HN:
                        case CipherData.CardRarity.PR:
                        case CipherData.CardRarity.ST:
                            lowerTierCards.Add(cardID);
                            break;

                        case CipherData.CardRarity.R:
                        case CipherData.CardRarity.Rp:
                        case CipherData.CardRarity.SR:
                        case CipherData.CardRarity.SRp:
                        case CipherData.CardRarity.pX:
                        case CipherData.CardRarity.HR:
                        case CipherData.CardRarity.PRp:
                        case CipherData.CardRarity.STp:
                            upperTierCards.Add(cardID);
                            break;

                        default: // else cardRarity = M and we shouldn't include in the pack pulls    
                            break;
                    }              
                }
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
                    cardObjectPool.GetCard(packCards[i], cardLocationArray[i]);
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
        /// This method removes the card objects from the scene to enable another pack pull.
        /// </summary>
        private void RemoveCards()
        {
            foreach (Transform cardLocation in cardLocationArray)
            {
                BasicCard cardScript = cardLocation.GetComponentInChildren<BasicCard>();
                if (cardScript != null)
                {
                    cardObjectPool.ReturnCard(cardScript);
                }
                else
                {
                    Debug.LogWarning("No BasicCard found under " + cardLocation.ToString());
                }
            }
        }

        /*
        private List<CipherData.CardID> CreateSeries1Box()
        {
            // Create a list of all Series 1 cards
            List<CipherCardData> allSeries1Cards = new List<CipherCardData>();
            foreach (CipherData.CardID cardID in CipherData.CardID.GetValues(typeof(CipherData.CardID)))
            {
                CipherCardData cardData = LoadCardData(cardID);

                if (cardData != null && cardData.cardNumber.StartsWith("B01"))
                {
                    allSeries1Cards.Add(cardData);             
                }
            }

            //Create a list of Series 1 cards in each rarity category
            List<CipherCardData> Series1NCards = allSeries1Cards.FindAll(card => card.cardRarity == CipherData.CardRarity.N);
            List<CipherCardData> Series1HNCards = allSeries1Cards.FindAll(card => card.cardRarity == CipherData.CardRarity.HN);
            List<CipherCardData> Series1RCards = allSeries1Cards.FindAll(card => card.cardRarity == CipherData.CardRarity.R);
            List<CipherCardData> Series1SRCards = allSeries1Cards.FindAll(card => card.cardRarity == CipherData.CardRarity.SR);
            List<CipherCardData> Series1PlusCards = allSeries1Cards.FindAll(card => card.cardRarity == CipherData.CardRarity.Rp || card.cardRarity == CipherData.CardRarity.SRp);

            //Create a list (box) of 16 queues (packs)
            List<Queue<CipherCardData>> cipherPackBox = new List<Queue<CipherCardData>>(16);
            for (int i = 0; i < 16; i++)
            {
                Queue<CipherCardData> cipherCardPack = new Queue<CipherCardData>(10);
                cipherPackBox.Add(cipherCardPack);
            }

            //Create a duplicate of the N card list to use as a tracker/checklist
            List<CipherCardData> NCardTracker = Series1NCards.ToList();

            //calculate the number of cards to add to each pack from the tracker
            //also calculate the remainder of any cards left over.  Then iterate through that 1 per pack as needed. (should be 0 for series 1, but don't assume.

            foreach (Queue<CipherCardData> pack in cipherPackBox)
            {

            }

            //TODO Remove
            //return CipherData.CardID.GetValues(typeof(CipherData.CardID));

            //start to fill up individual "packs" and put them into a "box"
        }
        */

        //private List<CipherCardData> CreateCardDataList(List<CipherData.CardID>)

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
