using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallestCloud : MonoBehaviour {

	public GameObject shipStatusDooer;
	public Camera sceneCamera;
	protected ShipStatusDooer ship;
	public float speedMultiplier = 1f;

	// Use this for initialization
	void Start () {
		ship = shipStatusDooer.GetComponent<ShipStatusDooer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 move = new Vector3(-ship.getCurrentThrottle() * speedMultiplier, -ship.getVerticalSpeed(), 0);

		transform.position += move;

		if (!this.GetComponent<SpriteRenderer> ().isVisible) {
			transform.position = new Vector3(sceneCamera.aspect * 2, 0, transform.position.z);
		}
	}
}
