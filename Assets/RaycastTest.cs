using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour
{

    Vector3 touchPosWorld;

    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Began;

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private bool debug = true;
    private string isHit = "Can't touch this!";
    RaycastHit hit;
    Ray ray;

    void Update() {
        //We check if we have more than one touch happening.
        //We also check if the first touches phase is Ended (that the finger was lifted)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase) {
            //We transform the touch position into word space from screen space and store it.
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            //We now raycast with this information. If we have hit something we can process it.
            //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            ray = Camera.main.ScreenPointToRay(touchPosWorld2D);

            if (Physics.Raycast(ray, out hit)) {
                //Debug.Log(hit.collider.gameObject.name);
                isHit = hit.collider.gameObject.name;
            }
        }
    }

    protected void OnGUI() {
        if (!debug)
            return;
        guiStyle.fontSize = 80; //change the font size
        GUILayout.Label("Touched?: " + isHit, guiStyle);
        GUILayout.Label("Touching: " + touchPosWorld, guiStyle);

    }
}