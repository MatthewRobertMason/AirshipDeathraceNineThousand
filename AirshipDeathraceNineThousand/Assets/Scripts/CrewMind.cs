using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMind : MonoBehaviour {

	public PeopleActionDecider.Task currentTask = PeopleActionDecider.Task.Idle;
	public float waitTime = 0;
	public float speed = 1;
	public float strength = 0.08f;

	public GameObject peopleActionDecider;
	public GameObject locationIdle, locationControls, locationPedals, locationUpGasser, locationHook, locationCenter;
	public GameObject shipStatusDooer;

	public Queue<Vector3> pathPoints;

	private ShipStatusDooer ship;
	private PeopleActionDecider decider;
	private Animator animator;

	private bool workStarted = false;

	// Use this for initialization
	void Start () {
		pathPoints = new Queue<Vector3> ();
		decider = peopleActionDecider.GetComponent<PeopleActionDecider>();
		ship = shipStatusDooer.GetComponent<ShipStatusDooer> ();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		// Get delta
		float delta = Time.deltaTime;

		// Walk along paths until we get to our destination
		if(WalkTo(delta)) return;

		// Wait
		if (waitTime > 0) {
			waitTime -= delta;
			if (waitTime < 0)
				waitTime = 0;
			return;
		}

		// We have no more paths, so we preform the action
		if(Action(delta)) return;

		// Action is done
		decider.ReleaseTask(currentTask);
		currentTask = decider.GetJob ();
		StartAction();
	}

	// Walk around, return if there is more walking to do
	bool WalkTo(float delta){
		if (pathPoints.Count > 0) {
			animator.SetTrigger ("crewWalk");
			transform.position = Vector3.MoveTowards (transform.position, pathPoints.Peek (), delta * speed);
			if (this.transform.position == pathPoints.Peek ()) {
				animator.SetTrigger ("crewStop");
				animator.ResetTrigger ("crewWalk");
				pathPoints.Dequeue ();
			}
			return true;
		}

		return false;
	}

	// Do a job, return if there is more job doing to do
	bool Action(float delta){
		switch (currentTask) {
		case PeopleActionDecider.Task.Idle:
			return false;

		case PeopleActionDecider.Task.Throttle:
			if (!workStarted) {
				animator.SetTrigger ("crewPedal");
				workStarted = true;
			} 

			ship.doPedalling (delta * strength);

			if (ship.getCurrentThrottle() >= decider.GetIdealThrottle()) {
				animator.SetTrigger ("crewStop");
				return false;
			}

			return true;

		case PeopleActionDecider.Task.Steer:
			if (!workStarted) {
				animator.SetTrigger ("crewSteer");
				workStarted = true;
			}

			ship.doSteering (delta * strength, decider.getIdealAngle ());

			if (Mathf.Abs (ship.getCurrentAngle () - decider.getIdealAngle ()) < 0.1) {
				animator.SetTrigger ("crewStop");
				return false;
			}

			return true;

		case PeopleActionDecider.Task.Hook:

			if (!workStarted) {
				ship.toggleHook ();
				workStarted = true;
				waitTime = 5;
				return true;
			}

			ship.toggleHook ();
			return false;

		case PeopleActionDecider.Task.Stoke:
			if (!workStarted) {
				workStarted = true;
				waitTime = 2;
				return true;
			}

			ship.stokeFire ();
			pathPoints.Enqueue (locationCenter.transform.position);
			return false;

		}
		return true;
	}

	void StartAction(){
		workStarted = false;

		switch (currentTask) {
		case PeopleActionDecider.Task.Idle:
			pathPoints.Enqueue (locationIdle.transform.position);
			pathPoints.Enqueue (locationCenter.transform.position);
			waitTime = 5;
			break;

		case PeopleActionDecider.Task.Throttle:
			pathPoints.Enqueue (locationPedals.transform.position);
			waitTime = 0.3f;
			break;

		case PeopleActionDecider.Task.Steer:
			pathPoints.Enqueue (locationControls.transform.position);
			break;

		case PeopleActionDecider.Task.Stoke:
			pathPoints.Enqueue (locationCenter.transform.position);
			pathPoints.Enqueue (locationUpGasser.transform.position);
			break;

		case PeopleActionDecider.Task.Hook:
			pathPoints.Enqueue (locationHook.transform.position);
			break;
		}
	}
}
