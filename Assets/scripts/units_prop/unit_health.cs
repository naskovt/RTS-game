using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_health : MonoBehaviour {

    private unit_prop uniPropScript;
    public string enemyTagToAttack = "enemy";

    private float damageUnitFromBullet = 2;
    private float damageUnitFromMalee = 10;
    private float zombieBonusDamage = 30;
    private string playerTagsInGame = "Player";
    private string enemyTagsInGame = "enemy";
    private string bulletTagsInGame = "bullet";


    private void Start()
    {
        uniPropScript = GetComponent<unit_prop>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        //if bullet touches the coll and the unit is an enemy
        if (collision.collider.tag == bulletTagsInGame && transform.tag == enemyTagsInGame)
        {
            uniPropScript.TakeDamage(damageUnitFromBullet);
        }
        //if the unit is a player and enemy touches him
        if (transform.name == playerTagsInGame && collision.collider.tag == enemyTagToAttack)
        {
            uniPropScript.TakeDamage(damageUnitFromMalee + zombieBonusDamage);
        }
        //the opposite
        else if (transform.name == enemyTagToAttack && collision.collider.tag == playerTagsInGame)
        {
            uniPropScript.TakeDamage(damageUnitFromMalee);
        }
    }


    //private void RespawnPlayer()
    //{
    //    health = 100;
    //    modulateGUIBar();

    //    transform.position = new Vector3(0, 1, 0);

    //    GameManager.score = 0;
    //}

}