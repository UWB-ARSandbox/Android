using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Uses the compass in the phone in order to adjust the compass
 * at the top of the players view
 */

public class Compass : MonoBehaviour
{
    public Vector3 NorthDirection;
    public RectTransform NorthLayer;
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private bool debug = true;


    void Start() {
        Input.compass.enabled = true;
    }

    /
    void Update() {
        ChangeNorthDirection();


    }

    public void ChangeNorthDirection() {
        NorthDirection.z = Input.compass.magneticHeading;
        NorthLayer.localEulerAngles = NorthDirection;

    }

    protected void OnGUI() {
        if (!debug)
            return;
        guiStyle.fontSize = 80; //change the font size
    }




}
