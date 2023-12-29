using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            M00N001,        // Default Cipher back
            B01N001,        // Marth 5
            B01N001p,       // Marth 5 holo
            B01N002,        // Marth 2 
            B01N003,        // Marth 1
            B01N003s,       // Marth 1 holo 
            B01N004,        // Caeda 4
            B01N004p,       // Caeda 4 holo
            B01N005,        // Caeda 2
            B01N006,        // Caeda 1
            B01N007,        // Cain 3
            B01N007s,       // Cain 3 starter
            B01N008,        // Cain 1
            B01N009,        // Abel 3
            B01N009s,       // Abel 3 starter
            B01N010,        // Able 1
            B01N011,        // Draug 3
            B01N012,        // Draug 1
            B01N013,        // Gordin 3
            B01N014,        // Gordin 1
            B01N015,        // Wrys 1
            B01N016,        // Ogma 4
            B01N017,        // Ogma 2
            B01N018,        // Ogma 1
            B01N019,        // Bord 1
            B01N020,        // Cord 1
            B01N021,        // Barst 2
            B01N022,        // Navarre 4
            B01N022p,       // Navarre 4 plus
            B01N023,        // Navarre 2
            B01N024,        // Navarre 1
            B01N025,        // Lena 3
            B01N026,        // Lena 1
            B01N027,        // Julian 2
            B01N028,        // Merric 4
            B01N028s,       // Merric 4 starter
            B01N029,        // Merric 1
            B01N030,        // Minerva 4
            B01N030p,       // Minerva 4 plus
            B01N031,        // Minerva 1
            B01N032,        // Maria 3
            B01N033,        // Maria 1
            B01N034,        // Jeorge 3
            B01N035,        // Linde 3
            B01N036,        // Linde 1
            B01N036s,       // Linde 1 holo
            B01N037,        // Midia 3
            B01N038,        // Palla 3
            B01N038p,       // Palla 3 plus
            B01N039,        // Palla 1
            B01N040,        // Catria 3
            B01N040p,       // Catria 3 plus
            B01N041,        // Catria 1
            B01N042,        // Est 3
            B01N042p,       // Est 3 plus
            B01N043,        // Est 1
            B01N044,        // Astram 3
            B01N045,        // Xane 3
            B01N046,        // Tiki 5
            B01N047,        // Tiki 1
            B01N048,        // Elice 2
            B01N049,        // Athena 2
            B01N050,        // Camus 5
            B01N051,        // Chrom 5
            B01N051p,       // Chrom 5 plus
            B01N052,        // Chrom 2
            B01N053,        // Chrom 1
            B01N053s,       // Chrom 1 holo
            B01N054,        // Lucina 4
            B01N054p,       // Lucina 4 plus
            B01N055,        // Lucina 2
            B01N056,        // Lucina 1
            B01N057,        // RobinF 4
            B01N057p,       // RobinF 4 plus
            B01N057s,       // RobinF 4 starter
            B01N058,        // RobinF 1
            B01N059,        // Lissa 3
            B01N059p,       // Lissa 3 plus
            B01N059s,       // Lissa 3 starter
            B01N060,        // Lissa 1
            B01N061,        // Frederick 3
            B01N061s,       // Frederick 3 holo
            B01N062,        // Virion 3
            B01N063,        // Virion 1
            B01N064,        // Sully 3
            B01N064s,       // Sully 3 holo
            B01N065,        // Sully 1
            B01N066,        // Stahl 3
            B01N066s,       // Stahl 3 holo
            B01N067,        // Stahl 1
            B01N068,        // Sumia 3
            B01N069,        // Sumia 1
            B01N070,        // Lon'qu 3
            B01N070p,       // Lon'qu 3 plus
            B01N070s,       // Lon'qu 3 starter
            B01N071,        // Lon'qu 1
            B01N072,        // Maribelle 1
            B01N073,        // Gaius 3
            B01N074,        // Gaius 1
            B01N075,        // Cordelia 3
            B01N076,        // Cordelia 1
            B01N077,        // Gregor 2
            B01N078,        // Nowi 3
            B01N079,        // Nowi 1
            B01N080,        // Tharja 4
            B01N080p,       // Tharja 4 plus
            B01N081,        // Tharja 2
            B01N082,        // Tharja 1
            B01N083,        // Olivia 2
            B01N084,        // Cherche 3
            B01N085,        // Cherche 1
            B01N086,        // Henry 3
            B01N087,        // Henry 1
            B01N088,        // MorganM 2
            B01N089,        // Gerome 4
            B01N090,        // Gerome 2
            B01N091,        // Owain 4
            B01N092,        // Owain 2
            B01N093,        // Owain 1
            B01N094,        // Severa 3
            B01N095,        // Severa 1
            B01N096,        // Noire 2
            B01N097,        // Inigo 3
            B01N097p,       // Inigo 3 plus
            B01N098,        // Inigo 1
            B01N099,        // Tiki 2
            B01N100,        // Anna 3
            P01N001,        // CorrinM 1
            P01N002,        // CorrinF 1
            P01N003,        // Marth 1
            P01N004,        // Lucina 1
            P01N005,        // Chrom 3
            P01N006,        // Jagan 3
            P01N007,        // Wrys 3
            P01N008,        // Minerva 1
            P01N009,        // Cordelia 1
            P01N010,        // Tharja 1
            P01N011,        // Tiki 2
            P01N012,        // Marth 2
            P01N013,        // Marth 3
            P01N014,        // Lucina 2
            P01N015,        // RobinM 2
            S01N001,        // Marth 4
            S01N001p,       // Marth 4 holo
            S01N002,        // Caeda 3
            S01N002p,       // Caeda 3 holo
            S01N003,        // Jagen 3
            S01N004,        // Ogma 3
            S01N004p,       // Ogma 3 holo
            S01N005,        // Navarre 3
            S01N005p,       // Navarre 3 holo
            S02N001,        // Chrom 4
            S02N001p,       // Chrom 4 plus
            S02N002,        // Kellam 1
            S02N003,        // Vaike 1
            S02N004,        // Miriel 1
            S02N004p,       // Miriel 1 holo
            S02N005,        // Ricken 1
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
            STp,    //Starter Holo
            M       //Marker    
        }
    }
}
