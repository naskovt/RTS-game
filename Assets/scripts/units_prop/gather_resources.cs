using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gather_resources : MonoBehaviour {

    public static void StartGathering(resources resource)
    {
        if (resource == resources.food)
        {
            resources_manager.food += Time.deltaTime;
        }
        else if (resource == resources.wood)
        {
            resources_manager.wood += Time.deltaTime;
        }
        else
        {
            resources_manager.stone += Time.deltaTime;
        }
    }

    public static void AddPopulation()
    {
        resources_manager.population += 5;
    }

}
