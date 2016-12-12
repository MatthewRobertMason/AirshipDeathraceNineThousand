using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandHolder : MonoBehaviour {

	public GameObject shipStatusDooer;
	public Camera sceneCamera;
	protected ShipStatusDooer ship;

	public float speedMultiplier;
	public float altitude;

	private Vector2 offset;

	// Use this for initialization
	void Start () {
		ship = shipStatusDooer.GetComponent<ShipStatusDooer>();
		offset = new Vector2 (0, 0);
	}

	float getViewHeight(){
		float altitudeDelta = altitude - ship.GetAltitude();
		return altitudeDelta;
	}

	// Update is called once per frame
	void Update () {
		// Slide the texture along horizontally to match the speed set by speedMultiplyer
		// (should be the same as the speedMultiplier given to anything resting on the ground)
		float delta = ship.getCurrentThrottle () * speedMultiplier * Time.deltaTime;

		// Scale it by the width of the texture so it is in world space
		offset += new Vector2(delta/this.transform.localScale.x, 0);
		this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);

		// Pin it at the correct altitude relative to the ship.
		Vector3 pos = transform.position;
		pos.y = getViewHeight();
		transform.position = pos;

	}
}
