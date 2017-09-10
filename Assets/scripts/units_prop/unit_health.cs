using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class unit_health : MonoBehaviour {

    //this unit will attack objs with this tag
    public string enemyTagToAttack = "enemy";

    private unit_prop uniPropScript;

    //health
    internal bool isSelected = false;

    private void Start()
    {
        uniPropScript = GetComponent<unit_prop>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if bullet touches the coll and the unit is an enemy
        if (other.tag == global_const.bulletsTag && transform.tag == global_const.enemiesTag)
        {
            if (uniPropScript != null)
            {
            uniPropScript.TakeDamage(global_const.damageUnitFromBullet);

            }
            //print("7ombi takes damage bullet");
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if bullet touches the coll and the unit is an enemy
    //    if (collision.collider.tag == global_const.bulletsTag && transform.tag == global_const.enemiesTag)
    //    {
    //        uniPropScript.TakeDamage(global_const.damageUnitFromBullet);
    //        print("7ombi takes damage");
    //    }
    //    //if the unit is a player and enemy touches him
    //    if (transform.name == global_const.playersTag && collision.collider.tag == enemyTagToAttack)
    //    {
    //        print("7ombi takes damage");

    //        uniPropScript.TakeDamage(global_const.damageUnitFromMalee + global_const.zombieBonusDamage);
    //    }
    //    //the opposite
    //    else if (transform.name == enemyTagToAttack && collision.collider.tag == global_const.playersTag)
    //    {
    //        print("7ombi takes damage");

    //        uniPropScript.TakeDamage(global_const.damageUnitFromMalee);
    //    }
    //}

}