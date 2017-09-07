using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class unit_prop : MonoBehaviour {

    private float health = 100;
    private float attack = 5;
    private units_type type = units_type.peasant;
    public Image greenBar;

    //resources
    private gather_resources gatheringAgent;
    private resources gatheredType = 0;

    //enemy
    private unit_sense sense;


    // Use this for initialization
    void Start () {
        sense = GetComponent<unit_sense>();
        
    }
	
	// Update is called once per frame
	void Update () {

        if (gatheredType != resources.none)
        {
            gather_resources.StartGathering(gatheredType);
        }

        //if (sense.isEnemyInRange)
        //{

        //}

	}


    private void UpgradeAttack(float upgrade)
    {
        this.attack += upgrade;
    }

    private void UpgradeHealth(float upgrade)
    {
        this.health += upgrade;
    }

    private void Attack(GameObject enemy)
    {

    }

    private void TakeDamage(float damage)
    {
        this.health -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "resource")
        {
            gatheredType = collision.collider.gameObject.GetComponent<resource_info>().type;
        }
        else if (collision.collider.tag == "building")
        {
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "resource")
        {
            gatheredType = resources.none;
        }
        else if (collision.collider.tag == "building")
        {
        }
    }


}
