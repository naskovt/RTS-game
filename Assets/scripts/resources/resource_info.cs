using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resource_info : MonoBehaviour {

    public float amount = 5000;
    public resources type = resources.supplies;


    void FixedUpdate()
    {
        if (amount <= 0)
        {
            Destroy(gameObject);
        }
    }


}
