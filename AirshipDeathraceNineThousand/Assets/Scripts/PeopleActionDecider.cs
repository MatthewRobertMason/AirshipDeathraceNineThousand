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

	private Queue<Task> TaskList;
	private HashSet<Task> ActiveTasks;


    public GameObject idealSteeringPointer;
    public GameObject idealThrottleLevel;

    public GameObject shipStatusDooer;

<<<<<<< HEAD
    public GameObject hookLever;    
=======
	// Use this for initialization
	void Start ()
	{
		TaskList = new Queue<Task>();
		ActiveTasks = new HashSet<Task> ();
	}
>>>>>>> 0fc686ce7c2610dd37ec93d6e6de316481c22c63

    public void ThrottleUp()
    {
        if (throttleIdeal < 1.0f - 0.025f)
            throttleIdeal += 0.025f;
        
        idealThrottleLevel.transform.localPosition = new Vector3(0.0f, (throttleIdeal-0.5f), 0.0f);

		AddJob (Task.Throttle);
    }

    public void ThrottleDown()
    {
<<<<<<< HEAD
        if (throttleIdeal > 0.025f)
            throttleIdeal -= 0.025f;
        
        idealThrottleLevel.transform.localPosition = new Vector3(0.0f, (throttleIdeal - 0.5f), 0.0f);

        if (TaskList.Contains(Task.Throttle))
            TaskList.Add(Task.Throttle);
=======
        if (throttleIdeal >= 0.05f)
            throttleIdeal -= 0.05f;
		AddJob (Task.Throttle);
>>>>>>> 0fc686ce7c2610dd37ec93d6e6de316481c22c63
    }

    public void SteerUp()
    {
        if (steerAngleIdeal <= 44.00f)
            steerAngleIdeal += 1.00f;

        idealSteeringPointer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, steerAngleIdeal);

		AddJob (Task.Steer);
	}

    public void SteerDown()
    {
        if (steerAngleIdeal >= -44.00f)
            steerAngleIdeal -= 1.00f;

        idealSteeringPointer.transform.rotation = Quaternion.Euler(0.0f, 0.0f, steerAngleIdeal);

		AddJob (Task.Steer);
	}

    public void GoHooking()
    {
        hooking = !hooking;
		AddJob (Task.Hook);
	}

    public void StokeYourself()
    {
        stoking = !stoking;
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

    /*
	// Update is called once per frame
	void Update ()
    {
		This will require access to the ship manager (Dance commander?) to pull the current values from
	}
    */
}
