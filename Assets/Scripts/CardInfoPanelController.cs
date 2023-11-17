using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Com.SakuraStudios.FECipherCollection
{
    public class CardInfoPanelController : MonoBehaviour
    {
        public static CardInfoPanelController Instance = null;                      // The instance reference to this script as it is a singleton pattern.

        [SerializeField] private BasicCard displayCard;
        [SerializeField] private TextMeshProUGUI displayText;       //The display text of the InfoPanel.

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
            // Set the image of the panel to the face of the chosen card.
            //displayImage.sprite = card.CardSprite;

            // Format the card's information and store it to be displayed.
            //displayText.text = TranslateCard(card);
            displayText.text = card.CardID.ToString();


            // Set the scroll bar to the top of the card's information.
            //MoveScrollBarToValue(1f);
        }
        #endregion
    }
}
