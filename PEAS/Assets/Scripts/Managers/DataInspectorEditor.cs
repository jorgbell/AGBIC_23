using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataManager))]
public class DataInspectorEditor : Editor
{
    private SerializedProperty pointsDataProperty;
    private List<PeaType> enumValues;
    private bool isFoldoutOpen;


    private void OnEnable()
    {
        pointsDataProperty = serializedObject.FindProperty("pointsData");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        enumValues = new List<PeaType>();
        for (int i = 0; i < Enum.GetValues(typeof(PeaType)).Length - 1; i++)
        {
            enumValues.Add((PeaType)i);
        };
        int enumLength = enumValues.Count;
        int pointsDataSize = pointsDataProperty.arraySize;

        // Update pointsData size if enum length is changed
        if (enumLength != pointsDataSize)
        {
            pointsDataProperty.arraySize = enumLength;
        }

        isFoldoutOpen = EditorGUILayout.Foldout(isFoldoutOpen, "Points per Pea");
        if (isFoldoutOpen)
        {
            for (int i = 0; i < enumLength; i++)
            {
                EditorGUILayout.BeginHorizontal();
                SerializedProperty enumDataProperty = pointsDataProperty.GetArrayElementAtIndex(i);
                PeaType enumValue = enumValues[i];
                EditorGUILayout.PropertyField(enumDataProperty, new GUIContent(enumValue.ToString()));
                EditorGUILayout.EndHorizontal();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}