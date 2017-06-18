using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHead : MonoBehaviour
{

    private GameObject target;
    private GameObject nose;
    //public Camera mCam;
    public bool haveHead = false;
    private Vector3 ranNum;







    private void start() {
        //output = "none";
    }
    void Update() {
        if (target != null) {
            target.transform.position = transform.position;
            //nose.transform.position = mCam.transform.position + mCam.transform.forward * .1f;
        }

        //nose.transform.position = mCam.transform.forward;
        if (target == null && haveHead) {
            haveHead = false;
        }


    }

    public bool doesHeadExsist() {
        return haveHead;
    }

    [PunRPC]
    public void createHead() {
        if (!haveHead) {
            target = PhotonNetwork.Instantiate("Sphere", Vector3.zero, Quaternion.identity, 0);
            //nose = PhotonNetwork.Instantiate("Sphere", Vector3.zero, Quaternion.identity, 0);
            //nose.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            haveHead = !haveHead;
            //ranNum = new Vector3(Random.Range(-2.5f, 2.5f), 0, Random.Range(-2.5f, 2.5f));
            target.transform.position = transform.position;
            /*nose.GetComponent<SphereCollider>().enabled = false;
            Vector3 tempScale = nose.transform.localScale;
            tempScale.x = .1f;
            tempScale.y = .1f;
            tempScale.z = .1f;
            nose.transform.localScale = tempScale;
            nose.transform.position = mCam.transform.position + mCam.transform.forward;*/
            //PhotonNetwork.Destroy(create.target);
            //cam.runMe = true;
        }
    }


}
