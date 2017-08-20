using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public float width = 512f;
	public float heigth = 256f;
	public float multiplier = 1f;

	public Color color = Color.white;

	void OnDrawGizmos() {
		width *= multiplier;
		heigth *= multiplier;
		Vector3 position = new Vector3 (0f, 0f, 0f);
		Gizmos.color = this.color;
	
		for (float x = position.x - 1000 * width; x < 1000 * width; x += width) {
			Gizmos.DrawLine(new Vector3 (x, position.y-heigth*100, 0), new Vector3(x+200*width, position.y+heigth*100, 0));
			Gizmos.DrawLine(new Vector3 (x, position.y+heigth*100, 0), new Vector3(x+200*width, position.y-heigth*100, 0));				
		}
		
	}
}
