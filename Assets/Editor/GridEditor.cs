using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor {

	Grid grid;

	private int oldindex = 0;

	void OnEnable() {
		grid = (Grid)target;
	}

	[MenuItem("Assets/Create/TileSet")]
	static void CreateTileSet() {
		var asset = ScriptableObject.CreateInstance<TileSet> ();
		var path = AssetDatabase.GetAssetPath (Selection.activeObject);

		if (string.IsNullOrEmpty (path)) {
			path = "Assets";
		} else if (Path.GetExtension(path) != "") {
			path = path.Replace (Path.GetFileName (path), "");
		} else {
			path += "/";
		}

		var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "TileSet.asset");
		AssetDatabase.CreateAsset (asset, assetPathAndName);
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
		asset.hideFlags = HideFlags.DontSave;

	}

	public override void OnInspectorGUI() {
		//base.OnInspectorGUI (); // to show the original GUI

		// grid width
		GUILayout.BeginHorizontal (); 
		grid.width = EditorGUILayout.FloatField("Grid Width",grid.width);
		GUILayout.EndHorizontal(); 

		// grid heigth
		GUILayout.BeginHorizontal (); 
		grid.heigth = EditorGUILayout.FloatField("Grid Heigth", grid.heigth);
		GUILayout.EndHorizontal(); 

		// grid multiplier
		GUILayout.BeginHorizontal (); 
		grid.multiplier = EditorGUILayout.FloatField("Grid Multiplier", grid.multiplier);
		GUILayout.EndHorizontal(); 

		// grid color
		if (GUILayout.Button ("Open Grid Window")) {
			GridWindow window = (GridWindow)EditorWindow.GetWindow (typeof(GridWindow));
			window.init ();
		}

		// Tile prefab
		EditorGUI.BeginChangeCheck ();
		var newTilePrefab = (Transform)EditorGUILayout.ObjectField("Tile Prefab", grid.tilePrefab, typeof(Transform), false);
		if(EditorGUI.EndChangeCheck()) {
			grid.tilePrefab = newTilePrefab;
			Undo.RecordObject(target, "Grid Changed");
		}

		// Tile Map
		EditorGUI.BeginChangeCheck();
		var newTileSet = (TileSet)EditorGUILayout.ObjectField ("Tileset", grid.tileSet, typeof(TileSet), false);
		if (EditorGUI.EndChangeCheck ()) {
			grid.tileSet = newTileSet;
			Undo.RecordObject (target, "Grid Changed");

		}

		if (grid.tileSet != null) {
			EditorGUI.BeginChangeCheck ();
			var names = new string[grid.tileSet.prefabs.Length];
			var values = new int[names.Length];

			for (int i = 0; i < names.Length; i++) {
				names [i] = grid.tileSet.prefabs [i] != null ? grid.tileSet.prefabs [i].name : "";
				values [i] = i;
			}

			var index = EditorGUILayout.IntPopup ("Select Tile", oldindex, names, values);

			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObject (target, "Grid Changed");
				if (oldindex != index) {
					oldindex = index;
					grid.tilePrefab = grid.tileSet.prefabs [index];
				}
			}
		}
	}

	void OnSceneGUI() {
		int controlId = GUIUtility.GetControlID (FocusType.Passive);
		Event e = Event.current;
		Ray ray = Camera.current.ScreenPointToRay (new Vector3 (e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
		Vector3 mousePos = ray.origin;

		if (e.isMouse && e.type == EventType.MouseDown) {
			GUIUtility.hotControl = controlId;
			e.Use ();

			GameObject gameObject;
			Transform prefab = grid.tilePrefab;

			if (prefab) {
				Undo.IncrementCurrentGroup ();
				gameObject = (GameObject)PrefabUtility.InstantiatePrefab (prefab.gameObject);

				Undo.RegisterCreatedObjectUndo (gameObject, "Create " + gameObject.name);
			}
		}

	}

}
