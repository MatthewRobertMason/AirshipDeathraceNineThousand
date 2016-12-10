using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleActionDecider : MonoBehaviour
{
    public enum Task
    {
        Idle,
        Steer,
        Throttle,
        Stoke,
        Hook
    }

    [Range(0, 1)]
    [SerializeField]
    private float throttleIdeal = 0.5f;
    [Range(0, 1)]
    [SerializeField]
    private float throttleCurrent = 0.5f;

    [Range(-45, 45)]
    [SerializeField]
    private float steerAngleIdeal = 0.0f;
    [Range(-45, 45)]
    [SerializeField]
    private float steerAngleCurrent = 0.0f;

    [SerializeField]
    private bool hooking;
    [SerializeField]
    private bool stoking;

    private List<Task> TaskList;


    public GameObject idealSteeringPointer;
    public GameObject idealThrottleLevel;

    public GameObject shipStatusDooer;

    public GameObject hookLever;    

    public void ThrottleUp()
    {
        if (throttleIdeal < 1.0f - 0.025f)
            throttleIdeal += 0.025f;
        
        idealThrottleLevel.transform.localPosition = new Vector3(0.0f, (throttleIdeal-0.5f), 0.0f);

        if (TaskList.Contains(Task.Throttle))
            TaskList.Add(Task.Throttle);
    }

    public void ThrottleDown()
    {
        if (throttleIdeal > 0.025f)
            throttleIdeal -= 0.025f;
        
        idealThrottleLevel.transform.localPosition = new Vector3(0.0f, (throttleIdeal - 0.5f), 0.0f);

        if (TaskList.Contains(Task.Throttle))
            TaskList.Add(Task.Throttle);
    }

    public void SteerUp()
    {
        if (steerAngleIdeal <= 44.00f)
            steerAngleIdeal += 1.00f;

        idealSteeringPointer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, steerAngleIdeal);

        if (TaskList.Contains(Task.Steer))
            TaskList.Add(Task.Steer);
    }

    public void SteerDown()
    {
        if (steerAngleIdeal >= -44.00f)
            steerAngleIdeal -= 1.00f;

        idealSteeringPointer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, steerAngleIdeal);

        if (TaskList.Contains(Task.Steer))
            TaskList.Add(Task.Steer);
    }

    public void GoHooking()
    {
        hooking = !hooking;
        if (TaskList.Contains(Task.Hook))
            TaskList.Add(Task.Hook);
    }

    public void StokeYourself()
    {
        stoking = !stoking;
        if (TaskList.Contains(Task.Stoke))
            TaskList.Add(Task.Stoke);
    }

    
	// Use this for initialization
	void Start ()
    {
        TaskList = new List<Task>();
    }
    
    /*
	// Update is called once per frame
	void Update ()
    {
		This will require access to the ship manager (Dance commander?) to pull the current values from
	}
    */
}
