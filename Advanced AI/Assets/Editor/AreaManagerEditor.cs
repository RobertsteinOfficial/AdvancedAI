using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AreaManager))]
public class AreaManagerEditor : Editor
{
    AreaManager myTarget;

    private void OnEnable()
    {
        myTarget = (AreaManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (myTarget.showButtons)
            DrawAddButton();
    }

    void DrawAddButton()
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Add Area", GUILayout.Height(20), GUILayout.MaxWidth(60));
        if (GUILayout.Button("+", GUILayout.Height(20), GUILayout.MinWidth(20), GUILayout.MaxWidth(60)))
        {
            myTarget.AddArea();
        }
        if (GUILayout.Button("-", GUILayout.Height(20), GUILayout.MinWidth(20), GUILayout.MaxWidth(60)))
        {
            myTarget.RemoveArea();
        }
        GUILayout.EndHorizontal();
    }
}
