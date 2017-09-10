using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loosing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == global_const.enemiesTag)
        {
            pop_up_text.StrToPop = "Game over! Your score is not that bad actually!";
        }
    }
}
