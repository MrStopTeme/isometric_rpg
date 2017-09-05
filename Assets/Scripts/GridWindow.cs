using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridWindow : EditorWindow {
	Grid grid;

	public void init() {
		grid = (Grid)FindObjectOfType (typeof(Grid));
	}

	void OnGUI() {
		grid.color = EditorGUILayout.ColorField("Grid Color", grid.color, GUILayout.Width(200));
		grid.width = EditorGUILayout.FloatField("Grid Width",grid.width);
		grid.heigth = EditorGUILayout.FloatField("Grid Heigth", grid.heigth);
	}

}
