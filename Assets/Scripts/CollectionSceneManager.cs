using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SakuraStudios.FECipherCollection
{
    public class CollectionSceneManager : MonoBehaviour
    {
        [SerializeField] private Transform cardGridTransform;

        // Position variables
        float initialX = -6.1f;
        float initialY = 3.1f;
        float incrementX = 2.5f;
        float incrementY = -3.4f;
        int maxColumns = 4;


        #region Unity Callbacks

        // Start is called before the first frame update
        void Start()
        {
            Vector3 newCardPosition = new Vector3(initialX, initialY, 0);
            int columnCounter = 1;

            // Create and position all cards 
            foreach (CipherData.CardID cardID in CipherData.CardID.GetValues(typeof(CipherData.CardID)))
            {
                LoadCard(cardID, cardGridTransform, newCardPosition);

                newCardPosition.x += incrementX;
                columnCounter++;

                //check if we need to move back to the first column
                if (columnCounter > maxColumns)
                {
                    newCardPosition.x = initialX;
                    newCardPosition.y += incrementY;
                    columnCounter = 1;
                }
                
            }
        }

        // Update is called once per frame
        void Update()
        {

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
