using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Button_Create : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    private bool once = true;
    public GameObject target;
    public GameObject prevTarget;
    private Rigidbody testShot;
    public Camera mCam;

    private void start() {
    }


    private void update() {
        //if (target != null) {
            //create.target.rigidbody.use
            //target.transform.position = mCam.transform.position + mCam.transform.forward.normalized;
        //}
    }
    

public virtual void OnPointerDown(PointerEventData ped) {
        if (once) {
            
            
            target = PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity, 0);
            target.transform.position = mCam.transform.position + mCam.transform.forward.normalized;
            testShot = target.GetComponent<Rigidbody>();
            if (target != null) {

                testShot.AddForce(mCam.transform.forward * 100);
            }
            once = !once;
            
        }
    }


    public virtual void OnPointerUp(PointerEventData ped) {
        
        once = !once;
    }


}