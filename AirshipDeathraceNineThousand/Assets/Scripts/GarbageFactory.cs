using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageFactory : MonoBehaviour {

	[Header("House spawning")]
	protected float houseDistance = 0;
	public float houseMinSpacing = 3;
	public float houseExpectedSpacing = 5;
	public float houseAltitude = 1;
	public GameObject aHouse;

	[Header("Links")]
	public Camera sceneCamera;
	public GameObject shipStatusDooer;
	ShipStatusDooer ship;


	// Use this for initialization
	void Start () {
		ship = shipStatusDooer.GetComponent<ShipStatusDooer>();
	}
	
	// Update is called once per frame
	void Update () {
		// 
		houseDistance += ship.getCurrentThrottle() * Time.deltaTime;


		if (houseDistance > houseMinSpacing) {
			if (Mathf.Exp (houseDistance - houseExpectedSpacing) / 2 > Random.value) {
				GameObject newHouse = Instantiate(aHouse);

				PrizeMind houseMind = newHouse.GetComponent<PrizeMind> ();
				houseMind.shipStatusDooer = shipStatusDooer;
				houseMind.sceneCamera = sceneCamera;
				houseMind.altitude = houseAltitude;

				newHouse.transform.position = new Vector3(sceneCamera.aspect * sceneCamera.orthographicSize + newHouse.GetComponent<SpriteRenderer> ().bounds.size.x, 0, 0);
				houseDistance = 0;
			}
		}



	}
}
