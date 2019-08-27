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
        gridManager = (GridManager)target;
        width = serializedObject.FindProperty("cellWidth");
        height = serializedObject.FindProperty("cellHeight");
        length = serializedObject.FindProperty("cellLength");
        size = serializedObject.FindProperty("cellSize");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.IntSlider(width, 0, 10, new GUIContent("Width"));
        EditorGUILayout.IntSlider(height, 0, 10, new GUIContent("Height"));
        EditorGUILayout.IntSlider(length, 0, 10, new GUIContent("Length"));
        if (EditorGUI.EndChangeCheck())
        {
            gridManager.CalculatePoints(Vector3.zero);
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.Slider(size, 0, 5, new GUIContent("Size"));
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            gridManager.ResizeCells();
            gridManager.CalculatePoints(Vector3.zero);
        }
    }
}
