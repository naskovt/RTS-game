using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resources_manager : MonoBehaviour {

    public Text resourcesText;

    public static float supplies = 0;

    public static float courage = 0;

    public static float gameScore = 0;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        gameScore = (supplies * courage) / 1000;
        resourcesText.text = "supplies: " + Convert.ToString((int)supplies) + "         " + "courage: " + Convert.ToString((int)courage) + "      Score: " + Convert.ToString((int)gameScore);

    }


}
