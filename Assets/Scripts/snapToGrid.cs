using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class snapToGrid : MonoBehaviour {
	
	public float tileWidth , tileHeigth;
	public Grid grid;

	void Update() {

		if (this.GetComponent<placedObject> ().objectType == placedObject.object_types.tile) {
			tileWidth = this.GetComponent<Renderer> ().bounds.size.x;	
			tileHeigth = this.GetComponent<Renderer> ().bounds.size.y;
		} else {
			tileWidth = grid.width;	
			tileHeigth = grid.heigth;
		}

		transform.position = Snap (transform.position);


	}

	Vector3 Snap(Vector3 localPosition) {
		// Calculate ratios for simple grid snap
		float xx = Mathf.Round(localPosition.y / tileHeigth - localPosition.x / tileWidth);
		float yy = Mathf.Round(localPosition.y / tileHeigth + localPosition.x / tileWidth);

		// Calculate grid aligned position from current position
		float x = (yy - xx) * 0.5f * tileWidth;
		float y = (yy + xx) * 0.5f * tileHeigth;

		return new Vector3(x, y, 0f);
	}


}
