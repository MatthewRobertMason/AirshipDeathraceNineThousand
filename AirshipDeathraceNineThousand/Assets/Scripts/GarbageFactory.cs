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

	[Header("Small cloud spawning")]
	protected float cloudDistance = 0;
	public float cloudMinSpacing = 0;
	public float cloudExpectedSpacing = 1;
	public float cloudMinAltitude = 4;
	public float cloudMaxAltitude = 20;
	public GameObject aCloud1;
	public GameObject aCloud2;

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
		cloudDistance += ship.getCurrentThrottle() * Time.deltaTime;

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

		if (cloudDistance > cloudMinSpacing) {
			if (Mathf.Exp (cloudDistance - cloudExpectedSpacing) / 2 > Random.value) {
				GameObject newCloud;
				if(Random.value < 0.5)
					newCloud = Instantiate(aCloud1);
				else
					newCloud = Instantiate(aCloud2);

				SmallestCloud cloudMind = newCloud.GetComponent<SmallestCloud> ();
				cloudMind.shipStatusDooer = shipStatusDooer;
				cloudMind.sceneCamera = sceneCamera;
				cloudMind.altitude = cloudMinAltitude + (cloudMaxAltitude - cloudMinAltitude) * Random.value;

				newCloud.transform.position = new Vector3(sceneCamera.aspect * sceneCamera.orthographicSize + newCloud.GetComponent<SpriteRenderer> ().bounds.size.x, 0, 0);
				cloudDistance = 0;
			}
		}

	}
}
