using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAssist : MonoBehaviour
{

    public Camera mCam;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (mCam == null) {
            mCam = Camera.main;
        }
    }

    public void helpHead() {
       //mCam.GetComponent<CreateHead>().createHead();
    }

    [PunRPC]
    public void deleteMe() {
        //PhotonNetwork.Destroy(self);
    }
}
