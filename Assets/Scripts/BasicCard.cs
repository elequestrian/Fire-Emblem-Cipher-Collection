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
        /// <param name="cardID">The number of the card to be loaded; used to set the face image for this card.</param>
        public void SetUp(string cardID)
        {
            ChangeCardFace(cardID);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method changes the texture used as the card's face by loading a new one from the Resource folder and assigning it to a node in the shader graph.
        /// </summary>
        /// <param name="cardID">The number of the card face to be loaded; must match the name of a texture in the Resources folder.</param>
        private void ChangeCardFace(string cardID)
        {
            Material cardFrontMaterial = GetComponent<MeshRenderer>().materials[2];
            cardFrontMaterial.SetTexture("_CardFront", Resources.Load("Card Faces/" + cardID, typeof(Texture)) as Texture);
        }

        #endregion
    }
}
