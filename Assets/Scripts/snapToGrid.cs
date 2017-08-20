using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class snapToGrid : MonoBehaviour {
	
	public int xMultiplier = 128, yMultiplier = 128;

	void Update () {
		
		int localX = Mathf.RoundToInt(transform.position.x * 100);
		int localY = Mathf.RoundToInt(transform.position.y * 100);
		transform.position = new Vector3 (getClosest (xMultiplier, localX) / 100f, getClosest (yMultiplier, localY) / 100f, 0f);
	}

	int getClosest(int multiplier, int value) {
		int closest = value - value % multiplier;
		if (value - closest < closest + multiplier - value)
			return closest;
		else
			return closest + multiplier;
		

	}
}
