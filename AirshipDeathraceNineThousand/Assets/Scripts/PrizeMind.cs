using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeMind : MonoBehaviour {

	public GameObject shipStatusDooer;
	public Camera sceneCamera;
	protected ShipStatusDooer ship;
	public float speedMultiplier = 1f;
	public float altitude = 1.0f;
	public bool hooked = false;

	// Use this for initialization
	void Start () {
		ship = shipStatusDooer.GetComponent<ShipStatusDooer> ();
	}

	float getViewHeight(){
		float altitudeDelta = altitude - ship.GetAltitude();
		return altitudeDelta;
	}

	// Update is called once per frame
	void Update () {
		if (!hooked) {
			Vector3 pos = transform.position;
			pos += new Vector3 (-ship.getCurrentThrottle () * speedMultiplier, 0, 0);
			pos.y = getViewHeight ();

			transform.position = pos;

			if (transform.position.x < -sceneCamera.aspect * sceneCamera.orthographicSize - GetComponent<SpriteRenderer> ().bounds.size.x){
				Destroy(this.gameObject);
			}
		}
	}

}
