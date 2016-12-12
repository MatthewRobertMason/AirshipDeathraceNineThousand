using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallestCloud : MonoBehaviour {

	public GameObject shipStatusDooer;
	public Camera sceneCamera;
	protected ShipStatusDooer ship;
	public float speedMultiplier = 1f;
	public float altitude = 10.0f;

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
		pos += new Vector3(-ship.getCurrentThrottle() * speedMultiplier * Time.deltaTime, 0, 0);
		pos.y = getViewHeight();

		transform.position = pos;


		if (transform.position.x < -sceneCamera.aspect * sceneCamera.orthographicSize - GetComponent<SpriteRenderer> ().bounds.size.x){
			Destroy(this.gameObject);
		}
	}
}
