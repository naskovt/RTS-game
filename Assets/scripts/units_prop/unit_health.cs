using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_health : MonoBehaviour {

    private unit_prop uniPropScript;
    public string enemyTagToAttack = "enemy";

    private float damageUnitFromBullet = 2;
    private float damageUnitFromMalee = 10;



    private void Start()
    {
        uniPropScript = GetComponent<unit_prop>();
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        //if bullet touches the coll and the unit is an enemy
        if (collision.collider.tag == global_const.bulletsTag && transform.tag == global_const.enemiesTag)
        {
            uniPropScript.TakeDamage(damageUnitFromBullet);
        }
        //if the unit is a player and enemy touches him
        if (transform.name == global_const.playersTag && collision.collider.tag == enemyTagToAttack)
        {
            uniPropScript.TakeDamage(damageUnitFromMalee + global_const.zombieBonusDamage);
        }
        //the opposite
        else if (transform.name == enemyTagToAttack && collision.collider.tag == global_const.playersTag)
        {
            uniPropScript.TakeDamage(damageUnitFromMalee);
        }
    }

}