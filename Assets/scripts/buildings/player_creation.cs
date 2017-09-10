using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_creation : MonoBehaviour {

    public GameObject playerSpawnPoint;
    public GameObject prefab;

    public Text textOnButton;


    private float timeLapsed = 0;

    private bool isCreatingPlayer = false;
     
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (isCreatingPlayer)
        {
            timeLapsed += Time.deltaTime * global_const.playerCreationTime;
            textOnButton.text += " " + Convert.ToString( (int)timeLapsed ) + "%";

            if (timeLapsed > 99)
            {
                CreatePlayer();
            }
        }

	}

    private void CreatePlayer()
    {
                textOnButton.text = "player";
                timeLapsed = 0;
                isCreatingPlayer = false;
                Instantiate(prefab, playerSpawnPoint.transform.position, Quaternion.identity);
    }

    public void StartCreation()
    {
        isCreatingPlayer = true;
        // resources --
    }
}
