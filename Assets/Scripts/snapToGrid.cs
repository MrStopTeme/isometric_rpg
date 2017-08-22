using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class snapToGrid : MonoBehaviour {
	
	public int xMultiplier = 128, yMultiplier = 128;

	void Update () {
		
		int localX = Mathf.RoundToInt(transform.position.x * 100);
		int localY = Mathf.RoundToInt(transform.position.y * 100);
	

		float newX = getClosest (xMultiplier, localX) / 100f;
		float newY = getClosest (yMultiplier, localY) / 100f;
		
		transform.position = new Vector3 (newX, newY, 0f);
	}

	int getClosest(int multiplier, int value) {
		int closest = value - (value % multiplier);
		if (value - closest < closest + multiplier - value)
			return closest;
		else
			return closest + multiplier;

	}
}
