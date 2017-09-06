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
    private bool isEnemyInRange = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gatheredType != resources.none)
        {
            gather_resources.StartGathering(gatheredType);
        }

        if (isEnemyInRange)
        {

        }

        if (health <= 0)
        {
            PlayerDies();
        }
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

    private void PlayerDies()
    {
        resources_manager.population += 1;
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "resource")
        {
            gatheredType = collision.collider.gameObject.GetComponent<resource_info>().type;
        }
        else if (collision.collider.tag == "enemy")
        {
            isEnemyInRange = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "resource")
        {
            gatheredType = resources.none;
        }
        else if (collision.collider.tag == "enemy")
        {
            isEnemyInRange = false;
        }
    }


}
