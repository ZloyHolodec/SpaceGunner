using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Runtime.Serialization;

public class PlayerMovement : NetworkBehaviour {
	public Mover PlayerMover;
	public MachineGuns Guns;

	[SyncVar]
	public float HealthPoints;
	[SyncVar]
	public bool isFiring;
	[SyncVar]
	public Vector3 TurnToPos;
	[SyncVar]
	public float ForwardMoveVal;
	[SyncVar]
	public float SideMoveVal;

	private GameObject HealthBar;

	public void Start() {
		HealthPoints = 100.0f;

		Guns.Init (this);
		PlayerMover.Init (this);
		if (isLocalPlayer) {
			GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
			CameraWalker walker = camera.GetComponent<CameraWalker> ();
			walker.player = this.transform;
			HealthBar = GameObject.FindGameObjectWithTag("HealthPoints");
		}

		isFiring = false;
		TurnToPos = Vector3.up;
	}

	public void FixedUpdate() {
		TurnToMouse ();
		if (isLocalPlayer) {
			float forward = Input.GetAxisRaw ("Vertical");
			float sideway = Input.GetAxisRaw ("Horizontal");
			if (forward != ForwardMoveVal || sideway != SideMoveVal) 
				CmdMoveShip (forward, sideway);
		}

		if (ForwardMoveVal != 0)
			PlayerMover.Forward ();
		if (SideMoveVal != 0)
			PlayerMover.SideMove (SideMoveVal);

		PlayerMover.Update ();
	}

	public void Update() {
		if (isLocalPlayer)
			Fire ();
		if (isFiring)
			Guns.Fire (this);
		Guns.Update (this);

		if (isLocalPlayer) {
			Slider slider = HealthBar.GetComponent<Slider>();
			slider.value = HealthPoints;
		}
	}
	
	private void Fire() {
		if (Input.GetButton ("Fire1") && !isFiring) {
			CmdFire (true);
		} else if (!Input.GetButton("Fire1") && isFiring) {
			CmdFire (false);
		}
	}

	private void TurnToMouse() {
		if (isLocalPlayer) {
			Vector3 mouse_pos = Input.mousePosition;
			CmdTurnPlayer (mouse_pos);
		}
		PlayerMover.TurnToScreenPosition (TurnToPos);
	}

	[Command(channel = 0)]
	void CmdFire(bool isFire) {
		isFiring = isFire;
	}
	
	[Command(channel = 0)]
	void CmdMoveShip(float forward, float sideway) {
		forward = Mathf.Clamp (forward, 0.0f, 1.0f);
		sideway = Mathf.Clamp (sideway, -1.0f, 1.0f);

		ForwardMoveVal = forward;
		SideMoveVal = sideway;
	}
	
	[Command(channel = 0)]
	void CmdTurnPlayer(Vector3 turn_to) {
		TurnToPos = turn_to;
	}
}
