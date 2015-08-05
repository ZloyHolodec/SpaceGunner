using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

public class PlayerMovement : MonoBehaviour {
	public float speed;
	public float sidewaySpeed;
	public float TorqueSpeed;

	public ParticleSystem EngineParticles;
	public ParticleSystem LeftEngine;
	public ParticleSystem RightEngine;

	public MachineGuns Guns;
	
	private Rigidbody2D rb;
	private Vector3 mouseLastPosition;
	public void Start() {
		rb = GetComponent<Rigidbody2D> ();

		mouseLastPosition = Input.mousePosition;
		Guns.Init (this);
	}

	public void FixedUpdate() {
		TurnToMouse ();
		MoveShip ();
	}

	public void Update() {
		Fire ();
		Guns.Update (this);
	}

	private void MoveShip() {
		if (Input.GetAxisRaw ("Vertical") > 0) {
			rb.AddForce (transform.up * speed * Time.deltaTime);
			EngineParticles.enableEmission = true;
		} else {
			EngineParticles.enableEmission = false;
		}

		float LeftRightAxis = Input.GetAxisRaw ("Horizontal");
		if (LeftRightAxis != 0) {
			rb.AddForce(transform.right * LeftRightAxis * sidewaySpeed * Time.deltaTime);
		}

		LeftEngine.enableEmission = LeftRightAxis > 0;
		RightEngine.enableEmission = LeftRightAxis < 0;
	}
	
	private void Fire() {
		if (Input.GetButton ("Fire1")) {
			Guns.Fire(this);
		}
	}

	private void TurnToMouse() {
		mouseLastPosition = Vector3.Lerp (mouseLastPosition, Input.mousePosition, TorqueSpeed * Time.deltaTime);
		
		Vector3 object_pos = Camera.main.WorldToScreenPoint(rb.position);
		float x_diff = mouseLastPosition.x - object_pos.x;
		float y_diff = mouseLastPosition.y - object_pos.y;
		float angle = Mathf.Atan2(y_diff, x_diff) * Mathf.Rad2Deg - 90.0f;
		
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}
