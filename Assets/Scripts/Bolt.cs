using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour {

	public float liveTime = 10.0f;
	public float speed = 10.0f;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, liveTime);
		rb = GetComponent<Rigidbody2D> ();
		Vector2 move_vector = new Vector2 (transform.up.x, transform.up.y);
		rb.velocity += move_vector * speed;
	}
}
