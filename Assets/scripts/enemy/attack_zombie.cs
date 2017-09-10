using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_zombie : MonoBehaviour {

    private bool isAttacking = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == global_const.playerCollidersTag)
        {

            isAttacking = true;
        }
        //print(other.tag);
    }

    //private void OnTriggerExit(Collider other)
    //{
        
    //    if (other.tag == global_const.playerCollidersTag)
    //    {

    //        isAttacking = false;
    //    }
    //}

    public bool IsAttacking()
    {
        return isAttacking;
    }

}
