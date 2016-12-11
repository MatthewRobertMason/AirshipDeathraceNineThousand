using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGrabDooer : MonoBehaviour {

	public GameObject hooked = null;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hooked) {
			PrizeMind prize = hooked.GetComponent<PrizeMind> ();
			prize.transform.position = transform.position;
		}		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (hooked)
			return;


		hooked = other.gameObject;
		hooked.GetComponent<PrizeMind>().hooked = true;
	}
}
