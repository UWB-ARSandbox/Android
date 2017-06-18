using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Creates the head for the user
 */

public class CreateHead : MonoBehaviour
{

    private GameObject target;
    private GameObject nose;
    public Camera mCam = Camera.main;
    public bool haveHead = false;
    private Vector3 ranNum;

    
    private void start() {
    }
    void Update() {

        if (mCam == null) {
            mCam = Camera.main;
        }
        if (haveHead) {
            Debug.Log(target.transform.position);
        }
        



    }

    public GameObject getTarget() {
        return target;
    }

    public bool doesHeadExsist() {
        return haveHead;
    }

    [PunRPC]
    public void createHead() {
        if (mCam == null) {
            mCam = Camera.main;
        }
        haveHead = mCam.GetComponent<LookAround>().HaveHead;
        if (!haveHead) {
            target = PhotonNetwork.Instantiate("Sphere", Vector3.zero, Quaternion.identity, 0);
            haveHead = true;
            mCam.GetComponent<LookAround>().HaveHead = true;
            mCam.GetComponent<LookAround>().setHead(target);
            target.transform.position = transform.position;
        }
    }

    [PunRPC]
    public void setLocation() {
        if (mCam == null) {
            mCam = Camera.main;
        }
        ranNum = new Vector3(Random.Range(-2.5f, 2.5f), 1, Random.Range(-2.5f, 2.5f));
        mCam.transform.position = ranNum;
        target.transform.position = ranNum;
    }


}
