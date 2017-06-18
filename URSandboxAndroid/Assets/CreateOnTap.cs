using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Not used in the game, but is used for different touches on the screen
 * Can change the ammount of time to hold for, where to type on the screen
 */
public class CreateOnTap : MonoBehaviour
{
    private float holdTime = 0.8f;
    private float acumTime = 0;


    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private bool debug = true;
    private bool once = true;

    void Update() {

    }
    void Start() {

    }

    protected void OnGUI() {
        if (!debug)
            return;
        guiStyle.fontSize = 80; //change the font size
        if (Input.touchCount > 0) {
            acumTime += Input.GetTouch(0).deltaTime;
            //PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity, 0);
            var touch = Input.touches[0];
            if (touch.position.x < Screen.width / 2) {
                // GUILayout.Label("LEFT", guiStyle);

            } else if (touch.position.x > Screen.width / 2) {
                //GUILayout.Label("RIGHT", guiStyle);
                if (once) {
                    //PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity, 0);
                    once = !once;
                }
            }
            //GUILayout.Label("TAPING", guiStyle);

            if (acumTime >= holdTime) {
                // GUILayout.Label("HOLDING", guiStyle);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                acumTime = 0;
                once = !once;
            }
        }

    }
}
