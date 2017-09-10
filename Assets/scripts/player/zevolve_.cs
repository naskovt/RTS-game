using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zevolve_ : MonoBehaviour {

    public GameObject zombiePrefab;


    private bool isZevolved = false;

    private float transformationTimer = 0;

	
	// Update is called once per frame
	void FixedUpdate () {
        if (isZevolved)
        {
            //start the transformation
            transformationTimer += Time.deltaTime;


            if (transformationTimer > global_const.zevolveTimer)
            {
                Zevolve();
            }
        }
	}

    private void Zevolve()
    {
        Instantiate(zombiePrefab, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == global_const.enemyCollidersTag)
        {
            isZevolved = true;
        }
    }
}
