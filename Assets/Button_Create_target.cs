using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Button_Create_target : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    private bool once = true;
    public GameObject target;
    public Button_Create create;
    private Vector3 ranNum;
    //public Camer cam;

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private string output;

    private void start() {
        //output = "none";
    }


    public virtual void OnPointerDown(PointerEventData ped) {
        if (once) {
            target = PhotonNetwork.Instantiate("Sphere", Vector3.zero, Quaternion.identity, 0);
            once = !once;
            ranNum = new Vector3(Random.Range(-2.5f, 2.5f), 0, Random.Range(-2.5f, 2.5f));
            target.transform.position = ranNum;
            //PhotonNetwork.Destroy(create.target);
            //cam.runMe = true;
        }
    }
    



    public virtual void OnPointerUp(PointerEventData ped) {
        once = !once;
    }


}