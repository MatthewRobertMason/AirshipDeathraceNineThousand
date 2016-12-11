using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    // I'm not sure what the max should be, almost certainly not 500
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float scrollspeed;

    public GameObject shipStatusDooer;
	protected ShipStatusDooer ship;

	public GameObject top;
	public GameObject bottom;


    private Vector2 offset;

    // Use this for initialization
    void Start ()
    {
		ship = shipStatusDooer.GetComponent<ShipStatusDooer>();
	}
	
	// Update is called once per frame
	void Update () {
        float y = scrollspeed * ship.getCurrentThrottle();

        offset += new Vector2(y, 0);

        this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);

		// Make it so that changes in hight become smaller and smaller the higher you get.
		float height_ratio = 1 - (2.0f / (1 + Mathf.Exp (-ship.GetAltitude () / (ShipStatusDooer.MaxAltitude () / 2))) - 1);
		//float height_ratio = 1 -  Mathf.Sqrt(ship.GetAltitude() / ShipStatusDooer.MaxAltitude());

		// Move the background vertically.
		Vector3 position = transform.position;
		position.y = (top.transform.position.y - bottom.transform.position.y) * height_ratio + bottom.transform.position.y;
		transform.position = position;
        
	}
}
