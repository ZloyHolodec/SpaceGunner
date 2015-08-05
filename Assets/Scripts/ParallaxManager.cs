using UnityEngine;

public class ParallaxManager : MonoBehaviour {
	
	public GameObject backgroundStar;
	public int starsCount;
	public float MinZ;
	public float MaxZ;
	public int spriteSize;

	private GameObject[] Stars;
	private Vector3 oldCameraPos;

	// Use this for initialization
	void Start () {
		initStars ();
		oldCameraPos = Camera.main.transform.position;
	}

	void Update () {
		moveStars ();
		oldCameraPos = Camera.main.transform.position;
	}

	private void moveStars() {
		Vector3 cameraPos = Camera.main.transform.position;
		Vector3 delta = oldCameraPos - cameraPos;

		Vector3 min_pos = new Vector3 (-spriteSize, -spriteSize, 0);
		Vector3 max_pos = new Vector3 (Screen.width + spriteSize, Screen.height + spriteSize);
		min_pos = Camera.main.ScreenToWorldPoint (min_pos);
		max_pos = Camera.main.ScreenToWorldPoint (max_pos);

		foreach (GameObject star in Stars) {
			Vector3 star_pos = star.transform.position + (delta * star.transform.position.z);
			float x = star_pos.x;
			float y = star_pos.y;
			if (x < min_pos.x)
				x = max_pos.x;
			if (x > max_pos.x)
				x = min_pos.x;
			if (y < min_pos.y)
				y = max_pos.y;
			if (y > max_pos.y)
				y = min_pos.y;

			star_pos.Set(x, y, star_pos.z);
	
			star.transform.position = star_pos;
		}
	}

	private void initStars() {
		Stars = new GameObject[starsCount];
		for (int i = 0; i < Stars.Length; i++) {
			//Camera.main.ScreenToWorldPoint
			float x = (Screen.width + spriteSize) * Random.value;
			float y = (Screen.height + spriteSize) * Random.value;
			float z = Random.Range(MinZ, MaxZ);
 	
			Vector3 star_pos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, z));
			star_pos.z = z;

			Stars[i] = (GameObject)Instantiate(backgroundStar, star_pos, backgroundStar.transform.rotation);
			float scale = MinZ + (MaxZ - MinZ) * (z / MaxZ);
			Stars[i].transform.localScale = new Vector3(scale, scale, scale);
		}
	}
}