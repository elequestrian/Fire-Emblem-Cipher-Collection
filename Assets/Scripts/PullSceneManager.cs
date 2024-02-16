using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SakuraStudios.FECipherCollection
{
    public class PullSceneManager : MonoBehaviour
    {
        [SerializeField] private Button packButton;
        [SerializeField] private Transform[] cardLocationArray = new Transform[10];
        [SerializeField] private CardObjectPool cardObjectPool;

        //List<CipherCardData> cardDataList = new List<CipherCardData>();
        //List<CipherData.CardID> lowerTierCards = new List<CipherData.CardID>();
        //List<CipherData.CardID> upperTierCards = new List<CipherData.CardID>();
        Queue<CipherData.CardID> currentSeries1Box = new Queue<CipherData.CardID>();
        bool pulledOnce = false;

        #region Unity Callbacks

        // Start is called before the first frame update
        void Start()
        {

            /*
            // Fill up the card data list
            foreach (CipherData.CardID cardID in CipherData.CardID.GetValues(typeof(CipherData.CardID)))
            {
                CipherCardData cardData = LoadCardData(cardID);

                if (cardData != null)
                {
                    //Debug.Log(cardData.ToString() + " Rarity: " + cardData.cardRarity.ToString());

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
            */

            currentSeries1Box = CreateSeries1Box();

            //print the list of cards to debug
            Debug.Log("List of cards in packs: ");
            foreach (CipherData.CardID cardID in currentSeries1Box)
            {
                Debug.Log(cardID.ToString());
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

            //Check that there are enough cards in the current box.
            if (currentSeries1Box.Count < 10)
            {
                if (currentSeries1Box.Count != 0)
                    Debug.LogError("currentSeries1Box has and unexpected number of cards.  currentSeries1Box.Count = " + currentSeries1Box.Count);
                currentSeries1Box = CreateSeries1Box();
            }

            //Pull 10 cards
            for (int i = 0; i < 10; i++)
            {
                cardObjectPool.GetCard(currentSeries1Box.Dequeue(), cardLocationArray[i]);
            }

            pulledOnce = true;

            //Turn the button back on
            packButton.interactable = true;


            /*
            //Randomize the pack
            List<CipherData.CardID> packCards = new List<CipherData.CardID>(9);

            for (int i = 0; i <= 9; i++)
            {
                //choose 5 lower tier cards, then 5 higher tier cards
                if (i <= 4)
                    packCards.Add(lowerTierCards[UnityEngine.Random.Range(0, lowerTierCards.Count)]);
                else
                    packCards.Add(upperTierCards[UnityEngine.Random.Range(0, upperTierCards.Count)]);
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

        private Queue<CipherData.CardID> CreateSeries1Box()
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
            List<CipherCardData> Series1RPlusCards = allSeries1Cards.FindAll(card => card.cardRarity == CipherData.CardRarity.Rp);
            List<CipherCardData> Series1SRPlusCards = allSeries1Cards.FindAll(card => card.cardRarity == CipherData.CardRarity.SRp);

            //Create a list (box) of 16 queues (packs)
            List<Queue<CipherCardData>> cipherPackBox = new List<Queue<CipherCardData>>(16);
            for (int i = 0; i < 16; i++)
            {
                Queue<CipherCardData> cipherCardPack = new Queue<CipherCardData>(10);
                cipherPackBox.Add(cipherCardPack);
            }

            //Create a duplicate of the N and HN card list to use as a tracker/checklist
            List<CipherCardData> NCardTracker = Series1NCards.ToList();
            List<CipherCardData> HNCardTracker = Series1HNCards.ToList();

            //calculate the number of cards to add to each pack from the tracker; also calculate the remainder of any cards left over.
            int NCardsRemainder;
            int NCardsPerPack = Math.DivRem(NCardTracker.Count, 16, out NCardsRemainder);
            int HNCardsRemainder;
            int HNCardsPerPack = Math.DivRem(HNCardTracker.Count, 16, out HNCardsRemainder);

            /*
            //Debug work to list all of the holo cards
            Debug.Log("Number of R cards: " + Series1RCards.Count + "; Number of SR cards: " + Series1SRCards.Count + "; Number of R+ cards: " + Series1RPlusCards.Count
                + "; Number of SR+ cards: " + Series1SRPlusCards.Count);
            Debug.Log("List of R Cards: ");
            foreach (CipherCardData card in Series1RCards)
            {
                Debug.Log(card.cardID.ToString());
            }
            Debug.Log("List of SR Cards: ");
            foreach (CipherCardData card in Series1SRCards)
            {
                Debug.Log(card.cardID.ToString());
            }
            Debug.Log("List of R+ Cards: ");
            foreach (CipherCardData card in Series1RPlusCards)
            {
                Debug.Log(card.cardID.ToString());
            }
            Debug.Log("List of SR+ Cards: ");
            foreach (CipherCardData card in Series1SRPlusCards)
            {
                Debug.Log(card.cardID.ToString());
            }
            */

            CipherCardData randomCard;

            //fill each pack with the correct number of N cards
            foreach (Queue<CipherCardData> pack in cipherPackBox)
            {
                //fill with a certain number of tracked cards
                for (int i = 0; i < NCardsPerPack; i++)
                {
                    randomCard = NCardTracker[UnityEngine.Random.Range(0, NCardTracker.Count)];
                    pack.Enqueue(randomCard);
                    NCardTracker.Remove(randomCard);
                }

                //Add 1 extra tracked card if needed.
                if (NCardsRemainder > 0)
                {
                    randomCard = NCardTracker[UnityEngine.Random.Range(0, NCardTracker.Count)];
                    pack.Enqueue(randomCard);
                    NCardTracker.Remove(randomCard);
                    NCardsRemainder--;
                }
               
                //Fill the rest of the pack with random N cards up to 7.
                while (pack.Count < 7)
                {
                    //choose a random N card from the entirety of Series 1
                    randomCard = Series1NCards[UnityEngine.Random.Range(0, Series1NCards.Count)];

                    //ensure that we choose a card that's not already in the pack
                    if (!pack.Contains(randomCard))
                    {
                        pack.Enqueue(randomCard);
                    }
                }              
                
            }

            //fill each pack with the correct number of HN cards
            foreach (Queue<CipherCardData> pack in cipherPackBox)
            {
                //fill with a certain number of tracked cards
                for (int i = 0; i < HNCardsPerPack; i++)
                {
                    randomCard = HNCardTracker[UnityEngine.Random.Range(0, HNCardTracker.Count)];
                    pack.Enqueue(randomCard);
                    HNCardTracker.Remove(randomCard);
                }

                //Add 1 extra tracked card if needed.
                if (HNCardsRemainder > 0)
                {
                    randomCard = HNCardTracker[UnityEngine.Random.Range(0, HNCardTracker.Count)];
                    pack.Enqueue(randomCard);
                    HNCardTracker.Remove(randomCard);
                    HNCardsRemainder--;
                }

                //Fill the rest of the pack with random HN cards up to 9.
                while (pack.Count < 9)
                {
                    //choose a random HN card from the entirety of Series 1
                    randomCard = Series1HNCards[UnityEngine.Random.Range(0, Series1HNCards.Count)];

                    //ensure that we choose a card that's not already in the pack
                    if (!pack.Contains(randomCard))
                    {
                        pack.Enqueue(randomCard);
                    }
                }
            }

            //Add holo card to each pack
            //track SR cards to add to the box
            int SRCardsToAdd = 4;

            //Track added cards to ensure what's added is unique
            List<CipherCardData> addedHoloCards = new List<CipherCardData>();

            //add an SR+ (25% chance) or an R+ to the first pack
            if (UnityEngine.Random.value < 0.25f)
            {
                randomCard = Series1SRPlusCards[UnityEngine.Random.Range(0, Series1SRPlusCards.Count)];               
                SRCardsToAdd--;
            }
            else
            {
                randomCard = Series1RPlusCards[UnityEngine.Random.Range(0, Series1RPlusCards.Count)];
            }
            cipherPackBox[0].Enqueue(randomCard);
            addedHoloCards.Add(randomCard);

            //add remaining SR cards and then fill the rest of the box with R cards
            for (int i = 1; i < cipherPackBox.Count; i++)
            {
                //loop on the pack until an unique card is chosen and added.
                while (cipherPackBox[i].Count < 10)
                {
                    if (SRCardsToAdd > 0)
                    {
                        randomCard = Series1SRCards[UnityEngine.Random.Range(0, Series1SRCards.Count)];
                        if (!addedHoloCards.Contains(randomCard))
                        {
                            cipherPackBox[i].Enqueue(randomCard);
                            addedHoloCards.Add(randomCard);
                            SRCardsToAdd--;
                        }
                    }
                    else
                    {
                        randomCard = Series1RCards[UnityEngine.Random.Range(0, Series1RCards.Count)];
                        if (!addedHoloCards.Contains(randomCard))
                        {
                            cipherPackBox[i].Enqueue(randomCard);
                            addedHoloCards.Add(randomCard);
                        }
                    }
                }
            }


            /*
            //print the list of added cards to check
            Debug.Log("List of cards in packs: ");
            int n = 1;
            foreach (Queue<CipherCardData> pack in cipherPackBox)
            {
                Debug.Log("Pack " + n + ": ");
                foreach (CipherCardData card in pack)
                {
                    Debug.Log(card.cardID.ToString());
                }
                n++;
            }

            //print the list of added holo cards to check
            Debug.Log("List of cards in packs: ");
            foreach (CipherCardData card in addedHoloCards)
            {
                Debug.Log(card.cardID.ToString());
            }
            */

            //return the list of all cards in the box randomizing the packs chosen.
            Queue<CipherData.CardID> finalBoxQueue = new Queue<CipherData.CardID>();
            int m = cipherPackBox.Count;
            Queue<CipherCardData> packToEmpty;
            int n;

            for (int i = 0; i < m; i++)
            {
                packToEmpty = cipherPackBox[UnityEngine.Random.Range(0, cipherPackBox.Count)];
                n = packToEmpty.Count;
                for (int j = 0; j < n; j++)
                {
                    finalBoxQueue.Enqueue(packToEmpty.Dequeue().cardID);
                }
                cipherPackBox.Remove(packToEmpty);
            }
            
            return finalBoxQueue;
        }

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
