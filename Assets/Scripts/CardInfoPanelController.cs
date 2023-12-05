using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SakuraStudios.FECipherCollection
{
    public class CardInfoPanelController : MonoBehaviour
    {
        public static CardInfoPanelController Instance = null;                      // The instance reference to this script as it is a singleton pattern.

        [SerializeField] private BasicCard displayCard;             //The main card image of the panel.
        [SerializeField] private TextMeshProUGUI displayText;       //The display text of the InfoPanel.
        [SerializeField] private Scrollbar cardTextScrollbar;
        [SerializeField] private Button altArtButton;

        // Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (Instance == null)

                //if not, set instance to this
                Instance = this;

            //If instance already exists and it's not this:
            else if (Instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of the InfoPanelController.
                Destroy(gameObject);
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Public Methods
        // Method called by a BasicCard when clicked to display the card information. 
        public void DisplayCard(BasicCard card)
        {
            //Set the large display card up the same as a clicked card.
            displayCard.SetUp(card.CardID);
            
            // Format the card's information and store it to be displayed.
            displayText.text = TranslateCard(card);

            // Set the scroll bar to the top of the card's information.
            cardTextScrollbar.value = 1f;

            // Set up the alt art button.
            altArtButton.onClick.RemoveAllListeners();
            if (card.GetCardData.altArtIDs == null)
            { 
                altArtButton.interactable = false;
            }
            else
            {
                altArtButton.interactable = true;
                altArtButton.onClick.AddListener(SwapDisplayToAltArt);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// A method that formats a cards information into a nice string to be displayed.  It also compares the original card data and any local values on the card for differences to color.
        /// </summary>
        /// <param name="card">The card to be translated.</param>
        /// <returns>A string with translated info on the card.</returns>
        private string TranslateCard(BasicCard card)
        {
            if (card.GetCardData == null)
            {
                Debug.LogError("Card data for " + card.ToString() + " is null.  Double check why it wasn't set up.");
                return "ERROR: Card data null.";
            }
            
            StringBuilder cardInfo = new StringBuilder(1000);
            cardInfo.Append(card.CardNumber + " " + card.CharName + ": " + card.CharTitle +
                "\n" + card.ClassTitle + "/Cost: ");

            //cardInfo.Append(ColorTextIfDifferent(card.DeploymentCost.ToString(), card.GetCardData.deploymentCost.ToString()));
            cardInfo.Append(card.DeploymentCost.ToString());

            if (card.Promotable)
            {
                //cardInfo.Append("(").Append(ColorTextIfDifferent(card.PromotionCost.ToString(), card.GetCardData.promotionCost.ToString())).Append(")");
                cardInfo.Append("(").Append(card.PromotionCost.ToString()).Append(")");
            }

            cardInfo.Append("\n");

            //adds a well-formatted list of the colors on the card to the cardInfo.
            //This is complicated because we need to know how many entries to add before we add them due to the backslashes.
            //Start by adding all but the last color to the cardInfo.
            List<string> colorList = card.CardColorList;
            for (int i = 0; i < colorList.Count - 1; i++)
            {
                //confirms if the color in the Color List is part of the ColorsEnum. 
                CipherData.ColorsEnum colorValue;
                if (Enum.TryParse(colorList[i], out colorValue))
                    if (Enum.IsDefined(typeof(CipherData.ColorsEnum), colorValue))
                    {
                        /*
                        //If the color is on the local card and not the cardData, print that color in green text.
                        if (card.CardColorArray[(int)colorValue] == card.GetCardData.cardColor[(int)colorValue])
                        {
                            cardInfo.Append(colorValue.ToString() + "/");
                        }
                        else
                        {
                            cardInfo.Append("<color=green>" + colorValue.ToString() + "</color>" + "/");
                        }
                        */
                        cardInfo.Append(colorValue.ToString() + "/");
                    }
                    else
                        Debug.LogWarning(card.CardID.ToString() + "'s " + colorList[i] + " is not an underlying value of the ColorsEnum enumeration.");
                else
                    Debug.LogWarning(card.CardID.ToString() + "'s " + colorList[i] + "is not a member of the Colors enumeration.");
            }

            //Adds the final color to the cardInfo
            if (colorList.Count > 0)
            {
                //confirms if the color in the Color List is part of the ColorsEnum. 
                CipherData.ColorsEnum colorValue;
                if (Enum.TryParse(colorList[colorList.Count - 1], out colorValue))
                    if (Enum.IsDefined(typeof(CipherData.ColorsEnum), colorValue))
                    {
                        /*
                        //If the color is on the local card and not the cardData, print that color in green text.
                        if (card.CardColorArray[(int)colorValue] == card.GetCardData.cardColor[(int)colorValue])
                        {
                            cardInfo.Append(colorValue.ToString());
                        }
                        else
                        {
                            cardInfo.Append("<color=green>" + colorValue.ToString() + "</color>");
                        }
                        */
                        cardInfo.Append(colorValue.ToString());
                    }
                    else
                        Debug.LogWarning(card.CardID.ToString() + "'s " + colorList[colorList.Count - 1] + " is not an underlying value of the ColorsEnum enumeration.");
                else
                    Debug.LogWarning(card.CardID.ToString() + "'s " + colorList[colorList.Count - 1] + "is not a member of the Colors enumeration.");
            }
            //If no colors on card, then print "Colorless".
            else if (colorList.Count == 0)
            {
                /*
                //Check if there were colors on the original cardData
                int n = 0;
                for (int i = 0; i < card.GetCardData.cardColor.Length; i++)
                {
                    if (card.GetCardData.cardColor[i])
                    {
                        n++;
                    }
                }

                if (n == 0)         //No color values in the card data or the local card
                {
                    cardInfo.Append("Colorless");
                }
                else                //The cardData has colors which were removed on the local card; display the text in green.
                {
                    cardInfo.Append("<color=green>Colorless</color>");
                }
                */
                cardInfo.Append("Colorless");
            }

            //adds a well-formatted list of the genders on the card to the cardInfo.
            //Loops through each possible gender
            for (int i = 0; i < card.CharGenderArray.Length; i++)
            {
                //if the gender is on the card then add the gender name to the list
                if (card.CharGenderArray[i])
                {
                    /*
                    //Check if the gender was on the original card data and if not print it in green text.
                    if (card.GetCardData.charGender[i])
                    {
                        cardInfo.Append("/").Append(((CipherData.GendersEnum)i).ToString());
                    }
                    else
                    {
                        cardInfo.Append("/<color=green>").Append(((CipherData.GendersEnum)i).ToString()).Append("</color>");
                    }
                    */
                    cardInfo.Append("/").Append(((CipherData.GendersEnum)i).ToString());
                }
            }

            //adds a well-formatted list of the weapons on the card to the cardInfo.
            //Loops through each possible weapon
            for (int i = 0; i < card.CharWeaponArray.Length; i++)
            {
                //if the weapon is on the card then add the weapon name to the list
                if (card.CharWeaponArray[i])
                {
                    /*
                    //Check if the weapon was on the original card data and if not print it in green text.
                    if (card.GetCardData.charWeaponType[i])
                    {
                        cardInfo.Append("/").Append(((CipherData.WeaponsEnum)i).ToString());
                    }
                    else
                    {
                        cardInfo.Append("/<color=green>").Append(((CipherData.WeaponsEnum)i).ToString()).Append("</color>");
                    }
                    */
                    cardInfo.Append("/").Append(((CipherData.WeaponsEnum)i).ToString());
                }
            }

            //adds a well-formatted list of the unit types on the card to the cardInfo.
            //Loops through each possible unit type
            for (int i = 0; i < card.UnitTypeArray.Length; i++)
            {
                //if the unit type is on the card then add the type name to the list
                if (card.UnitTypeArray[i])
                {
                    /*
                    //Check if the unit type was on the original card data and if not print it in green text.
                    if (card.GetCardData.unitTypes[i])
                    {
                        cardInfo.Append("/").Append(((CipherData.UnitTypesEnum)i).ToString());
                    }
                    else
                    {
                        cardInfo.Append("/<color=green>").Append(((CipherData.UnitTypesEnum)i).ToString()).Append("</color>");
                    }
                    */
                    cardInfo.Append("/").Append(((CipherData.UnitTypesEnum)i).ToString());
                }
            }

            cardInfo.Append("\n");

            //cardInfo.Append(ColorTextIfDifferent(card.CurrentAttackValue.ToString(), card.GetCardData.baseAttack.ToString())).Append(" ATK/");
            //cardInfo.Append(ColorTextIfDifferent(card.CurrentSupportValue.ToString(), card.GetCardData.baseSupport.ToString())).Append(" SUPP/");
            cardInfo.Append(card.BaseAttack.ToString()).Append(" ATK/");
            cardInfo.Append(card.BaseSupport.ToString()).Append(" SUPP/");

            //adds a card's range to the cardInfo
            cardInfo.Append(PrintRange(card));

            if (card.CardSkills.Length > 0)
            {
                for (int i = 0; i < card.CardSkills.Length; i++)
                {
                    cardInfo.AppendLine("\n").Append(card.CardSkills[i]);
                }
            }

            /*
            //add skill change information if present on the card.
            if (card.SkillChangeTracker.Count > 0)
            {
                for (int i = 0; i < card.SkillChangeTracker.Count; i++)
                {
                    //check if the tracker's entry is blank and ignore it if so.
                    if (!card.SkillChangeTracker[i].Equals(""))
                    {
                        cardInfo.AppendLine("\n").Append("<color=green>").Append(card.SkillChangeTracker[i]).Append("</color>");
                    }

                }
            }
            */

            // Finish off by adding the character quote and illustrator
            cardInfo.AppendLine("\n").Append(card.CharQuote);
            cardInfo.AppendLine().Append("Illust. " + card.CardIllustrator);

            return cardInfo.ToString();
        }

        /*
        /// <summary>
        /// This method prints the given text in a specified color if it differs from given reference text.
        /// </summary>
        /// <param name="textToPrint">the text that will be printed either in the given color or as normal.</param>
        /// <param name="referenceText">the text to compare to the printed text.  If the two are different, the printed text will be in the given color.</param>
        /// <param name="color">text to specify the color.  supported inputs are black, blue, green, orange, purple, red, white, and yellow; defaults to green.</param>
        private string ColorTextIfDifferent(string textToPrint, string referenceText, string color = "green")
        {
            if (!color.Equals("green") && !color.Equals("black") && !color.Equals("blue") && !color.Equals("orange") && !color.Equals("purple") && !color.Equals("red")
                && !color.Equals("white") && !color.Equals("yellow"))
            {
                Debug.LogWarning("InfoPanelController.ColorTextIfDifferent() does not recognize \"" + color + "\" as a valid color.  Text will be rended in green if different.");
                color = "green";
            }

            if (textToPrint == referenceText)
                return textToPrint;
            else
                return "<color=" + color + ">" + textToPrint + "</color>";
        }
        */

        //Formats a card's range information nicely.
        private string PrintRange(BasicCard card)
        {
            string rangeText = "";
            int numEntries = 0;
            //int originalEntries = 0;            //helps with checks to confirm differences between the current card and the original.
            bool firstEntry = true;

            for (int i = 0; i < card.BaseRangeArray.Length; i++)
            {
                if (card.BaseRangeArray[i])
                {
                    numEntries++;
                }
                /*
                if (card.GetCardData.baseRange[i])
                {
                    originalEntries++;
                }
                */
            }

            switch (numEntries)
            {
                //no range entries
                case 0:
                    /*
                    //checks for a difference between the original cardData and the current card's range.
                    //If a difference is found, color the text in green.
                    if (originalEntries == 0)
                    {
                        rangeText += "NO";
                    }
                    else
                    {
                        rangeText += "<color=green>NO</color>";
                    }
                    */
                    rangeText += "NO";
                    break;

                //A single range entry that needs to be posted.  Loop through the array to find the entry.
                case 1:
                    for (int i = 0; i < card.BaseRangeArray.Length; i++)
                    {
                        if (card.BaseRangeArray[i])
                        {
                            /*
                            //checks for a difference between the original cardData and the current card's range.
                            //If a difference is found, color the text in green.
                            if (card.GetCardData.baseRange[i])
                            {
                                rangeText += (i + 1);
                            }
                            else
                            {
                                rangeText += "<color=green>" + (i + 1) + "</color>";
                            }
                            */
                            rangeText += (i + 1);
                        }
                    }
                    break;

                //Two or more range entries that need to be posted.  Loop through the array to find the first entry and then the rest.
                case 2:
                case 3:
                    int n = 0;
                    while (firstEntry)
                    {
                        if (card.BaseRangeArray[n])
                        {
                            /*
                            //checks for a difference between the original cardData and the current card's range.
                            //If a difference is found, color the text in green.
                            if (card.GetCardData.baseRange[n])
                            {
                                rangeText += (n + 1);
                            }
                            else
                            {
                                rangeText += "<color=green>" + (n + 1) + "</color>";
                            }
                            */
                            rangeText += (n + 1);
                            firstEntry = false;
                        }
                        n++;
                    }

                    while (n < card.BaseRangeArray.Length)
                    {
                        if (card.BaseRangeArray[n])
                        {
                            rangeText += ", ";

                            /*
                            //checks for a difference between the original cardData and the current card's range.
                            //If a difference is found, color the text in green.
                            if (card.GetCardData.baseRange[n])
                            {
                                rangeText += (n + 1);
                            }
                            else
                            {
                                rangeText += "<color=green>" + (n + 1) + "</color>";
                            }
                            */
                            rangeText += (n + 1);
                        }
                        n++;
                    }
                    break;

                //An unexpected number of range entires.
                default:
                    Debug.LogWarning(gameObject.ToString() + " has an unexpected number of range elements!  Please check the cardData and PrintRange() method.");
                    break;
            }

            rangeText += " RNG";

            return rangeText;
        }

        private void SwapDisplayToAltArt()
        {
            if (displayCard.GetCardData.altArtIDs != null)
            {
                displayCard.SetUp(displayCard.GetCardData.altArtIDs[0]);
                DisplayCard(displayCard);
            }
        }

        #endregion
    }
}
