using UnityEngine;
using System.Collections;

/*
 * Originally going to handle collisions, but thats better to be handled by the server
 * Only destroys the bullets self on hit with another bullet, or when traveling too far
 */

public class DestroyCube : MonoBehaviour
{
    public GameObject self;
    

    private void start() {
    }
    private void Update() {
        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.z > 10 || transform.position.z < -10 || transform.position.y > 10 || transform.position.y < -10) {
            
            PhotonNetwork.Destroy(self);
        }
    }

    void OnCollisionEnter(Collision col) {
        
        if (col.gameObject.tag == "Head") {
            

        } else if (col.gameObject.tag == "bullet") {

        } else {
            PhotonNetwork.Destroy(self);
        }
       
    }
    [PunRPC]
    public void deleteMe() {
        PhotonNetwork.Destroy(self);
    }



}