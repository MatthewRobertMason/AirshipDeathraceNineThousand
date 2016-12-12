using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageFactory : MonoBehaviour {

	[Header("House spawning")]
	public float houseMinSpacing = 3;
	public float houseExpectedSpacing = 5;
	public float houseAltitude = 1;
	public GameObject aHouse;
	protected float houseDistance = 0;

	[Header("Small cloud spawning")]
	public float cloudMinSpacing = 0;
	public float cloudExpectedSpacing = 1;
	public float cloudMinAltitude = 4;
	public float cloudMaxAltitude = 20;
	public GameObject aCloud1;
	public GameObject aCloud2;
	protected float cloudDistance = 0;

	[Header("Big cloud spawning")]
	public float bigCloudMinSpacing = 1;
	public float bigCloudExpectedSpacing = 2f;
	public float bigCloudMinAltitude = 16;
	public float bigCloudMaxAltitude = 60;
	public GameObject aBigCloud;
	protected float bigCloudDistance = 0;

	[Header("Village spawning")]
	public float villageMinSpacing = 10;
	public float villageExpectedSpacing = 15f;
	public float villageMinAltitude = 15;
	public float villageMaxAltitude = 40;
	public GameObject aVillage;
	public GameObject bVillage;
	public GameObject cVillage;
	protected float villageDistance = 0;


	[Header("Links")]
	public Camera sceneCamera;
	public GameObject shipStatusDooer;
	ShipStatusDooer ship;


	// Use this for initialization
	void Start () {
		ship = shipStatusDooer.GetComponent<ShipStatusDooer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// 
		houseDistance += ship.getCurrentThrottle() * Time.fixedDeltaTime;
		cloudDistance += ship.getCurrentThrottle() * Time.fixedDeltaTime;
		bigCloudDistance += ship.getCurrentThrottle() * Time.fixedDeltaTime;
		villageDistance += ship.getCurrentThrottle() * Time.fixedDeltaTime;

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

		if (bigCloudDistance > bigCloudMinSpacing) {
			if (Mathf.Exp (bigCloudDistance - bigCloudExpectedSpacing) / 2 > Random.value) {
				GameObject newCloud = Instantiate(aBigCloud);

				SmallestCloud cloudMind = newCloud.GetComponent<SmallestCloud> ();
				cloudMind.shipStatusDooer = shipStatusDooer;
				cloudMind.sceneCamera = sceneCamera;
				cloudMind.altitude = bigCloudMinAltitude + (bigCloudMaxAltitude - bigCloudMinAltitude) * Random.value;

				newCloud.transform.position = new Vector3(sceneCamera.aspect * sceneCamera.orthographicSize + newCloud.GetComponent<SpriteRenderer> ().bounds.size.x, 0, 0);
				bigCloudDistance = 0;
			}
		}

		if (villageDistance > villageMinSpacing) {
			if (Mathf.Exp (villageDistance - villageExpectedSpacing) / 2 > Random.value) {
				GameObject newVillage;
				if(Random.value < 0.333)
					newVillage = Instantiate(aVillage);
				else if(Random.value < 0.666)
					newVillage = Instantiate(bVillage);
				else
					newVillage = Instantiate(cVillage);

				SmallestCloud cloudMind = newVillage.GetComponent<SmallestCloud> ();
				cloudMind.shipStatusDooer = shipStatusDooer;
				cloudMind.sceneCamera = sceneCamera;
				cloudMind.altitude = villageMinAltitude + (villageMaxAltitude - villageMinAltitude) * Random.value;

				newVillage.transform.position = new Vector3(sceneCamera.aspect * sceneCamera.orthographicSize + newVillage.GetComponent<SpriteRenderer> ().bounds.size.x, 0, 0);
				villageDistance = 0;
			}
		}
	}
}
