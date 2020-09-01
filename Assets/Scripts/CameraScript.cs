using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Transform player;
	public float speed = 0.5f;
	
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 newPosition = new Vector3(
			player.position.x,
			transform.position.y,
			transform.position.z
		);
		transform.position = Vector3.Lerp(transform.position, newPosition, speed);

	}
}
