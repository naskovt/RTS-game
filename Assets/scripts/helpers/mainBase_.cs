using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainBase_ : MonoBehaviour {

    public static GameObject mainBaseRef;

	// Use this for initialization
	void Start () {
        mainBaseRef = GameObject.FindGameObjectWithTag("townCenter");
;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
