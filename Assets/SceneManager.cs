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
        /// This method creates a new card in the scene by loading it from the Resource folder.
        /// </summary>
        /// <param name="cardNumber">The number of the card to be loaded; must match the name of a prefab in the referenced folder.</param>
        private void LoadCard(string cardNumber)
        {
            //Debug.Log("Trying to load " + cardNumber + " from Resources.");
            GameObject loadedObject = Instantiate(Resources.Load(cardNumber, typeof(GameObject))) as GameObject;
            //Debug.Log(loadedObject + " has been successfully loaded.");


            //TODO: add 


            //BasicCard cardToAdd = loadedObject.GetComponent<BasicCard>();
            //deck.Add(cardToAdd);
            //Debug.Log(cardToAdd.ToString() + " has been added to the deck.");
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
