using UnityEngine;
using System.Collections;

/*
 * This code detects swiping on the screen
 * The minimum distance and maximum time can be adjusted
 */

public class SwipeScript : MonoBehaviour
{


    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;
    private Vector3 right;

    private bool debug = false;
    public GameObject[] allPlayers;
    WebCamTexture backCam;

    public GameObject[] allhead;

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable

    void Start() {
        backCam = new WebCamTexture();

        //rend = GetComponent<Renderer>();//.material.maintexture = backCam;
        //rend.material.mainTexture = backCam;



    }
    // Update is called once per frame
    void Update() {
        allhead = GameObject.FindGameObjectsWithTag("Head");
        if (allhead.Length > 0) {
            foreach (GameObject onehead in allhead) {
                onehead.GetComponent<Renderer>().material.mainTexture = backCam;
            }
        }

        if (Input.touchCount > 0) {

            foreach (Touch touch in Input.touches) {
                switch (touch.phase) {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            } else {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f) {
                                if (swipeType.x > 0.0f) {
                                    if (touch.position.x > Screen.width / 2) {
                                        //if (touch.position.y > Screen.width / 2) {
                                        right = transform.right;
                                        right.y = 0;
                                        //transform.position += right;
                                        //}
                                        //GUILayout.Label("RIGHT", guiStyle);
                                    }

                                    // MOVE RIGHT
                                } else {
                                    if (touch.position.x > Screen.width / 2) {
                                        //if (touch.position.y > Screen.width / 2) {
                                        right = transform.right;
                                        right.y = 0;
                                        //transform.position -= right;
                                        //}
                                    }
                                    // MOVE LEFT
                                }
                            }

                            if (swipeType.y != 0.0f) {
                                if (swipeType.y > 0.0f) {
                                    if(touch.position.x > Screen.width / 2) {
                                        //if (touch.position.y > Screen.width / 2) {
                                        if (transform.position.y == 3) {

                                        } else {
                                            Vector3 temp = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                                            transform.position = temp;
                                        }
                                        //}
                                    }
                                    if (touch.position.x < Screen.width / 2) {
                                        //if (touch.position.y > Screen.width / 2) {
                                        debug = !debug;

                                        

                                        /*if (backCam.isPlaying) {
                                            backCam.Pause();
                                        }else {
                                            backCam.Play();
                                        }*/

                                        //}
                                    }
                                    // MOVE UP
                                } else {
                                    if (touch.position.x > Screen.width / 2) {
                                        //if (touch.position.y > Screen.width / 2) {
                                        if (transform.position.y == 0) {

                                        } else {
                                            Vector3 temp = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                                            transform.position = temp;
                                        }
                                        //}
                                    }
                                    if (touch.position.x < Screen.width / 2) {
                                        //if (touch.position.y > Screen.width / 2) {
                                        debug = !debug;
                                        //}

                                        
                                    }
                                    // MOVE DOWN
                                }
                            }

                        }

                        break;
                }
            }
        }

    }

    protected void OnGUI() {
        if (!debug)
            return;
        guiStyle.fontSize = 80; //change the font size
        GUILayout.Label("Your Score is : " + PhotonNetwork.player.GetScore(), guiStyle);
        if (backCam.isPlaying) {
            if (GUILayout.Button("Pause", guiStyle))
                backCam.Pause();
        } else if (GUILayout.Button("Play", guiStyle))
            backCam.Play();

        //GUILayout.Label("Location: " + transform.position, guiStyle);

    }
}