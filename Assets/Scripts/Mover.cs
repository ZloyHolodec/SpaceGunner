using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

[System.Serializable]
public class Mover {
	public ParticleSystem LeftEngine;
	public ParticleSystem RightEngine;
	public ParticleSystem MainEngine;
	public float Speed;
	public float SidewaySpeed;
	public float TorqueSpeed;

	public bool forward;
	public bool rightMove;
	public bool leftMove;

	private MonoBehaviour parent;
	private Rigidbody2D parentRB;

	private Vector3 lastMoveVector;

	public void Init(MonoBehaviour parent) {
		this.parent = parent;
		parentRB = parent.GetComponent<Rigidbody2D> ();
		forward = false;
		rightMove = false;
		leftMove = false;
		lastMoveVector = parent.transform.up;
	}
	
	public void Forward() {
		forward = true;
		parentRB.AddForce (parent.transform.up * Speed * Time.deltaTime);
	}

	public void SideMove(float direction) {
		if (direction == 0)
			return;
		if (direction < 0)
			rightMove = true;
		if (direction > 0)
			leftMove = true;

		parentRB.AddForce(parent.transform.right * direction * SidewaySpeed * Time.deltaTime);
	}

	public void TurnToScreenPosition(Vector3 position) {
		float x_c = Screen.width / 2.0f;
		float y_c = Screen.height / 2.0f;

		lastMoveVector = Vector3.Lerp (lastMoveVector, position, TorqueSpeed * Time.deltaTime);

		float x_diff = lastMoveVector.x - x_c;
		float y_diff = lastMoveVector.y - y_c;
		float angle = Mathf.Atan2(y_diff, x_diff) * Mathf.Rad2Deg - 90.0f;
		
		parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	public void Update() {
		MainEngine.enableEmission = forward;
		RightEngine.enableEmission = rightMove;
		LeftEngine.enableEmission = leftMove;

		forward = false;
		rightMove = false;
		leftMove = false;
	}
}