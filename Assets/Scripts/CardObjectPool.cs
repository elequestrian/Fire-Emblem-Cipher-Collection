using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Com.SakuraStudios.FECipherCollection
{
    // An expansion of the simple object pool to add card object specific features    
    public class CardObjectPool : SimpleObjectPool
    {
        /// <summary>
        /// This method creates a new card in the scene as a child to a given parent and at a given location relative to the parent transform.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="parentTransform">The transform of the parent for the card object.</param>
        /// <param name="loadPosition">The position of the card object relative to its parent.</param>
        /// <param name="faceUp">An optional boolean parameteer to indicate if the object should be instantiated faceup or down; face up is default</param>
        /// <returns>Returns the BasicCard script component attached to the object unless the load fails, then returns null.</returns>
        public BasicCard GetCard(CipherData.CardID cardID, Transform parentTransform, Vector3 loadPosition, bool faceUp = true)
        {
            GameObject cardObject = GetObject();

            //Set the parent of the card object
            cardObject.transform.SetParent(parentTransform, false);

            //Set or reset the local position and rotation of the object
            cardObject.transform.SetLocalPositionAndRotation(loadPosition, Quaternion.Euler(-90, 0, 0));
            if (!faceUp)
            {
                cardObject.transform.Rotate(new Vector3(0, 180, 0));
            }

            //Set up the card including its correct face texture.
            BasicCard loadedCard = cardObject.GetComponent<BasicCard>();
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
        public BasicCard GetCard(CipherData.CardID cardID, Transform parentTransform, bool faceUp = true)
        {
            return GetCard(cardID, parentTransform, Vector3.zero, faceUp);
        }

        /// <summary>
        /// This method creates a new card in the scene as a child to a given parent and at a given location relative to the parent transform.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="loadPosition">The position of the card object in the scene.</param>
        /// <param name="faceUp">An optional boolean parameteer to indicate if the object should be instantiated faceup or down; face up is default</param>
        /// <returns>Returns the BasicCard script component attached to the object unless the load fails, then returns null.</returns>
        public BasicCard GetCard(CipherData.CardID cardID, Vector3 loadPosition, bool faceUp = true)
        {
            return GetCard(cardID, null, loadPosition, faceUp);
        }

        /// <summary>
        /// Overload method to create a new card in the scene at the origin.
        /// </summary>
        /// <param name="cardID">The ID of the card to be loaded; enum value must match the name of a prefab in the referenced folder.</param>
        /// <param name="faceUp">An optional boolean parameteer to indicate if the object should be instantiated faceup or down; face up is default</param>
        /// <returns>Returns the BasicCard script component attached to the object unless the load fails, then returns null.</returns>
        public BasicCard LoadCard(CipherData.CardID cardID, bool faceUp = true)
        {
            return GetCard(cardID, Vector3.zero, faceUp);
        }

        /// <summary>
        /// Returns a BasicCard object back to the pool.
        /// </summary>
        /// <param name="returnedCard">The BasicCard to be returned.</param>
        public void ReturnCard(BasicCard returnedCard)
        {
            //Remove all listeners from the events on the card.
            returnedCard.OnClickEvent.RemoveAllListeners();
            
            ReturnObject(returnedCard.gameObject);
        }

    }
}