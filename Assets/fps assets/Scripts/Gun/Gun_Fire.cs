using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Fire : MonoBehaviour {

    public GameObject sleeve;
    public GameObject bullet;
    public float fireRateLimit = 20;

    private GameObject sleeveSpawnPoint;
    private GameObject bulletSpawnPoint;
    private float fireCount;
    private movement playerMovementScript;

	void Start () {
        bulletSpawnPoint = Custom.findChildWithTag(gameObject, "bulletSpawn");
        sleeveSpawnPoint = Custom.findChildWithTag(gameObject, "sleeveSpawn");
        playerMovementScript = GetComponentInParent<movement>();
    }


    private void Update()
    {

        //fire!!!
        if (playerMovementScript.isShooting)
        {
            if (fireCount == fireRateLimit || fireCount == 0)
            {
                Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                Instantiate(sleeve, sleeveSpawnPoint.transform.position, sleeveSpawnPoint.transform.rotation);
                fireCount = 1;
            }
            else
            {
                fireCount++;
            }

        }
        else
        {
            fireCount = 0;
        }
        
    }
}
