using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class global_const {

    public const string playersTag = "Player";
    public const string enemiesTag = "enemy";
    public const string enemyCollidersTag = "enemyCollider";
    public const string playerCollidersTag = "playerCollider";
    public const string resourcesTag = "resource";
    public const string buildingsTag = "building";
    public const string wallTag = "wall";
    public const string bulletsTag = "bullet";
    public const string sleeveTag = "sleeve";

    public const float populationLimit = 99;


    public static int zombieBonusDamage = 30;

    public static float playerWidth = 0.5f;

    public static float destroyZombieCorpsAfter = 1f;

    public static float damageUnitFromBullet = 2f;
    public static float damageUnitFromMalee = 10;

    public static float buildingSpeed = 2;
    public static float wallHeight = 25;

    public static float deviationRadius = 1;

    public static float resourcesGathering = 0.005f;

    public static float playerCreationTime = 30;

    //surprising the player by transofrming the infected unit into a zombie(zevolving process) after a zombie touched him and the timer passes
    public static float zevolveTimer = 0.1f;

    public static float spawnEnemiesEach = 1;



    public static Vector3 mainBasePointOfDefeat = new Vector3(2.47f, 0.1f, -0.34f);

}

