using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatusDooer : MonoBehaviour
{
    [Header("People Action Decider")]
    public GameObject PeopleActionDecider;

    [Header("Ship Part Objects")]
    public GameObject steeringPointerActual;
    public GameObject throttleLevelActual;
    public GameObject fuelLevel;

    public GameObject chain;
    public GameObject chainQuad;
    public GameObject startVertex;
    public GameObject endVertex;

    [Header("Constants")]
    [SerializeField]
    private float FUEL_USAGE = 0.05f;
    [SerializeField]
    private float THROTTLE_USAGE = 0.0005f;

    [SerializeField]
    private float STOKING_AMOUNT = 0.5f;
    [SerializeField]
    private float THROTTLE_AMOUNT = 0.05f;

    [Header("Ship Properties Info")]
    [Range(0, 1)]
    [SerializeField]
    private float currentThrottle = 0.0f;
    [Range(-45.0f, 45.0f)]
    [SerializeField]
    private float currentSteeringAngle = 0.0f;

    [SerializeField]
    private bool currentlyStoking = false;
    [SerializeField]
    private bool currentlyHooking = false;

    [Range(0.0f, 50.0f)]
    private float currentFuelLevel = 10.0f;

	[Range(0.0f, 500.0f)]
	private float stashedFuelLevel = 50.0f;


	private float altitude = 10.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentThrottle -= THROTTLE_USAGE;
        currentFuelLevel -= FUEL_USAGE;

		if (currentThrottle <= 0.0f)
			currentThrottle = 0.0f;

		if (currentFuelLevel <= 0.0f)
			currentFuelLevel += 10.0f;
		
        // Tick fuel usage
        fuelLevel.transform.localScale = new Vector3(1.0f, (currentFuelLevel / 50.0f), 1.0f);

        // Tick throttle usage
        throttleLevelActual.transform.localScale = new Vector3(1.0f, currentThrottle, 1.0f);

        steeringPointerActual.transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentSteeringAngle);

        Vector2 startVec = new Vector2(startVertex.transform.position.x, startVertex.transform.position.y);
        Vector2 endVec = new Vector2(endVertex.transform.position.x, endVertex.transform.position.y);

		altitude += getVerticalSpeed() * Time.deltaTime;
		altitude = Mathf.Clamp(altitude, 0, MaxAltitude());

		if (altitude < 1) {
			Debug.Log("CRASH");
		}

        chain.transform.position = startVertex.transform.position;
        //float angle = Mathf.Acos(Vector3.Dot(startVertex.transform.position, endVertex.transform.position));
        float angle = Vector2.Angle(Vector2.right, endVec - startVec) * ((endVec.y < startVec.y) ? -1.0f : 1.0f);

        chain.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
        chain.transform.Rotate(1.0f, 1.0f, 90.0f);


        float dist = Vector2.Distance(startVec, endVec);
        Vector2 offset = new Vector2(1.0f, dist * 6.25f);

        chainQuad.GetComponent<Renderer>().material.mainTextureScale = offset;
        chain.transform.localScale = new Vector3(1.0f, dist, 1.0f);
    }

    public void toggleHook()
    {
        currentlyHooking = !currentlyHooking;
    }

    public void stokeFire()
    {
		float delta = Mathf.Min(0.2f, stashedFuelLevel);
		stashedFuelLevel -= delta;
        currentFuelLevel += delta;
    }

	public void doPedalling(float workLevel){
		currentThrottle += workLevel;
	}

	public void doSteering(float workLevel, float targetAngle){
		float move = (targetAngle - currentSteeringAngle);
		move = Mathf.Sign(move) * Mathf.Min(workLevel * 45.0f/2.0f, Mathf.Abs(move));
		currentSteeringAngle += move;
	}
		
	public float getCurrentThrottle(){
		return currentThrottle;
	}

	public float getCurrentAngle(){
		return currentSteeringAngle;
	}

	public float getVerticalSpeed(){
		return Mathf.Sin(currentSteeringAngle * Mathf.Deg2Rad) * getCurrentThrottle();
    }

	public float GetAltitude(){
		return altitude;
	}

	public static float MaxAltitude(){
		return 100.0f;
	}
}
