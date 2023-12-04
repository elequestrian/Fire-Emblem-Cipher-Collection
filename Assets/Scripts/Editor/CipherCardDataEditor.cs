using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;

namespace Com.SakuraStudios.FECipherCollection
{
    // A custom editor for the Scriptable objects CipherCardData that displays the information in a more legible format for easier work.
    [CustomEditor(typeof(CipherCardData)), CanEditMultipleObjects]
    public class CipherCardDataEditor : Editor
    {
        /* CardData reference
            public CipherData.CardID cardID;
            public CipherData.CardRarity cardRarity;
           public CipherData.CardID[] altArtIDs;
        
           public string cardNumber;
           public string charTitle;
           public string charQuote;
           public string cardIllustrator;

           public string[] cardSkills;
           public bool[] skillTypes = new bool[CipherData.NumSkillTypes];
           public string charName;
           public string classTitle;
           public int deploymentCost;
           public bool canPromote;
           public int promotionCost;
           public bool[] cardColor = new bool[CipherData.NumColors];
           public bool[] charGender = new bool[CipherData.NumGenders];
           public bool[] charWeaponType = new bool[CipherData.NumWeapons];
           public bool[] unitTypes = new bool[CipherData.NumTypes];
           public int baseAttack;
           public int baseSupport;
           public bool[] baseRange = new bool[CipherData.NumRanges];
           */

        SerializedProperty cardIDProperty;
        SerializedProperty cardRarityProperty;
        SerializedProperty altArtIDsProperty;

        SerializedProperty cardNumberProperty;
        SerializedProperty charTitleProperty;
        SerializedProperty charQuoteProperty;
        SerializedProperty cardIllustratorProperty;
        SerializedProperty cardSkillsProperty;
        SerializedProperty skillTypesProperty;

        SerializedProperty charNameProperty;
        SerializedProperty classTitleProperty;
        SerializedProperty deploymentCostProperty;
        SerializedProperty canPromoteProperty;
        SerializedProperty promotionCostProperty;
        SerializedProperty cardColorProperty;
        SerializedProperty charGenderProperty;
        SerializedProperty charWeaponTypeProperty;
        SerializedProperty unitTypesProperty;
        SerializedProperty baseAttackProperty;
        SerializedProperty baseSupportProperty;
        SerializedProperty baseRangeProperty;

        // Expand all foldouts by default
        bool skillFoldout = true;
        bool colorFoldout = true;
        bool genderFoldout = true;
        bool weaponFoldout = true;
        bool unitFoldout = true;
        bool rangeFoldout = true;

        void OnEnable()
        {
            cardIDProperty = serializedObject.FindProperty("cardID");
            cardRarityProperty = serializedObject.FindProperty("cardRarity");
            altArtIDsProperty = serializedObject.FindProperty("altArtIDs");
            
            cardNumberProperty = serializedObject.FindProperty("cardNumber"); ;
            charTitleProperty = serializedObject.FindProperty("charTitle");
            charQuoteProperty = serializedObject.FindProperty("charQuote");
            cardIllustratorProperty = serializedObject.FindProperty("cardIllustrator");
            cardSkillsProperty = serializedObject.FindProperty("cardSkills");
            skillTypesProperty = serializedObject.FindProperty("skillTypes");

            charNameProperty = serializedObject.FindProperty("charName");
            classTitleProperty = serializedObject.FindProperty("classTitle");
            deploymentCostProperty = serializedObject.FindProperty("deploymentCost");
            canPromoteProperty = serializedObject.FindProperty("canPromote");
            promotionCostProperty = serializedObject.FindProperty("promotionCost");
            cardColorProperty = serializedObject.FindProperty("cardColor");
            charGenderProperty = serializedObject.FindProperty("charGender");
            charWeaponTypeProperty = serializedObject.FindProperty("charWeaponType");
            unitTypesProperty = serializedObject.FindProperty("unitTypes");
            baseAttackProperty = serializedObject.FindProperty("baseAttack");
            baseSupportProperty = serializedObject.FindProperty("baseSupport");
            baseRangeProperty = serializedObject.FindProperty("baseRange");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorStyles.textField.wordWrap = true; // This sets the wordwrap value of the properties

            EditorGUILayout.PropertyField(cardIDProperty);
            EditorGUILayout.PropertyField(cardRarityProperty);
            EditorGUILayout.PropertyField(altArtIDsProperty, true);

            EditorGUILayout.PropertyField(cardNumberProperty);
            EditorGUILayout.PropertyField(charTitleProperty);
            EditorGUILayout.PropertyField(charQuoteProperty, GUILayout.ExpandHeight(true));
            EditorGUILayout.PropertyField(cardIllustratorProperty);
            EditorGUILayout.PropertyField(cardSkillsProperty, true, GUILayout.ExpandHeight(true));
            skillFoldout = ShowCipherList(skillFoldout, skillTypesProperty, typeof(CipherData.SkillTypesEnum));

            EditorGUILayout.PropertyField(charNameProperty);
            EditorGUILayout.PropertyField(classTitleProperty);
            EditorGUILayout.PropertyField(deploymentCostProperty);
            EditorGUILayout.PropertyField(canPromoteProperty);
            EditorGUILayout.PropertyField(promotionCostProperty);

            colorFoldout = ShowCipherList(colorFoldout, cardColorProperty, typeof(CipherData.ColorsEnum));
            genderFoldout = ShowCipherList(genderFoldout, charGenderProperty, typeof(CipherData.GendersEnum));
            weaponFoldout = ShowCipherList(weaponFoldout, charWeaponTypeProperty, typeof(CipherData.WeaponsEnum));
            unitFoldout = ShowCipherList(unitFoldout, unitTypesProperty, typeof(CipherData.UnitTypesEnum));

            EditorGUILayout.PropertyField(baseAttackProperty);
            EditorGUILayout.PropertyField(baseSupportProperty);

            rangeFoldout = ShowCipherList(rangeFoldout, baseRangeProperty, typeof(CipherData.RangesEnum));


            serializedObject.ApplyModifiedProperties();
        }

        // This method helps draw all boolean arrays for the different card attributes consistently.
        private bool ShowCipherList(bool foldout, SerializedProperty list, Type cipherEnum)
        {
            //check in case we try to show an non-array or pass in a Type that's not an Enum!
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return foldout;
            }
            else if (!cipherEnum.IsEnum)
            {
                EditorGUILayout.HelpBox(cipherEnum.Name + " is not an Enum!", MessageType.Error);
                return foldout;
            }

            //Begins a foldout group and returns the value based on user input.
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, list.displayName);

            if (foldout)
            {
                EditorGUI.indentLevel += 1;
                //SerializedProperty size = list.FindPropertyRelative("Array.size");
                //EditorGUILayout.PropertyField(size);
                string[] enumNames = Enum.GetNames(cipherEnum);
                for (int i = 0; i < list.arraySize; i++)
                {
                    string name = "";
                    if (Enum.IsDefined(cipherEnum, i))
                        name = enumNames[i];

                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent(name));
                }
                EditorGUI.indentLevel -= 1;
            }

            // Be sure to end the Foldout Group.
            EditorGUILayout.EndFoldoutHeaderGroup();

            return foldout;
        }
    }
}
