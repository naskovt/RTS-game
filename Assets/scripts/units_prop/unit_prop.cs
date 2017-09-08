using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class unit_prop : MonoBehaviour {

    public Image greenBar;

    private float health = 100;
    private units_type type = units_type.peasant;

    //resources
    private gather_resources gatheringAgent;
    private resources gatheredType = 0;

    //enemy
    private string playerTagsInGame = "Player";
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
	}

    internal void modulateGUIBar()
    {
        float healthPercent = health / 100f;

        if (healthPercent >= 0)
        {
            greenBar.rectTransform.localScale = new Vector3(healthPercent, 1, 1);
        }
    }

    internal void UpgradeHealth(float upgrade)
    {
        this.health += upgrade;
        modulateGUIBar();
    }

    internal void TakeDamage(float damage)
    {
        if (health <= 0)
        {
            UnitDies();
        }
        else
        {
            this.health -= damage;
            modulateGUIBar();
        }
    }

    internal void UnitDies()
    {
        //if (transform.tag == playerTagsInGame && selection_manager.IsSameObjectSelected(gameObject))
        //{
        //    print("selected object dies");
        //}
        gameObject.SetActive(false);
    }

    /// Collision listening functions

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
