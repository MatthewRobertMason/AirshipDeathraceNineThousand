using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatusDooer : MonoBehaviour
{
    public GameObject PeopleActionDecider;
    public GameObject steeringPointerActual;
    public GameObject throttleLevelActual;

    private float currentThrottle = 0.0f;
    private float currentSteeringAngle = 0.0f;
    private bool currentlyStoking = false;
    private bool currentlyHooking = false;

    private float currentFuelLevel = 10.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentThrottle <= 0.0f)
            currentThrottle += 0.5f;

        if (currentFuelLevel <= 0.0f)
            currentFuelLevel += 10.0f;

        currentThrottle -= 0.0005f;
        currentFuelLevel -= 0.05f;
    }
}
