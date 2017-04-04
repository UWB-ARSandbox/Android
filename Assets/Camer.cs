using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer : MonoBehaviour
{
    WebCamTexture backCam;
    Renderer rend;
    public bool runMe = false;
    public Button_change change;

    // Use this for initialization
    void Start() {
      
    }

    // Update is called once per frame
    void Update() {
        if (runMe) {
            backCam = new WebCamTexture();

            rend = GetComponent<Renderer>();//.material.maintexture = backCam;
            rend.material.mainTexture = backCam;
            backCam.Play();
        }

    }

}
