using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(platform))]
public class platformeditor : Editor {

	// Use this for initialization
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //DrawDefaultInspector();

        platform myScript = (platform)target;
        if (GUILayout.Button("Build Platform"))
        {
            myScript.GeneratePlatform();
        }
        if (GUILayout.Button("Destroy Platform"))
        {
            myScript.DestroyExistingTiles();
        }

	}
   
	
	// Update is called once per frame
	void Update () {
		
	}
}
