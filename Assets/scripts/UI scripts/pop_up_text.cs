using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pop_up_text : MonoBehaviour {

    public Text text;
    private static bool isPopingUp = false;
    private float timeLapsed;
    private float timeToStay = 2;
    static string strToPop;

    internal static string StrToPop
    {
        set
        {
            strToPop = value;
            isPopingUp = true;
        }

    }


	// Use this for initialization
	void Start () {
        text.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {

        if (isPopingUp)
        {
            if (timeLapsed < timeToStay)
            {
                text.enabled = true;
                text.text = strToPop;
                timeLapsed += Time.deltaTime;
            }
            else
            {
                isPopingUp = false;
                timeLapsed = 0;
                text.enabled = false;
            }
        }

	}
}
