using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food_ : MonoBehaviour {

    public float amount = 5000;
    private resources type = resources.food;
	

	void FixedUpdate () {
        if (amount <= 0)
        {
            Destroy(gameObject);
        }
	}


}
