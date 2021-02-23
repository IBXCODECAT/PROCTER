using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateTerrain))]
public class TerrainInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Generate"))
        {
            GameObject.Find("Terrain").GetComponent<GenerateTerrain>().Generate();
        }
    }
}
