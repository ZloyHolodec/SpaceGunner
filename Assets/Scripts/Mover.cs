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

	private MonoBehaviour parent;
	private Rigidbody2D parentRB;
	private bool forward;
	private bool rightMove;
	private bool leftMove;
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
		parentRB.AddForce(parent.transform.right * direction * SidewaySpeed * Time.deltaTime);
		rightMove = direction < 0;
		leftMove = direction > 0;
	}

	public void TurnTo(Vector3 position) {
		lastMoveVector = Vector3.Lerp (lastMoveVector, position, TorqueSpeed * Time.deltaTime);

		float x_diff = lastMoveVector.x - parent.transform.position.x;
		float y_diff = lastMoveVector.y - parent.transform.position.y;
		float angle = Mathf.Atan2(y_diff, x_diff) * Mathf.Rad2Deg - 90.0f;
		
		parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	public void Update() {
		MainEngine.enableEmission = forward;
		RightEngine.enableEmission = rightMove;
		LeftEngine.enableEmission = leftMove;
	}
}
