using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectWindow : EditorWindow {
	public enum object_type {
		tile, decal, wall
	}

	public class mapObject {
		public object_type type;
		public Sprite sprite;
		public GameObject prefab;
	}

	public mapObject placeObject;

	Grid grid;

	public void init() {
		grid = (Grid)FindObjectOfType (typeof(Grid));
		placeObject.sprite = (Sprite)AssetDatabase.LoadAssetAtPath ("Assets/Images/isometric_test_tile.png", typeof(Sprite));
	}

	void OnGUI() {
		placeObject.sprite = (Sprite)EditorGUILayout.ObjectField ("Texture", placeObject.sprite, typeof(Sprite), false);
		placeObject.prefab = (GameObject)EditorGUILayout.ObjectField ("Prefab", placeObject.prefab, typeof(GameObject));
	}
}
