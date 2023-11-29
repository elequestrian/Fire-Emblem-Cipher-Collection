using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Com.SakuraStudios.FECipherCollection
{
    public class BasicCard : MonoBehaviour, IPointerClickHandler
    {
        private CipherCardData cardData;
        public MyBasicCardEvent OnClickEvent = new MyBasicCardEvent();

        //These card stats are held locally in case they get changed by other cards.
        protected int localDeploymentCost;
        protected bool localCanPromote;
        protected int localPromotionCost;
        protected bool[] localCardColorArray;
        protected bool[] localCharGenderArray;
        protected bool[] localCharWeaponArray;
        protected bool[] localUnitTypeArray;
        protected int localBaseAttack;
        protected int localBaseSupport;
        protected bool[] localBaseRangeArray;

        // A List that holds information about the skills impacting a card.
        protected List<string> skillChangeTracker = new List<string>();

        // Used to modify a card's attack power outside of battle (with the effects of skills, etc.)
        // Made public since card skills can adjust attack power.
        public int attackModifier = 0;
        //public int battleModifier = 0;                      //Used to update a card's attack power in battle (with supports, etc.)  Is reset after battle?
        public int supportModifier = 0;                     //NOTE: Not fully integrated.  Needs to have a way to reset similar to attackModifier.
                                                            //Used to modify a card's support power with the effects of skills, etc. Reset once the card leaves the field.

        #region Public Properties

        public CipherCardData GetCardData { get { return cardData; } }

        public CipherData.CardID CardID { get { return cardData.cardID; } }

        public virtual string CardNumber { get { return cardData.cardNumber; } }
        public virtual string CharTitle { get { return cardData.charTitle; } }
        public virtual string CharQuote { get { return cardData.charQuote; } }
        public virtual string CardIllustrator { get { return cardData.cardIllustrator; } }
        public virtual string[] CardSkills { get { return cardData.cardSkills; } }
        public virtual string CharName { get { return cardData.charName; } }
        public virtual string ClassTitle { get { return cardData.classTitle; } }
        public virtual bool[] SkillTypes { get { return cardData.skillTypes; } }


        public virtual int DeploymentCost { get { return localDeploymentCost; } }
        public virtual bool Promotable { get { return localCanPromote; } }
        public virtual int PromotionCost { get { return localPromotionCost; } }
        public virtual bool[] CardColorArray { get { return localCardColorArray; } }
        public virtual bool[] CharGenderArray { get { return localCharGenderArray; } }
        public virtual bool[] CharWeaponArray { get { return localCharWeaponArray; } }
        public virtual bool[] UnitTypeArray { get { return localUnitTypeArray; } }
        public virtual int BaseAttack { get { return localBaseAttack; } }
        public virtual int BaseSupport { get { return localBaseSupport; } }
        public virtual bool[] BaseRangeArray { get { return localBaseRangeArray; } }

        public virtual int CurrentAttackValue { get { return BaseAttack + attackModifier; } }       //Returns the current attack stat including skill modifiers.
        public virtual int CurrentSupportValue { get { return BaseSupport + supportModifier; } }    //Returns the current support stat including skill modifiers.

        // Property returns a List of the string names of the colors on this card.
        public virtual List<string> CardColorList
        {
            get
            {
                string[] cipherColorArray = Enum.GetNames(typeof(CipherData.ColorsEnum));
                List<string> colorNames = new List<string>(cipherColorArray.Length);

                //Loops through each possible color
                for (int i = 0; i < CardColorArray.Length; i++)
                {
                    //if the color is on the card then add the color name to the list
                    if (CardColorArray[i])
                    {
                        colorNames.Add(cipherColorArray[i]);
                    }
                }

                return colorNames;
            }
        }

        /// <summary>
        /// This property returns all current information about skills affecting the card.
        /// </summary> 
        public List<string> SkillChangeTracker { get { return skillChangeTracker; } }
        #endregion

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

        #region IPointerHandler Interface Implementation

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent.Invoke(this);
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
            cardData = Resources.Load("Card Data/" + cardID.ToString(), typeof(CipherCardData)) as CipherCardData;
            
            //Double check the load worked and leave this reference null if it didn't.
            if (cardData == null)
            {
                Debug.LogError("BasicCard.SetUp() failed to load CipherCardData for " + cardID.ToString() + ". Check the Resources folder.");
                return;
            }
            else if (cardData.cardID != cardID)
            {
                cardData = null;
                Debug.LogError("BasicCard.SetUp() failed to load CipherCardData for " + cardID.ToString() + ". Check the Resources folder.  Previous cardData reference set to null.");
                return;
            }

            // Set local values based on standard card data.
            // We need to clone the arrays to make sure we aren't just copying a reference, but creating a new distinct array we can mess with.
            localDeploymentCost = cardData.deploymentCost;
            localPromotionCost = cardData.promotionCost;
            localCardColorArray = (bool[])cardData.cardColor.Clone();
            localCharGenderArray = (bool[])cardData.charGender.Clone();
            localCharWeaponArray = (bool[])cardData.charWeaponType.Clone();
            localUnitTypeArray = (bool[])cardData.unitTypes.Clone();
            localBaseAttack = cardData.baseAttack;
            localBaseSupport = cardData.baseSupport;
            localBaseRangeArray = (bool[])cardData.baseRange.Clone();
            localCanPromote = cardData.canPromote;
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

            //Double-check that the texture loaded.  If not, the texture is replaced by a standard white texture.
            if (cardFrontTexture == null)
                Debug.LogError("BasicCard ChangeCardFace(): Front card texture did not load for " + cardID.ToString());
            cardFrontMaterial.SetTexture("_CardFront", cardFrontTexture);
        }

        #endregion
    }

    /// <summary>
    /// Creates a UnityEvent class that takes a BasicCard as input.
    /// </summary>
    [System.Serializable]
    public class MyBasicCardEvent : UnityEvent<BasicCard>
    {
    }
}
