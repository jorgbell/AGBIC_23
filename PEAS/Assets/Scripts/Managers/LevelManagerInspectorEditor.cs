using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerInspectorEditor : Editor
{
    private SerializedProperty nObjectsDataProperty;
    private List<ScenarioObjectType> enumValues;
    private bool isFoldoutOpen;
    private void OnEnable()
    {
        nObjectsDataProperty = serializedObject.FindProperty("MaxActiveObjectsInLevel");
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        // Obtener la referencia al objeto LevelManager
        LevelManager levelManager = (LevelManager)target;

        int enumSize = Enum.GetValues(typeof(ScenarioObjectType)).Length;
        int numScenarioObjects = levelManager.nScenarioObjects;
        // Actualizar nScenarioObjects si la longitud del enum es diferente
        if (enumSize != numScenarioObjects)
        {
            // Actualizar el valor de nScenarioObjects en el LevelManager
            levelManager.nScenarioObjects = enumSize;
            Debug.Log(levelManager.nScenarioObjects);
        }
        enumValues = new List<ScenarioObjectType>();
        for (int i = 0; i < enumSize-1; i++)
        {
            enumValues.Add((ScenarioObjectType)i);
        };
        int scenarioDataSize = nObjectsDataProperty.arraySize;
        // Update pointsData size if enum length is changed
        if (enumSize != scenarioDataSize)
        {
            nObjectsDataProperty.arraySize = enumSize;
        }


        isFoldoutOpen = EditorGUILayout.Foldout(isFoldoutOpen, "Max number of scenario objects in level");
        if (isFoldoutOpen)
        {
            for (int i = 0; i < enumSize-1; i++)
            {
                EditorGUILayout.BeginHorizontal();
                SerializedProperty enumDataProperty = nObjectsDataProperty.GetArrayElementAtIndex(i);
                ScenarioObjectType enumValue = enumValues[i];
                EditorGUILayout.PropertyField(enumDataProperty, new GUIContent(enumValue.ToString()));
                EditorGUILayout.EndHorizontal();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
