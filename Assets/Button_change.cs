using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Button_change : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    private bool once = true;
    public bool change = false;

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


            change = !change;
            once = !once;

        }
    }


    public virtual void OnPointerUp(PointerEventData ped) {

        once = !once;
    }


}