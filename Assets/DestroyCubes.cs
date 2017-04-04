using UnityEngine;
using System.Collections;

public class DestroyCubes : MonoBehaviour
{


    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private string output;

    private void start() {
        output = "none";
    }

    void OnCollisionEnter(Collision col) {
        //if (col.gameObject.name == "Sphere(Clone)") {
        //Destroy(col.gameObject);
        //PhotonNetwork.Destroy(col.gameObject);
        //output = "hit";
        Debug.Log("Hit");
        // }
    }


    protected void OnGUI() {
        guiStyle.fontSize = 80; //change the font size
        GUILayout.Label("Direction: " + output, guiStyle);
        //GUILayout.Label("Location: " + transform.position, guiStyle);
    }
}