using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SakuraStudios.FECipherCollection
{
    public class SceneManager : MonoBehaviour
    {

        #region Unity Callbacks

        // Start is called before the first frame update
        void Start()
        {
            LoadCard(CipherData.CardID.B01N056HN);
            LoadCard(CipherData.CardID.B01N003STp, new Vector3(3, 0, 0));
            //LoadCard(CipherData.CardID.B01N003ST, new Vector3(6, 0, 0));
        }

        // Update is called once per frame
        void Update()
        {

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
