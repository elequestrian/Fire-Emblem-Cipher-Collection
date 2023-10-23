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
            B01N003STp,
            B01N056HN
        }
    }
}
