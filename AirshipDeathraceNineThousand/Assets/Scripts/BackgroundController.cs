using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    // I'm not sure what the max should be, almost certainly not 500
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float scrollspeed;

    public GameObject shipStatusDooer;

    private Vector2 offset;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
        float y = scrollspeed * shipStatusDooer.GetComponent<ShipStatusDooer>().getCurrentThrottle();

        offset += new Vector2(y, 0);

        this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);

        
	}
}
