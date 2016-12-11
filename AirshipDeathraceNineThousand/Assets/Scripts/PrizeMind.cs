using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeMind : MonoBehaviour {

	public GameObject shipStatusDooer;
	public Camera sceneCamera;
	protected ShipStatusDooer ship;
	public float speedMultiplier = 1f;
	public float altitude = 1.0f;

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
		Vector3 pos = transform.position;
		pos += new Vector3(-ship.getCurrentThrottle() * speedMultiplier, 0, 0);
		pos.y = getViewHeight();

		transform.position = pos;


		if (!this.GetComponent<SpriteRenderer> ().isVisible) {
			transform.position = new Vector3(sceneCamera.aspect * 2, getViewHeight(), transform.position.z);
		}
	}

}
