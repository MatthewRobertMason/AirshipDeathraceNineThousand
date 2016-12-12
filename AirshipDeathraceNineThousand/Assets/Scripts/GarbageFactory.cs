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

	[Header("Small Tree spawning")]
	public float smallTreeMinSpacing = 1;
	public float smallTreeExpectedSpacing = 1;
	public float smallTreeMinAltitude = 14;
	public float smallTreeMaxAltitude = 9;
	public GameObject aSmallTree;
	protected float smallTreeDistance = 0;
    
    [Header("Balloon Loot spawning")]
    public GameObject aMoneyBags;
    public float moneyBagsMinSpacing = 30;
    public float moneyBagsExpectedSpacing = 1;
    public float moneyBagsMinAltitude = 14;
    public float moneyBagsMaxAltitude = 9;
    protected float moneyBagsDistance = 0;

    public GameObject aCat;
    public float catMinSpacing = 100;
    public float catExpectedSpacing = 1;
    public float catMinAltitude = 14;
    public float catMaxAltitude = 9;
    protected float catDistance = 0;

    public GameObject aWood;
    public float woodMinSpacing = 10;
    public float woodExpectedSpacing = 1;
    public float woodMinAltitude = 14;
    public float woodMaxAltitude = 9;
    protected float woodDistance = 0;

    public GameObject aJunk;
    public float junkMinSpacing = 10;
    public float junkExpectedSpacing = 1;
    public float junkMinAltitude = 14;
    public float junkMaxAltitude = 9;
    protected float junkDistance = 0;
    
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
		smallTreeDistance += ship.getCurrentThrottle() * Time.fixedDeltaTime;

        moneyBagsDistance += ship.getCurrentThrottle() * Time.fixedDeltaTime;
        catDistance += ship.getCurrentThrottle() * Time.fixedDeltaTime;
        woodDistance += ship.getCurrentThrottle() * Time.fixedDeltaTime;
        junkDistance += ship.getCurrentThrottle() * Time.fixedDeltaTime;
        
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

		if (smallTreeDistance > smallTreeMinSpacing) {
			if (Mathf.Exp (smallTreeDistance - smallTreeExpectedSpacing) / 2 > Random.value) {
				GameObject newCloud = Instantiate(aSmallTree);

				SmallestCloud cloudMind = newCloud.GetComponent<SmallestCloud> ();
				cloudMind.shipStatusDooer = shipStatusDooer;
				cloudMind.sceneCamera = sceneCamera;
				cloudMind.altitude = smallTreeMinAltitude + (smallTreeMaxAltitude - smallTreeMinAltitude) * Random.value;

				newCloud.transform.position = new Vector3(sceneCamera.aspect * sceneCamera.orthographicSize + newCloud.GetComponent<SpriteRenderer> ().bounds.size.x, 0, 0);
				smallTreeDistance = 0;
			}
		}

        if (moneyBagsDistance > moneyBagsMinSpacing)
        {
            if (Mathf.Exp(moneyBagsDistance - moneyBagsExpectedSpacing) / 2 > Random.value)
            {
                GameObject newMoneyBags = Instantiate(aMoneyBags);

                PrizeMind moneyBagsMind = newMoneyBags.GetComponent<PrizeMind>();
                moneyBagsMind.shipStatusDooer = shipStatusDooer;
                moneyBagsMind.sceneCamera = sceneCamera;
                moneyBagsMind.altitude = moneyBagsMinAltitude + (moneyBagsMaxAltitude - moneyBagsMinAltitude) * Random.value; ;

                newMoneyBags.transform.position = new Vector3(sceneCamera.aspect * sceneCamera.orthographicSize + newMoneyBags.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
                moneyBagsDistance = 0;
            }
        }

        if (catDistance > catMinSpacing)
        {
            if (Mathf.Exp(catDistance - catExpectedSpacing) / 2 > Random.value)
            {
                GameObject newCat = Instantiate(aCat);

                PrizeMind catMind = newCat.GetComponent<PrizeMind>();
                catMind.shipStatusDooer = shipStatusDooer;
                catMind.sceneCamera = sceneCamera;
                catMind.altitude = catMinAltitude + (catMaxAltitude - catMinAltitude) * Random.value; ;

                newCat.transform.position = new Vector3(sceneCamera.aspect * sceneCamera.orthographicSize + newCat.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
                catDistance = 0;
            }
        }

        if (woodDistance > woodMinSpacing)
        {
            if (Mathf.Exp(woodDistance - woodExpectedSpacing) / 2 > Random.value)
            {
                GameObject newWood = Instantiate(aWood);

                PrizeMind woodMind = newWood.GetComponent<PrizeMind>();
                woodMind.shipStatusDooer = shipStatusDooer;
                woodMind.sceneCamera = sceneCamera;
                woodMind.altitude = woodMinAltitude + (woodMaxAltitude - woodMinAltitude) * Random.value; ;

                newWood.transform.position = new Vector3(sceneCamera.aspect * sceneCamera.orthographicSize + newWood.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
                woodDistance = 0;
            }
        }

        if (junkDistance > junkMinSpacing)
        {
            if (Mathf.Exp(junkDistance - houseExpectedSpacing) / 2 > Random.value)
            {
                GameObject newJunk = Instantiate(aJunk);

                PrizeMind junkMind = newJunk.GetComponent<PrizeMind>();
                junkMind.shipStatusDooer = shipStatusDooer;
                junkMind.sceneCamera = sceneCamera;
                junkMind.altitude = junkMinAltitude + (junkMaxAltitude - junkMinAltitude) * Random.value; ;

                newJunk.transform.position = new Vector3(sceneCamera.aspect * sceneCamera.orthographicSize + newJunk.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
                junkDistance = 0;
            }
        }
    }
}
