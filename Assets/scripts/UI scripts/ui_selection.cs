using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_selection : MonoBehaviour {

    /// <summary>
    /// check wich object is currently selected and pop up the UI for it
    /// </summary>

    //private Button button;


	// Use this for initialization
	void Start () {
        //nameText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        //if one object is selected
        if (array_manager.IsArrayWithOneObj(selection_manager.selectedObjects))
        {
            if (selection_manager.selectedObjects[0].tag == global_const.playersTag)
            {

            }
        }
    }



}
