using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridCell))]
public class CellEditor : Editor
{
    SerializedProperty width, height, length, size;
    GridCell cellScript;

    private void OnEnable()
    {
        width = serializedObject.FindProperty("cellWidth");
        height = serializedObject.FindProperty("cellHeight");
        length = serializedObject.FindProperty("cellLength");
        size = serializedObject.FindProperty("cellSize");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        cellScript = (GridCell)target;
        
        if (GUILayout.Button("Rotate 90º Right"))
        {
            cellScript.Rotate(90);
        }
        if (GUILayout.Button("Rotate 90º Left"))
        {
            cellScript.Rotate(-90);
        }
    }
}
