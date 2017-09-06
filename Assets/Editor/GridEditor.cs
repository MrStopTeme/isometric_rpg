using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Grid))]
[AddComponentMenu("CustomMapEditor/MapEditor")]
public class GridEditor : Editor {

	public Sprite objectSprite = null;
	public placedObject.object_types objectType = placedObject.object_types.tile;
	public bool isEditing = false;

	public override void OnInspectorGUI() {
		//base.OnInspectorGUI (); // to show the original GUI



		objectSprite = (Sprite)AssetDatabase.LoadAssetAtPath<Sprite> ("Assets/Images/isometric_test_tile.png");

		// grid settings
		if (GUILayout.Button ("Grid Window")) {
			GridWindow window = (GridWindow)EditorWindow.GetWindow (typeof(GridWindow));
			window.init ();
		}

		if(!GameObject.Find("Map")) { //if there isn't a "Map" object, instantiate one; it is going to be the parent to all map elements
			GameObject Map = Instantiate<GameObject>(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Map.prefab"), new Vector3(0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
			Map.name = "Map";
		}

		objectSprite = (Sprite)EditorGUILayout.ObjectField ("Texture:", objectSprite, typeof(Sprite), false);
		objectType = (placedObject.object_types)EditorGUILayout.EnumPopup ("Object Type:", objectType);
		isEditing = (bool)EditorGUILayout.Toggle ("Editing:", isEditing);
	}

	public GameObject gameobject;
	public placedObject placed;

	void OnSceneGUI() {

		int controlId = GUIUtility.GetControlID (FocusType.Passive);
	
	/*
	 Basic workflow:
	 If you are editing:
	 	if there is something under the cursor
	 		if there is a tile under the cursor
	 			if you have tile selected, just change its texture
	 			else, just place your object
	 		if there is a wall under the cursor
	 			if you have wall selected, just change its texture
	 			else, just place your object
	 		if there is a decal under the cursor
	 			if you have decal selected, just change its texture
				else, just place your object
		if there is nothing under the cursor
			place your object

		MAKE THAT INTO CODE ASAP PLEASE
	*/
		if (isEditing) { 
			GUIUtility.hotControl = controlId;
			Event.current.Use ();
			if (Physics2D.OverlapPoint (Camera.current.ScreenToWorldPoint (Event.current.mousePosition))) {
				gameobject = Physics2D.OverlapPoint (Camera.current.ScreenToWorldPoint (Event.current.mousePosition)).gameObject;
				if (placed != null) {
					if (Event.current.button == 0 && Event.current.type == EventType.mouseDown) {
						if (gameobject.GetComponent<placedObject> ().objectType == objectType) {
							gameobject.GetComponent<SpriteRenderer> ().sprite = objectSprite;
							gameobject.name = objectSprite.name;
						} else {
							gameobject = initializeObject (gameobject);
							Instantiate (gameobject, new Vector3 (Event.current.mousePosition.x, Event.current.mousePosition.y, 0f), new Quaternion (0f, 0f, 0f, 0f), GameObject.Find ("Map").transform);
						}
					}
				}
			}
		}
		
	}

	GameObject initializeObject (GameObject gameobject) {
		if (objectType == placedObject.object_types.tile)
			gameobject = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Tile.prefab");
		else if (objectType == placedObject.object_types.wall)
			gameobject = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Wall.prefab");
		else if (objectType == placedObject.object_types.decal)
			gameobject = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/Decal.prefab");
		gameobject.GetComponent<SpriteRenderer> ().sprite = objectSprite;
		gameobject.GetComponent<placedObject> ().objectType = objectType;
		gameobject.name = objectSprite.name;
		return gameobject;
	}
/*
	void OnSceneGUI() {
		int controlId = GUIUtility.GetControlID (FocusType.Passive);
		Event e = Event.current;
		Ray ray = Camera.current.ScreenPointToRay (new Vector3 (e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
		Vector3 mousePos = ray.origin;

		if (e.isMouse && e.type == EventType.mouseDown) {
			GUIUtility.hotControl = controlId;
			e.Use ();

			GameObject gameObject;
			Transform prefab = grid.tilePrefab;

			if (prefab) {
				Undo.IncrementCurrentGroup ();
				gameObject = (GameObject)PrefabUtility.InstantiatePrefab (prefab.gameObject);
				Vector3 aligned = new Vector3 (mousePos.x, mousePos.y, 0f);
				gameObject.transform.position = aligned;
				Undo.RegisterCreatedObjectUndo (gameObject, "Create " + gameObject.name);
			}

			if(e.isMouse && e.type == EventType.MouseUp) {
				GUIUtility.hotControl = 0;
			}
		}

	}
*/
}