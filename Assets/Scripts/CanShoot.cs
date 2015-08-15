using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CanShoot {
	public GunHardPod[] HardPods;

	public virtual void Fire(NetworkBehaviour parent) {
	}

	public virtual void Update(NetworkBehaviour parent) {
	}

	public virtual void Init(NetworkBehaviour parent) {
	}
}