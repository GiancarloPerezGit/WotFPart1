using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor
{
    public BoardCreator current
    {
        get
        {
            return (BoardCreator)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Clear"))
            current.Clear();
        if (GUILayout.Button("Grow"))
            current.Grow();
        if (GUILayout.Button("Shrink"))
            current.Shrink();
        if (GUILayout.Button("Grow Area"))
            current.GrowArea();
        if (GUILayout.Button("Shrink Area"))
            current.ShrinkArea();
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();
        if (GUILayout.Button("Rotate"))
            current.Rotate();
        if (GUILayout.Button("Create Flat"))
            current.CreateTile();
        if (GUILayout.Button("Create Slope"))
            current.SCreateTile();
        if (GUILayout.Button("Create Out Corner"))
            current.OCreateTile();
        if (GUILayout.Button("Create In Corner"))
            current.ICreateTile();
        if (GUILayout.Button("Remove"))
            current.RemoveTile();
        if (GUI.changed)
            current.UpdateMarker();
    }

}
