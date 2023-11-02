using System.Collections;
using System.Collections.Generic;
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
