using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridEditor : Editor
{
    SerializedProperty  width, height, length, size;
    GridManager gridManager;

    private void OnEnable()
    {
        width = serializedObject.FindProperty("cellWidth");
        height = serializedObject.FindProperty("cellHeight");
        length = serializedObject.FindProperty("cellLength");
        size = serializedObject.FindProperty("cellSize");
    }

    public override void OnInspectorGUI()
    {
        gridManager = (GridManager)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Clear List"))
        {
            gridManager.ClearList();
        }
        if (GUILayout.Button("Resize All Cells"))
        {
            gridManager.ResizeCells();
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.IntSlider(width, 0, 50, new GUIContent("Width"));
        EditorGUILayout.IntSlider(height, 0, 50, new GUIContent("Height"));
        EditorGUILayout.IntSlider(length, 0, 50, new GUIContent("Length"));
        if (EditorGUI.EndChangeCheck())
        {
            gridManager.CalculatePoints();
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.Slider(size, 0, 5, new GUIContent("Size"));
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            gridManager.ResizeCells();
            gridManager.CalculatePoints();
        }

        if (GUILayout.Button("Calculate Points"))
        {
            gridManager.CalculatePoints();
        }
    }
}
