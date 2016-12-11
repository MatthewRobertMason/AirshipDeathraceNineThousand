using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatusDooer : MonoBehaviour
{
    public GameObject PeopleActionDecider;
    public GameObject steeringPointerActual;
    public GameObject throttleLevelActual;
    public GameObject fuelLevel;

    [SerializeField]
    private float FUEL_USAGE = 0.05f;
    [SerializeField]
    private float THROTTLE_USAGE = 0.0005f;

    [SerializeField]
    private float STOKING_AMOUNT = 0.5f;
    [SerializeField]
    private float THROTTLE_AMOUNT = 0.05f;
    
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
		return Mathf.Sin(currentSteeringAngle * 3.14f/180f) * getCurrentThrottle();
	}
}
