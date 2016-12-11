﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallestCloud : MonoBehaviour {

	public GameObject shipStatusDooer;
	public Camera camera;
	protected ShipStatusDooer ship;
	public float speedMultiplier = 1f;

	// Use this for initialization
	void Start () {
		ship = shipStatusDooer.GetComponent<ShipStatusDooer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 move = new Vector3(-ship.getCurrentThrottle() * speedMultiplier, 0, 0);

		transform.position += move;

		if (!this.GetComponent<SpriteRenderer> ().isVisible) {
			transform.position = new Vector3(camera.aspect * 2, transform.position.y, transform.position.z);
		}
	}
}