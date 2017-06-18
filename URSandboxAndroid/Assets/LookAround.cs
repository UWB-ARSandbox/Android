
using UnityEngine;

/*
 * This code contains most of the code for all aspects of the player characters
 * The Quaternions are used in using the gyroscope in the phone and orienting it towards north
 * It takes the movement Vector from VirtualJoystick.cs in order to move depending on where the user is looking
 * An invisible "brain" is created to keep track of the players
 * 
 */

public class LookAround : MonoBehaviour
{

    private readonly Quaternion baseIdentity = Quaternion.Euler(90, 0, 0);

    private Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion cameraBase = Quaternion.identity;
    private Quaternion calibration = Quaternion.identity;
    private Quaternion baseOrientationRotationFix = Quaternion.identity;
    private Quaternion referanceRotation = Quaternion.identity;
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private bool debug = false;

    public Material nmaterial;
    public GameObject[] allHead;


    private GameObject brain;
    private GameObject head;
    private GameObject nose;
    public CreateHead CH;
    public bool HaveHead = false;
    public bool haveNose = false;

    private Vector3 forward;
    private Vector3 right;

    private Vector3 ranNum;

    public Rigidbody thisRigidbody;
    public VirtualJoystick joystick;
    public GameObject[] allhead;

    private float timeLeft;

    //public Button_Create create;

    public Vector3 MoveVector;
    public float MoveSpeed;
    WebCamTexture backCam;

    protected void Start() {
        Input.gyro.enabled = true;
        AttachGyro();
        forward = transform.right;
        right = transform.right;
        timeLeft = 3;


    }

    void OnJoinLobby() {
        brain = PhotonNetwork.Instantiate("Sphere1", Vector3.zero, Quaternion.identity, 0);
    }

    protected void Update() {

        if(head == null && brain != null) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                brain.GetComponent<PhotonView>().RPC("createHead", brain.GetComponent<PhotonView>().owner);
                timeLeft = 3f;
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, (ConvertRotation(referanceRotation * Input.gyro.attitude)), 0.1f);
        if (brain == null) {
            brain = PhotonNetwork.Instantiate("Sphere1", Vector3.zero, Quaternion.identity, 0);
        } else {
            brain.transform.position = transform.position;
        }
        if (head != null) {
            if (HaveHead) {
                head.transform.position = transform.position;
                if (haveNose) {
                    nose.transform.position = transform.position + transform.forward * .2f;
                } else {
                    haveNose = true;
                    nose = PhotonNetwork.Instantiate("Sphere 1", Vector3.zero, Quaternion.identity, 0);
                }
            } else if (CH.getTarget() != null) {
                head = CH.getTarget();
            }
            

        } else {
            HaveHead = false;
            haveNose = false;
            if (nose != null) {
                PhotonNetwork.Destroy(nose);
            }
        }

        
    }

    public void setHead(GameObject h) {
        head = h;
    }

    protected void FixedUpdate() {
        MoveVector = PoolInput();
        if (joystick.moving) {
            Move();
        }

    }

    private Vector3 PoolInput() {
        Vector3 dir = Vector3.zero;


        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();
        if (dir.magnitude > 1)
            dir.Normalize();

        return dir;
    }

    private void Move() {
        
        forward = transform.forward;
        forward.y = 0;  // zero out y, leaving only x & z

        forward *= MoveVector.z;
        right = transform.right;
        right.y = 0;  // zero out y, leaving only x & z

        right *= MoveVector.x;

        transform.position += forward * MoveSpeed;

        transform.position += right * MoveSpeed;
        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.z > 10 || transform.position.z < -10) {
            transform.position = new Vector3(0, 1, 0);
        }



    }

    private void AttachGyro() {
        ResetBaseOrientation();
        UpdateCalibration(true);
        UpdateCameraBaseRotation(true);
        RecalculateReferenceRotation();
    }
    

    private void UpdateCalibration(bool onlyHorizontal) {
        if (onlyHorizontal) {
            var fw = (Input.gyro.attitude) * (-Vector3.forward);
            fw.z = 0;
            if (fw == Vector3.zero) {
                calibration = Quaternion.identity;
            } else {
                calibration = (Quaternion.FromToRotation(baseOrientationRotationFix * Vector3.up, fw));
            }
        } else {
            calibration = Input.gyro.attitude;
        }
    }

    private void UpdateCameraBaseRotation(bool onlyHorizontal) {
        if (onlyHorizontal) {
            var fw = transform.forward;
            fw.y = 0;
            if (fw == Vector3.zero) {
                cameraBase = Quaternion.identity;
            } else {
                cameraBase = Quaternion.FromToRotation(Vector3.forward, fw);
            }
        } else {
            cameraBase = transform.rotation;
        }
    }

    private void RecalculateReferenceRotation() {
        referanceRotation = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);
    }
    private static Quaternion ConvertRotation(Quaternion q) {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private void ResetBaseOrientation() {
        baseOrientationRotationFix = Quaternion.identity;
        baseOrientation = baseOrientationRotationFix * baseIdentity;
    }

    [PunRPC]
    public void setLocation() {
        // dummy rpc
    }

    protected void OnGUI() {
        if (!debug)
            return;
        guiStyle.fontSize = 80; //change the font size
        if (backCam.isPlaying) {
            if (GUILayout.Button("Pause", guiStyle))
                backCam.Pause();
        } else if (GUILayout.Button("Play", guiStyle))
            backCam.Play();

    }

}
