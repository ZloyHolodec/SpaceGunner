using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

public class PlayerMovement : MonoBehaviour {
	[System.Serializable]
	public class GunHardPod {
		public Transform transform;
		public Light light;
	}

	public float speed;
	public float sidewaySpeed;
	public float TorqueSpeed;
	public GameObject Bolt;
	public GunHardPod[] HardPods;
	public float GunFlashRate;

	public ParticleSystem EngineParticles;
	public ParticleSystem LeftEngine;
	public ParticleSystem RightEngine;
	public float FireRate;
	
	private Rigidbody2D rb;
	private Vector3 mouseLastPosition;
	private float FireTime;
	private float FlashDisableTime;
	private int firePos;

	public void Start() {
		rb = GetComponent<Rigidbody2D> ();

		mouseLastPosition = Input.mousePosition;
		FireTime = Time.time + FireRate;
		FlashDisableTime = 0;
		firePos = 0;
	}

	public void FixedUpdate() {
		TurnToMouse ();
		MoveShip ();
	}

	public void Update() {
		Fire ();
		disableLighs ();
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

	private void disableLighs() {
		foreach (GunHardPod hard_pod in HardPods) {
			hard_pod.light.enabled = Time.time < FlashDisableTime;
		}
	}

	private void Fire() {
		if (Input.GetButton ("Fire1") && Time.time >= FireTime) {
			FireTime = Time.time + FireRate;
			FlashDisableTime = Time.time + GunFlashRate;

			firePos++;
			if (firePos >= HardPods.Length) firePos = 0;

			Transform fire_position = HardPods[firePos].transform;
			HardPods[firePos].light.enabled = true;

			GameObject bolt = (GameObject)Instantiate(Bolt, fire_position.position, fire_position.rotation);
			Rigidbody2D bolt_rb = bolt.GetComponent<Rigidbody2D>();
			bolt_rb.velocity = rb.velocity.normalized;
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
