using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_selection : MonoBehaviour {

    public static Text nameText;


	// Use this for initialization
	void Start () {
        nameText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal static void PrintNames(GameObject[] selectedObjs)
    {
        //string names = "lqlq";

        //print()

        for (int i = 0; i < selectedObjs.Length; i++)
        {
            if (selectedObjs[i] != null)
            {
                //names += selectedObjs[i].name + ", ";
                print(selectedObjs[i].name);
            }
        }

        //ChangeText(names);
    }

    private static void ChangeText(string textToShow)
    {
        nameText.text = textToShow;
    }

}
