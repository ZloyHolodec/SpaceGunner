using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class MachineGuns : CanShoot {
	public float FireRate;
	public float GunFlashRate;
	
	private float FireTime;
	private float FlashDisableTime;
	private int firePos;


	public MachineGuns() {
	}

	public override void Init(NetworkBehaviour parent) {
		FireTime = Time.time;
		FlashDisableTime = Time.time;
		firePos = 0;

	}

	public override void Fire(NetworkBehaviour parent) {
		if (FireTime > Time.time)
			return;

		FireTime = Time.time + FireRate;
		FlashDisableTime = Time.time + GunFlashRate;
		firePos++;
		if (firePos >= HardPods.Length) firePos = 0;
		
		Transform fire_position = HardPods[firePos].transform;
		HardPods[firePos].light.enabled = true;

		GameObject bolt = MonoBehaviour.Instantiate(HardPods[firePos].Bolt, fire_position.position, fire_position.rotation) as GameObject;
		Bolt bolt_manager = bolt.GetComponent<Bolt> ();
		bolt_manager.rotation = fire_position.rotation;
		//NetworkServer.Spawn (bolt);
	}

	public override void Update(NetworkBehaviour parent) {			
		disableLighs ();
	}

	private void disableLighs() {
		foreach (GunHardPod hard_pod in HardPods) {
			hard_pod.light.enabled = Time.time < FlashDisableTime;
		}
	}
}