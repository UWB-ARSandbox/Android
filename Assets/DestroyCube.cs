using UnityEngine;
using System.Collections;

public class DestroyCube : MonoBehaviour
{


    //private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    //private string output = "none";

    private void start() {
    }

    void OnCollisionEnter(Collision col) {
        //if (col.gameObject.name == "Sphere(Clone)") {
        //Destroy(col.gameObject);
        PhotonNetwork.Destroy(col.gameObject);
        //output = "hit";
        Debug.Log("Hit");
        // }
    }


    
}