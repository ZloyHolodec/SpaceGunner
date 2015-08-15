using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bolt : NetworkBehaviour {

	[SyncVar]
	public Quaternion rotation;

	public float liveTime = 10.0f;
	public float speed = 10.0f;
	
	private Rigidbody2D rb;
	private bool isFirstUpdate;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Vector2 move_vector = new Vector2 (transform.up.x, transform.up.y);
		rb.velocity += move_vector * speed;
		rb.velocity.Set (rb.velocity.x, rb.velocity.y);

		Destroy (this.gameObject, liveTime);

		this.transform.rotation = rotation;

		isFirstUpdate = true;
	}

	void Update() {
		if (isFirstUpdate) {
			this.transform.rotation = rotation;
			isFirstUpdate = false;
		}
	}  

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			PlayerMovement mover = other.gameObject.GetComponent<PlayerMovement>();
			mover.HealthPoints -= 10;
		}
		Destroy (this.gameObject);
	}
}
