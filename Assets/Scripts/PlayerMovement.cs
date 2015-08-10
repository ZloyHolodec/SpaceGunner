using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

public class PlayerMovement : MonoBehaviour {
	public Mover PlayerMover;
	public MachineGuns Guns;

	public void Start() {
		Guns.Init (this);
		PlayerMover.Init (this);
	}

	public void FixedUpdate() {
		TurnToMouse ();
		MoveShip ();
		PlayerMover.Update ();
	}

	public void Update() {
		Fire ();
		Guns.Update (this);
	}

	private void MoveShip() {
		if (Input.GetAxisRaw ("Vertical") > 0) {
			PlayerMover.Forward();
		}

		float LeftRightAxis = Input.GetAxisRaw ("Horizontal");
		if (LeftRightAxis != 0) {
			PlayerMover.SideMove(LeftRightAxis);
		}
	}
	
	private void Fire() {
		if (Input.GetButton ("Fire1")) {
			Guns.Fire(this);
		}
	}

	private void TurnToMouse() {
		Vector3 mouse_pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		PlayerMover.TurnTo (mouse_pos);
	}
}
