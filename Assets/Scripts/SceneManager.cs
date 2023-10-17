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
            LoadCard("Sample Card");
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
        /// <param name="cardNumber">The number of the card to be loaded; must match the name of a prefab in the referenced folder.</param>
        /// <param name="loadPosition">The position where the card object should be instantiated.</param>
        private void LoadCard(string cardNumber, Vector3 loadPosition)
        {
            //Debug.Log("Trying to load " + cardNumber + " from Resources.");
            GameObject loadedObject = Instantiate(Resources.Load(cardNumber, typeof(GameObject)), loadPosition, Quaternion.identity) as GameObject;
            //Debug.Log(loadedObject + " has been successfully loaded.");

            //Check if the load was successful.  Errors might be thrown earlier.
            if (loadedObject == null)
            {
                Debug.LogError(cardNumber + " was not loaded by LoadCard().  Check the Resources folder for the prefab.");
                return;
            }

            //BasicCard cardToAdd = loadedObject.GetComponent<BasicCard>();
            //deck.Add(cardToAdd);
            //Debug.Log(cardToAdd.ToString() + " has been added to the deck.");
        }

        /// <summary>
        /// This method creates a new card in the scene at the origin by loading it from the Resource folder.
        /// </summary>
        /// <param name="cardNumber">The number of the card to be loaded; must match the name of a prefab in the referenced folder.</param>
        private void LoadCard(string cardNumber)
        {
            LoadCard(cardNumber, Vector3.zero);
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
