using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

[System.Serializable]
public class GunHardPod  {
	public Transform transform;
	public Light light;	
	public GameObject Bolt;
	
	public void Fire(Rigidbody2D rb) {
		Transform fire_position = transform;
		light.enabled = true;

		GameObject bolt = (GameObject)MonoBehaviour.Instantiate (Bolt, fire_position.position, fire_position.rotation);
		Rigidbody2D bolt_rb = bolt.GetComponent<Rigidbody2D> ();
		bolt_rb.velocity = rb.velocity.normalized;
	}
}
