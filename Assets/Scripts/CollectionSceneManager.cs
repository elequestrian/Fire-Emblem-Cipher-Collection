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

        // Position variables
        float initialX = -6.1f;
        float initialY = 3.1f;
        float incrementX = 2.5f;
        float incrementY = 3.4f;
        int maxColumns = 4;
        int rowCounter = 1;

        #region Unity Callbacks

        // Start is called before the first frame update
        void Start()
        {
            
            //Populate card list method
            Vector3 newCardPosition = new Vector3(initialX, initialY, 0);
            int columnCounter = 1;
            

            // Create and position all cards 
            foreach (CipherData.CardID cardID in CipherData.CardID.GetValues(typeof(CipherData.CardID)))
            {
                //check if we need to move back to the first column for the next card
                if (columnCounter > maxColumns)
                {
                    newCardPosition.x = initialX;
                    newCardPosition.y -= incrementY;
                    columnCounter = 1;
                    rowCounter++;
                }

                LoadCard(cardID, cardGridTransform, newCardPosition);

                newCardPosition.x += incrementX;
                columnCounter++;

            }

            /*
            //Setup Scrollbar method
            if (rowCounter <= 2)
                cardListScrollbar.size = 1;
            else if (rowCounter == 3)
                cardListScrollbar.size = 0.95f;
            else if (rowCounter >= 4)
                cardListScrollbar.size = 0.9f;
            */
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
        /// This method creates a new card in the scene as a child to a given parent and at a given location relative to the parent transform.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="parentTransform">The transform of the parent for the card object.</param>
        /// <param name="loadPosition">The position of the card object relative to its parent.</param>
        private BasicCard LoadCard(CipherData.CardID cardID, Transform parentTransform, Vector3 loadPosition)
        {
            //Debug.Log("Trying to load " + cardNumber + " from Resources.");
            GameObject loadedObject = Instantiate(Resources.Load("Sample Card", typeof(GameObject)), parentTransform) as GameObject;

            //Debug.Log(loadedObject + " has been successfully loaded.");

            //Check if the load was successful.  Errors might be thrown earlier.
            if (loadedObject == null)
            {
                Debug.LogError(cardID.ToString() + " was not loaded by LoadCard().  Check the Resources folder for the prefab.");
                return null;
            }

            //Set the local position of the object
            loadedObject.transform.localPosition = loadPosition;
            loadedObject.transform.localScale = new Vector3(45, 45, 45);

            //Set up the card including its correct face texture.
            BasicCard loadedCard = loadedObject.GetComponent<BasicCard>();
            loadedCard.SetUp(cardID);
            return loadedCard;
        }

        #endregion
    }
}
