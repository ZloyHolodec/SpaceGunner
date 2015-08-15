using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraWalker : MonoBehaviour {

	public float speed = 10.0f;
	
	public Transform player;
	
	void FixedUpdate () {
		Vector3 new_position = new Vector3 (player.position.x, player.position.y, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, new_position, speed * Time.deltaTime);
	}
}
