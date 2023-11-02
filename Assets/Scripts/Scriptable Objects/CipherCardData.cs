using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SakuraStudios.FECipherCollection
{
    // This object stores the data associated with each card, almost like the printed text on the card itself.
    // Data is loaded from scriptable objects to allow different instantiations of cards to load the same data easily and always know what their original values were.
    // NOTE: Remember to update the CipherCardDataEditor if you add anything here so it's displayed in the Inspector.
    // Adding the CreateAssetMenu attribute to allow us to create this scriptable object more easily in the Unity Editor.
    [CreateAssetMenu(menuName = "Cipher Card Data")]
    public class CipherCardData : ScriptableObject
    {
        public CipherData.CardID cardID;
        public CipherData.CardRarity cardRarity;
        
        public string cardNumber;
        public string charTitle;
        // Tagging charQuote with the multiline attribute to allow for easier readability in the editor.
        [Multiline]
        public string charQuote;
        public string cardIllustrator;

        // Tagging cardSkills with the multiline attribute to allow for easier readability in the editor.
        [Multiline]
        public string[] cardSkills;
        public bool[] skillTypes = new bool[Enum.GetNames(typeof(CipherData.SkillTypesEnum)).Length];
        public string charName;
        public string classTitle;
        public int deploymentCost;
        public bool canPromote;
        public int promotionCost;
        public bool[] cardColor = new bool[Enum.GetNames(typeof(CipherData.ColorsEnum)).Length];
        public bool[] charGender = new bool[Enum.GetNames(typeof(CipherData.GendersEnum)).Length];
        public bool[] charWeaponType = new bool[Enum.GetNames(typeof(CipherData.WeaponsEnum)).Length];
        public bool[] unitTypes = new bool[Enum.GetNames(typeof(CipherData.UnitTypesEnum)).Length];
        public int baseAttack;
        public int baseSupport;
        public bool[] baseRange = new bool[Enum.GetNames(typeof(CipherData.RangesEnum)).Length];
    }
}
