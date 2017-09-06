using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placedObject : MonoBehaviour {

	public enum object_types {
		tile, decal, wall
	}

	public object_types objectType  = object_types.tile;

	void Start() {

	}
}
