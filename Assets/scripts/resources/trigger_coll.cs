using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_coll : MonoBehaviour {

    internal static bool isPlayerInside = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            isPlayerInside = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            isPlayerInside = false;
        }
    }


}
