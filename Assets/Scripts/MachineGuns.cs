using UnityEngine;
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

	public override void Init(MonoBehaviour parent) {
		FireTime = Time.time;
		FlashDisableTime = Time.time;
		firePos = 0;
	}

	public override void Fire(MonoBehaviour parent) {
		if (FireTime > Time.time)
			return;

		FireTime = Time.time + FireRate;
		FlashDisableTime = Time.time + GunFlashRate;
		
		firePos++;
		if (firePos >= HardPods.Length) firePos = 0;
		
		Transform fire_position = HardPods[firePos].transform;
		HardPods[firePos].light.enabled = true;
		
		GameObject bolt = (GameObject)MonoBehaviour.Instantiate(HardPods[firePos].Bolt, fire_position.position, fire_position.rotation);
		Rigidbody2D bolt_rb = bolt.GetComponent<Rigidbody2D>();
		Rigidbody2D parent_rb = parent.GetComponent<Rigidbody2D> ();
		bolt_rb.velocity = parent_rb.velocity.normalized;
	}

	public override void Update(MonoBehaviour parent) {
		disableLighs ();
	}

	private void disableLighs() {
		foreach (GunHardPod hard_pod in HardPods) {
			hard_pod.light.enabled = Time.time < FlashDisableTime;
		}
	}
}
