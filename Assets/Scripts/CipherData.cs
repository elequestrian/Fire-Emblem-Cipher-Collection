using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Static data class that sets the standard values for many concepts fundamental to the FE Cipher Card Game.
namespace Com.SakuraStudios.FECipherCollection
{
    public static class CipherData
    {
        // NOTE: Be sure to change the size of the below arrays in the exisiting card data as well as this information
        // if a size change is needed.  Otherwise, errors may occur.

        public enum ColorsEnum { Red, Blue, White, Black, Green, Purple, Yellow, Brown }

        public enum GendersEnum { Male, Female }

        public enum WeaponsEnum { Sword, Lance, Axe, Bow, Tome, Staff, Brawl, Dragonstone, Knife, Fang }

        public enum UnitTypesEnum { Armored, Flier, Beast, Dragon, Mirage, Monster }

        public enum RangesEnum { Range1, Range2, Range3 }

        //Creating a skill enum for each card so that the computer can know what types of skills are on each card.
        public enum SkillTypesEnum
        {
            Support, ClassChange, Formation, LevelUp, Union, CarnageForm, Bond, DragonVein, Hero, Twin, Increase,
            Awakening, DragonBlood, LegendaryItem, CrestPower
        }

        //This enum keeps track of the current phase in the game.
        public enum PhasesEnum { Beginning, Bond, Deployment, Action, End }

        // List of all valid CardIDs current set up to function in the game.
        public enum CardID
        {
            B01N001,        // Marth 5
            B01N001p,       // Marth 5 holo
            B01N002,        // Marth 2 
            B01N003,        // Marth 1
            B01N003p,       // Marth 1 holo 
            B01N004,        // Caeda 4
            B01N004p,       // Caeda 4 holo
            B01N005,        // Caeda 2
            B01N006,        // Caeda 1
            B01N016,        // Ogma 4
            B01N017,        // Ogma 2
            B01N018,        // Ogma 1
            B01N056,        // Lucina 1 
            P01N003,        // Marth 1
            P01N006,        // Jagan 3
            P01N012,        // Marth 2
            P01N013,        // Marth 3
            S01N001,        // Marth 4
            S01N001p,       // Marth 4 holo
            S01N002,        // Caeda 3
            S01N002p,       // Caeda 3 holo
            S01N003,        // Jagen 3
            S01N004,        // Ogma 4
            S01N004p,       // Ogma 4 holo
        }

        // List of all card rarities
        public enum CardRarity
        {
            N,      //Normal
            HN,     //High Normal
            R,      //Rare
            Rp,     //Rare+
            SR,     //Super Rare
            SRp,    //Super Rare+
            pX,     //Secret Rare
            HR,     //Hero Rare
            PR,     //Promo
            PRp,    //Promo Holo
            ST,     //Starter
            STp     //Starter Holo
        }
    }
}
