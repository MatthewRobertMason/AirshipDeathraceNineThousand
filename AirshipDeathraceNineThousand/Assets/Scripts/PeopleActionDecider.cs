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
    [Range(-45, 45)]
    [SerializeField]
    private float steerAngleIdeal = 0.0f;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float lazynessAutoThrottle = 0.5f;

    private Queue<Task> TaskList;
	private HashSet<Task> ActiveTasks;
    
    public GameObject idealSteeringPointer;
    public GameObject idealThrottleLevel;

    public GameObject shipStatusDooer;

    private bool steerUpButtonPushed;
    private bool steerDownButtonPushed;
    private bool throttleUpButtonPushed;
    private bool throttleDownButtonPushed;

    public void SteerUpButtonDown()
    { steerUpButtonPushed = true; }

    public void SteerUpButtonUp()
    { steerUpButtonPushed = false; }

    public void SteerDownButtonDown()
    { steerDownButtonPushed = true; }

    public void SteerDownButtonUp()
    { steerDownButtonPushed = false; }

    public void ThrottleUpButtonDown()
    { throttleUpButtonPushed = true; }

    public void ThrottleUpButtonUp()
    { throttleUpButtonPushed = false; }

    public void ThrottleDownButtonDown()
    { throttleDownButtonPushed = true; }

    public void ThrottleDownButtonUp()
    { throttleDownButtonPushed = false; }

    // Use this for initialization
    void Start ()
	{
		TaskList = new Queue<Task>();
		ActiveTasks = new HashSet<Task> ();
	}

    public void ThrottleUp()
    {
		throttleIdeal = Mathf.Clamp01(throttleIdeal + Time.deltaTime);
        
        idealThrottleLevel.transform.localPosition = new Vector3(idealThrottleLevel.transform.localPosition.x, (throttleIdeal-0.5f), idealThrottleLevel.transform.localPosition.z);

		AddJob (Task.Throttle);
    }

    public void ThrottleDown()
    {
		throttleIdeal = Mathf.Clamp01(throttleIdeal - Time.deltaTime);
        
        idealThrottleLevel.transform.localPosition = new Vector3(idealThrottleLevel.transform.localPosition.x, (throttleIdeal - 0.5f), idealThrottleLevel.transform.localPosition.z);
        
		AddJob (Task.Throttle);
    }

	public float GetIdealThrottle(){
		return throttleIdeal;
	}

    public void SteerUp()
    {
		steerAngleIdeal = Mathf.Clamp(steerAngleIdeal + 15*Time.deltaTime, -44, 44);

        idealSteeringPointer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, steerAngleIdeal);

		AddJob (Task.Steer);
	}

    public void SteerDown()
    {
		steerAngleIdeal = Mathf.Clamp(steerAngleIdeal - 15*Time.deltaTime, -44, 44);

        idealSteeringPointer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, steerAngleIdeal);

		AddJob (Task.Steer);
	}

	public float getIdealAngle(){
		return steerAngleIdeal;
	}

    public void GoHooking()
    {
		AddJob (Task.Hook);
	}

    public void StokeYourself()
    {
		AddJob (Task.Stoke);
    }

	protected void AddJob(Task task){
		if (!TaskList.Contains(task) && !ActiveTasks.Contains(task))
			TaskList.Enqueue(task);
	}

	// Called by the crew members to get a job.
	public Task GetJob()
	{
		if (TaskList.Count > 0) {
			Task selectedTask = TaskList.Dequeue ();
			ActiveTasks.Add (selectedTask);
			return selectedTask;
		}
		return Task.Idle;
	}

	public void ReleaseTask(Task task){
		ActiveTasks.Remove (task);
	}

    
	// Update is called once per frame
	void Update ()
    {
        if (steerUpButtonPushed)
        {
            SteerUp();
        }

        if (steerDownButtonPushed)
        {
            SteerDown();
        }

        if (throttleUpButtonPushed)
        {
            ThrottleUp();
        }

        if (throttleDownButtonPushed)
        {
            ThrottleDown();
        }

        if (throttleIdeal - shipStatusDooer.GetComponent<ShipStatusDooer>().getCurrentThrottle() > (throttleIdeal * lazynessAutoThrottle))
        {
            AddJob(Task.Throttle);
        }
    }
}
