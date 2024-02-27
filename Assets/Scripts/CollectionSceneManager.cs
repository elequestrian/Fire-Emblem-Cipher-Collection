using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SakuraStudios.FECipherCollection
{
    public class CollectionSceneManager : MonoBehaviour
    {
        [SerializeField] private Scrollbar cardListScrollbar;
        [SerializeField] private Transform cardGridTransform;
        [SerializeField] private CardObjectPool cardObjectPool;

        private List<CipherData.CardID> allCardIDs = new List<CipherData.CardID>();
        private List<CipherData.CardID> displayCardIDs = new List<CipherData.CardID>();

        // Position variables
        float initialX = -6.1f;
        float initialY = 2.7f;
        float incrementX = 2.5f;
        float incrementY = 3.4f;
        int maxColumns = 4;
        int rowCounter = 1;

        #region Unity Callbacks

        // Start is called before the first frame update
        void Start()
        {
            //Set up the Collection's card list
            allCardIDs.AddRange(CipherData.CardID.GetValues(typeof(CipherData.CardID)));
            
            
            foreach (CipherData.CardID cardID in allCardIDs)
            {
                CipherCardData cardData = Resources.Load<CipherCardData>("Card Data/" + cardID.ToString());
                
                if (cardData != null)
                {
                    //check that this ID doesn't have an alt art or that the alt art is not already present in the list.
                    if (cardData.altArtIDs.Length == 0 || (cardData.altArtIDs.Length > 0 && !displayCardIDs.Contains(cardData.altArtIDs[0])))
                    {
                        displayCardIDs.Add(cardID);
                    }
                }
            }
            
            PopulateCardList(displayCardIDs);
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Public Methods

        public void MoveCardGrid(float scrollbarValue)
        {
            Vector3 newPosition = cardGridTransform.localPosition;

            //calculate the max value for the Card Grid object.
            float maxGridY = 0.5f + ((rowCounter - 3) * incrementY);
            if (maxGridY < 0)
                maxGridY = 0;

            //Set the y position to a value between 0 and a max based on the number of rows.
            newPosition.y = scrollbarValue * maxGridY;

            cardGridTransform.localPosition = newPosition;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method creates and places the cards from a provided list in the collection screen viewer.
        /// </summary>
        /// <param name="cardIDs">The card list to add to the viewer.</param>
        private void PopulateCardList(List<CipherData.CardID> cardIDs)
        {
            rowCounter = 1;
            Vector3 newCardPosition = new Vector3(initialX, initialY, 0);
            int columnCounter = 1;

            // Create and position all cards 
            foreach (CipherData.CardID cardID in cardIDs)
            {
                //check if we need to move back to the first column for the next card
                if (columnCounter > maxColumns)
                {
                    newCardPosition.x = initialX;
                    newCardPosition.y -= incrementY;
                    columnCounter = 1;
                    rowCounter++;
                }

                BasicCard listedCard = cardObjectPool.GetCard(cardID, cardGridTransform, newCardPosition);
                listedCard.OnClickEvent.AddListener(CardInfoPanelController.Instance.DisplayCard);

                newCardPosition.x += incrementX;
                columnCounter++;
            }

            //Setup Card list's Scrollbar
            if (rowCounter <= 2)
                cardListScrollbar.size = 1;
            else if (rowCounter >= 3)
            {
                cardListScrollbar.size = 10.2f / (0.8f + rowCounter * incrementY);
            }
        }

        #endregion
    }
}
