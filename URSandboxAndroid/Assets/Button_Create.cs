using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/*
 * Chooses a bullet type based on the id of the player
 * Upon tapping the button, creates and fires a bullet where the player is looking
 */
public class Button_Create : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    private bool once = true;
    private GameObject target;
    private Rigidbody testShot;
    public Camera mCam;
    public string bullet;
    public CreateHead ch;

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private int temp;
    private bool runOnce = true;


    private bool debug = true;

    private void start() {

    }

    private void getBullet() {
        temp = PhotonNetwork.player.ID % 5;
        switch (temp) {
            case 0:
                bullet = "Apple";
                break;
            case 1:
                bullet = "Banana";
                break;
            case 2:
                bullet = "Kiwi";
                break;
            case 3:
                bullet = "Pear";
                break;
            case 4:
                bullet = "Strawberry";
                break;

        }
    }



    private void update() {
       
    }


    public virtual void OnPointerDown(PointerEventData ped) {

        if (runOnce) {
            getBullet();
            runOnce = !runOnce;
        }
        if (mCam.GetComponent<LookAround>().HaveHead == true) {
            target = PhotonNetwork.Instantiate(bullet, Vector3.zero, Quaternion.identity, 0);


            target.transform.position = mCam.transform.position + mCam.transform.forward.normalized;
            testShot = target.GetComponent<Rigidbody>();
            testShot.AddForce(mCam.transform.forward * 200);
        }

        once = !once;


    }


    public virtual void OnPointerUp(PointerEventData ped) {
       
    }


}