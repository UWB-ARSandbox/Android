using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {



    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
                                                // Use this for initialization

    private string Direction = "North";
    private int findNum = 1;

    private bool debug = true;


    void Start () {
        Input.compass.enabled = true;
        findNum = Random.Range(1, 4);
    }
	
	// Update is called once per frame
	void Update () {

        


		if (Input.compass.magneticHeading < 45 || Input.compass.magneticHeading >= 315) {
            Direction = "North";
        } else if(Input.compass.magneticHeading >= 45 && Input.compass.magneticHeading < 135) {
            Direction = "East";
        } else if (Input.compass.magneticHeading >= 135 && Input.compass.magneticHeading < 225) {
            Direction = "South";
        } else if (Input.compass.magneticHeading >= 225 && Input.compass.magneticHeading < 315) {
            Direction = "West";
        } else {
            Direction = "NULL";
        }

	}

   

    protected void OnGUI() {
        if (!debug)
            return;
        guiStyle.fontSize = 80; //change the font size

        GUILayout.Label("Direction: " + Direction, guiStyle);
       
       
    }
}
