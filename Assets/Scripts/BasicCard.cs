using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Com.SakuraStudios.FECipherCollection
{
    public class BasicCard : MonoBehaviour
    {
        #region Unity Callbacks

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method creates a new card in the scene as a child to a given parent and at a given location relative to the parent transform.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="parentTransform">The transform of the parent for the card object.</param>
        /// <param name="loadPosition">The position of the card object relative to its parent.</param>
        /// <param name="faceUp">An optional boolean parameteer to indicate if the object should be instantiated faceup or down; face up is default</param>
        /// <returns>Returns the BasicCard script component attached to the object unless the load fails, then returns null.</returns>
        public static BasicCard LoadCard(CipherData.CardID cardID, Transform parentTransform, Vector3 loadPosition, bool faceUp = true)
        {
            //Debug.Log("Trying to load " + cardNumber + " from Resources.");
            GameObject loadedObject = Instantiate(Resources.Load("Sample Card", typeof(GameObject)), parentTransform) as GameObject;

            //Debug.Log(loadedObject + " has been successfully loaded.");

            //Check if the load was successful.  Errors might be thrown earlier.
            if (loadedObject == null)
            {
                Debug.LogError(cardID.ToString() + " was not loaded by BasicCard.LoadCard().  Check the Resources folder for the prefab.");
                return null;
            }

            //Set the local position and rotation of the object
            loadedObject.transform.localPosition = loadPosition;
            if (!faceUp)
            {
                loadedObject.transform.Rotate(new Vector3(0, 180, 0));
            }

            //Set up the card including its correct face texture.
            BasicCard loadedCard = loadedObject.GetComponent<BasicCard>();
            loadedCard.SetUp(cardID);
            return loadedCard;
        }

        /// <summary>
        /// Overload method to create a new card in the scene as a child to a given parent.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="parentTransform">The transform of the parent for the card object.</param>
        /// <param name="faceUp">An optional boolean parameteer to indicate if the object should be instantiated faceup or down; face up is default</param>
        /// <returns>Returns the BasicCard script component attached to the object unless the load fails, then returns null.</returns>
        public static BasicCard LoadCard(CipherData.CardID cardID, Transform parentTransform, bool faceUp = true)
        {
            return LoadCard(cardID, parentTransform, Vector3.zero, faceUp);
        }

        /// <summary>
        /// This method creates a new card in the scene as a child to a given parent and at a given location relative to the parent transform.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="loadPosition">The position of the card object in the scene.</param>
        /// <param name="faceUp">An optional boolean parameteer to indicate if the object should be instantiated faceup or down; face up is default</param>
        /// <returns>Returns the BasicCard script component attached to the object unless the load fails, then returns null.</returns>
        public static BasicCard LoadCard(CipherData.CardID cardID, Vector3 loadPosition, bool faceUp = true)
        {
            //Debug.Log("Trying to load " + cardNumber + " from Resources.");
            GameObject loadedObject = Instantiate(Resources.Load("Sample Card", typeof(GameObject)), loadPosition, Quaternion.Euler(-90, 0, 0)) as GameObject;

            //Debug.Log(loadedObject + " has been successfully loaded.");

            //Check if the load was successful.  Errors might be thrown earlier.
            if (loadedObject == null)
            {
                Debug.LogError(cardID.ToString() + " was not loaded by BasicCard.LoadCard().  Check the Resources folder for the prefab.");
                return null;
            }

            //Check the rotation of the object
            if (!faceUp)
            {
                loadedObject.transform.Rotate(new Vector3(0, 180, 0));
            }

            //Set up the card including its correct face texture.
            BasicCard loadedCard = loadedObject.GetComponent<BasicCard>();
            loadedCard.SetUp(cardID);
            return loadedCard;
        }

        /// <summary>
        /// Overload method to create a new card in the scene at the origin.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="faceUp">An optional boolean parameteer to indicate if the object should be instantiated faceup or down; face up is default</param>
        /// <returns>Returns the BasicCard script component attached to the object unless the load fails, then returns null.</returns>
        public static BasicCard LoadCard(CipherData.CardID cardID, bool faceUp = true)
        {
            return LoadCard(cardID, Vector3.zero, faceUp);
        }

        /// <summary>
        /// This method sets up the card to ensure all functionality is working as expected.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; used to set the face image for this card.</param>
        public void SetUp(CipherData.CardID cardID)
        {
            ChangeCardFace(cardID);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method changes the texture used as the card's face by loading a new one from the Resource folder and assigning it to a node in the shader graph.
        /// </summary>
        /// <param name="cardID">The ID of the card face to be loaded; enum value must match the name of a texture in the Resources folder.</param>
        private void ChangeCardFace(CipherData.CardID cardID)
        {
            Material cardFrontMaterial = GetComponent<MeshRenderer>().materials[2];
            
            //Load texture based on cardID to use as the front.
            Texture cardFrontTexture = Resources.Load("Card Faces/" + cardID.ToString(), typeof(Texture)) as Texture;

            //Double-check that the texture loaded, and if so set it as the new face card.  Without the if, the texture is replaced by a standard white texture.
            if (cardFrontTexture != null)
                cardFrontMaterial.SetTexture("_CardFront", cardFrontTexture);
            else
                Debug.LogError("BasicCard ChangeCardFace(): Front card texture did not load for " + cardID.ToString());
        }

        #endregion
    }
}
