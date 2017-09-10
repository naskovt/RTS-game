using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resource_info : MonoBehaviour {

    public float amount = 5000;

    internal int numberUnitsGathering = 0;


    void FixedUpdate()
    {
        //if supplies are empty - destroy obj
        if (amount <= 0)
        {
            Destroy(gameObject);
        }

        if (numberUnitsGathering > 0)
        {

                resources_manager.supplies += global_const.resourcesGathering * numberUnitsGathering;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == global_const.playersTag)
        {
            numberUnitsGathering++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == global_const.playersTag)
        {
            if (numberUnitsGathering > 0)
            {
                numberUnitsGathering--;
                print("888");
            }

        }
    }
}
