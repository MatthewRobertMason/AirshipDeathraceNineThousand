using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipStatusDooer : MonoBehaviour
{
	[Header("Score")]
	public UnityEngine.UI.Text scoreForm;
	protected float score = 0;

    [Header("People Action Decider")]
    public GameObject PeopleActionDecider;

    [Header("Ship Part Objects")]
    public GameObject steeringPointerActual;
    public GameObject throttleLevelActual;
    public GameObject fuelLevel;
    public GameObject fuelReserve;
    public GameObject togWoggler;

    [Header("Hook Stuff")] 
    public GameObject Hook;
    private Vector3 hookInitial;
    [SerializeField]
    private GameObject hookDropPosition;
    [SerializeField]
    [Range(0.0f, 5.0f)]
    private float hookDropSpeed = 1.0f;

    [Header("Chain Stuff")]
    public GameObject chain;
    public GameObject chainQuad;
    public GameObject startVertex;
    public GameObject endVertex;

    [Header("Constants")]
    [SerializeField]
    private float FUEL_USAGE = 1f;
    [SerializeField]
    private float STOKING_AMOUNT = 10.0f;
    [SerializeField]
    private float FUEL_MAXIMUM = 50.0f;
    [SerializeField]
    private float RESERVE_MAXIMUM = 500.0f;
    [SerializeField]
    private float THROTTLE_USAGE = 0.1f;
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
    [SerializeField]
    private bool hookDeployed = false;
    [SerializeField]
    private bool hookReturned = false;

    [SerializeField]
    [Range(0.0f, 50.0f)]
    private float currentFuelLevel = 20.0f;

    [SerializeField]
    [Range(0.0f, 500.0f)]
	private float stashedFuelLevel = 50.0f;


	public float altitude = 10.0f;

    private AudioSource crashSfx;

    // Use this for initialization
    void Start ()
    {
        hookInitial = Hook.transform.position;
        AudioSource sfx = this.gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		currentThrottle -= THROTTLE_USAGE * Time.deltaTime;
		currentFuelLevel -= FUEL_USAGE * Time.deltaTime;

		if (currentThrottle <= 0.0f)
			currentThrottle = 0.0f;

		if (currentFuelLevel <= 0.0f)
			currentFuelLevel = 0.0f;
		
        // Tick fuel usage
        fuelLevel.transform.localScale = new Vector3(1.0f, (currentFuelLevel / FUEL_MAXIMUM), 1.0f);
        fuelReserve.transform.localScale = new Vector3(1.0f, (stashedFuelLevel / RESERVE_MAXIMUM), 1.0f);

        // Tick throttle usage
        throttleLevelActual.transform.localScale = new Vector3(1.0f, currentThrottle, 1.0f);

        steeringPointerActual.transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentSteeringAngle);

        

		altitude += getVerticalSpeed() * Time.deltaTime;
		altitude = Mathf.Clamp(altitude, 0, MaxAltitude());

		if (altitude < 1)
        {
            crashSfx.Play();
            SceneManager.LoadScene(1);
		}


        // ================== Chain Stuff
        Vector2 startVec = new Vector2(startVertex.transform.position.x, startVertex.transform.position.y);
        Vector2 endVec = new Vector2(endVertex.transform.position.x, endVertex.transform.position.y);

        chain.transform.position = startVertex.transform.position;

        float angle = Vector2.Angle(Vector2.right, endVec - startVec) * ((endVec.y < startVec.y) ? -1.0f : 1.0f);
        chain.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
        chain.transform.Rotate(1.0f, 1.0f, 90.0f);

        float dist = Vector2.Distance(startVec, endVec);
        Vector2 offset = new Vector2(1.0f, dist * 6.25f);
        chainQuad.GetComponent<Renderer>().material.mainTextureScale = offset;
        chain.transform.localScale = new Vector3(1.0f, dist, 1.0f); 

        // ================== Hooking Stuff
        float hookMoveDist = Time.deltaTime * hookDropSpeed;

        if (currentlyHooking && !hookDeployed)
            Hook.transform.position = Vector3.MoveTowards(Hook.transform.position, hookDropPosition.transform.position, hookMoveDist);
        
        if (Hook.transform.position == hookDropPosition.transform.position)
            hookDeployed = true;
        
        if (!currentlyHooking)
        {
            hookDeployed = false;
            Hook.transform.position = Vector3.MoveTowards(Hook.transform.position, hookInitial, hookMoveDist/2);
        }
        if (Hook.transform.position == hookInitial)
        {
            currentlyHooking = false;
        }

        if (Hook.transform.position == hookInitial)
            hookReturned = true;
        else
            hookReturned = false;

        togWoggler.transform.localScale.Set((currentlyHooking) ? -1.0f : 1.0f, 1.0f, 1.0f);

		// ====================
		// Score things

		score += Time.deltaTime * getCurrentThrottle() * GetAltitude();
		scoreForm.text = string.Format("{0:D}", (int)score);

        if (hookReturned)
        {
            LootGrabDooer lootGrabDooer = Hook.GetComponent<LootGrabDooer>();

            if (lootGrabDooer.hooked != null)
            {
                PrizeMind prizeMind = lootGrabDooer.hooked.GetComponent<PrizeMind>();

                switch (prizeMind.prizeType)
                {
                    case PrizeMind.PrizeType.Junk:
                        break;

                    case PrizeMind.PrizeType.Burnable:
                        stashedFuelLevel += prizeMind.prizeAmount;
                        if (stashedFuelLevel > RESERVE_MAXIMUM)
                            stashedFuelLevel = RESERVE_MAXIMUM;
                        break;

                    case PrizeMind.PrizeType.Points:
                        score += prizeMind.prizeAmount;
                        break;
                }

                Destroy(lootGrabDooer.hooked);
                lootGrabDooer.hooked = null;
            }
        }
    }

    public void toggleHook()
    {
        currentlyHooking = !currentlyHooking;
    }

    public void stokeFire()
    {
		float delta = Mathf.Min(STOKING_AMOUNT, stashedFuelLevel, (FUEL_MAXIMUM - currentFuelLevel));
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
		float furnaceEffect = (currentFuelLevel / FUEL_MAXIMUM - 0.05f);
		if (furnaceEffect < 0)
			furnaceEffect *= 25;
		else
			furnaceEffect *= 0.5f;

		float reserveEffect = (stashedFuelLevel / RESERVE_MAXIMUM) * 0.2f;

		return Mathf.Sin(currentSteeringAngle * Mathf.Deg2Rad) * getCurrentThrottle()  + furnaceEffect - reserveEffect;
    }

	public float GetAltitude(){
		return altitude;
	}

	public static float MaxAltitude(){
		return 100.0f;
	}
}
