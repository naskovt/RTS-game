using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class unit_prop : MonoBehaviour
{

    public Image greenBar;

    private float health = 100;

    //enemy
    private unit_sense sense;


    // Use this for initialization
    void Start()
    {
        sense = GetComponent<unit_sense>();
    }


    internal void modulateGUIBar()
    {
        float healthPercent = health / 100f;

        if (healthPercent >= 0)
        {
            greenBar.rectTransform.localScale = new Vector3(healthPercent, 1, 1);
        }
    }

    internal void TakeDamage(float damage)
    {
        this.health -= damage;

        if (health <= 0)
        {
            UnitDies();
        }
        else
        {
            modulateGUIBar();
        }
    }

    internal void UnitDies()
    {
        //check to what kind of unit, the script is attached
        if (gameObject.tag == global_const.enemiesTag)
        {
            GetComponent<zombie_movement>().isDead = true;
            resources_manager.courage++;
        }

    }
}

