using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mastercontrol : MonoBehaviour
{
    private PhotonPlayer target = null;

    private PhotonPlayer[] otherList = null;
    private GameObject sky = null;
    private GameObject create = null;
    private Renderer rend;
    public Material nmaterial;
    public GameObject[] allPlayers;
    public PhotonPlayer[] otherPlayers;

    private Vector3 ranNum;

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private bool debug = true;



    public PhotonView photonView;

    /*
     * Contains most of the server commands that can be used while playing
     *
     */

    



    // Update is called once per frame
    void Update() {

        otherPlayers = PhotonNetwork.otherPlayers;
        //otherList = PhotonNetwork.otherPlayers;
       
        if (Input.GetKeyDown("2")) { // creates a target sphere for the players to attack

            create = PhotonNetwork.Instantiate("Sphere", Vector3.zero, Quaternion.identity, 0);

        }
        if (Input.GetKeyDown("3")) { // tells all players to create their head, automatic and unneeded now

            allPlayers = GameObject.FindGameObjectsWithTag("Player");
            GameObject player = allPlayers[0];
            player.GetComponent<PhotonView>().RPC("createHead", PhotonTargets.Others);
            //}
        }
        if (Input.GetKeyDown("4")) { // randomize locations for all players

            allPlayers = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in allPlayers) {
                player.GetComponent<PhotonView>().RPC("setLocation", PhotonTargets.Others);

                
            }
        }
        if (Input.GetKeyDown("5")) { // combo of 3 and 4 above, also set score to 0

            allPlayers = GameObject.FindGameObjectsWithTag("Player");
            
            GameObject player = allPlayers[0];
            player.GetComponent<PhotonView>().RPC("createHead", PhotonTargets.Others);
            foreach (GameObject players in allPlayers) {
                players.GetComponent<PhotonView>().RPC("setLocation", PhotonTargets.Others);
            }

            foreach (PhotonPlayer pl in otherPlayers) {
                pl.SetScore(0);
            }

        }
        if (Input.GetKeyDown("6")) { //test for changing material

            create.GetComponent<Renderer>().material = nmaterial;

        }

    }


    [PunRPC]
    public void setLocation() {
        // dummy rpc
    }

    protected void OnGUI() {
        if (!debug)
            return;
        guiStyle.fontSize = 80; //change the font size
        foreach (PhotonPlayer pl in otherPlayers) {
            if (pl != null) {
                GUILayout.Label("ID:" + pl.ID + " Score:" + pl.GetScore(), guiStyle);
            }
        }

        //GUILayout.Label("Location: " + transform.position, guiStyle);

    }
}
