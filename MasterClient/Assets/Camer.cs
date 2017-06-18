
using UnityEngine;

public class Camer : MonoBehaviour
{
    WebCamTexture backCam;
    Renderer rend;
    public GameObject self;

    private Camera mCam;
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private bool debug = false;
    public bool hitMe = false;
    // Use this for initialization
    void Start() {
        //backCam = new WebCamTexture();

        //rend = GetComponent<Renderer>();//.material.maintexture = backCam;
        //rend.material.mainTexture = backCam;



    }

    // Use this for initialization

    // Update is called once per frame
    void Update() {
        if (hitMe) {
            PhotonNetwork.Destroy(self);
        }





    }

    void OnCollisionEnter(Collision col) {
        //Debug.LogError("Hit");
        if (col.gameObject.tag == "bullet") {
            //Debug.Log("Super Hit");
            //hitMe = true;
            col.gameObject.GetComponent<PhotonView>().owner.AddScore(1);
            col.gameObject.GetComponent<PhotonView>().RPC("deleteMe", col.gameObject.GetComponent<PhotonView>().owner);
            this.GetComponent<PhotonView>().RPC("killMe", this.GetComponent<PhotonView>().owner);
            //PhotonNetwork.Destroy(col.gameObject);
            //PhotonNetwork.Destroy(self);
            //Debug.LogError("Delete");
            //self.GetComponent<PhotonView>().RPC("killMe", PhotonTargets.All);
        }
    }

    [PunRPC]
    public void killMe() {
        //Debug.LogError("Hit");
        PhotonNetwork.Destroy(self);
        //if (haveHead) {
        // ranNum = new Vector3(Random.Range(-2.5f, 2.5f), 1, Random.Range(-2.5f, 2.5f));
        //transform.position = ranNum;
        //}
    }

    void OnGUI() {
        if (debug) {
            guiStyle.fontSize = 80;
            GUILayout.Label("test " + self.GetComponent<PhotonView>().owner, guiStyle);

        }/*
        if (backCam.isPlaying) {
            if (GUILayout.Button("Pause", guiStyle))
                backCam.Pause();
        } else if (GUILayout.Button("Play", guiStyle))
            backCam.Play();
    */
    }

}
