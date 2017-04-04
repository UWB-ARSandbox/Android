
using UnityEngine;


public class LookAround : MonoBehaviour
{

    private readonly Quaternion baseIdentity = Quaternion.Euler(90, 0, 0);

    private Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion cameraBase = Quaternion.identity;
    private Quaternion calibration = Quaternion.identity;
    private Quaternion baseOrientationRotationFix = Quaternion.identity;
    private Quaternion referanceRotation = Quaternion.identity;
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private bool debug = true;

    public Rigidbody thisRigidbody;
    public VirtualJoystick joystick;

    public Button_Create create;

    public Vector3 MoveVector;
    public float MoveSpeed;

    protected void Start() {
        Input.gyro.enabled = true;
        AttachGyro();


    }

    protected void Update() {
        transform.rotation = Quaternion.Slerp(transform.rotation, (ConvertRotation(referanceRotation * Input.gyro.attitude)), 0.1f);
        //transform.Translate(mjoystick.inputDirection);
        //transform.
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
        //thisRigidbody.MovePosition(transform.position + (MoveVector * MoveSpeed) );
        //transform.position = transform.position + (Vector3.Scale(MoveVector,transform.forward) * MoveSpeed);
        //transform.forward = transform.position;// + (MoveVector * MoveSpeed);
        //transform.position = transform.position + MoveVector * MoveSpeed;
        transform.Translate(MoveVector * MoveSpeed);
        
    }

    private void AttachGyro() {
        ResetBaseOrientation();
        UpdateCalibration(true);
        UpdateCameraBaseRotation(true);
        RecalculateReferenceRotation();
    }
    protected void OnGUI() {
        if (!debug)
            return;
        guiStyle.fontSize = 80; //change the font size
        //GUILayout.Label("Direction: " + transform.forward, guiStyle);
        //GUILayout.Label("Location: " + transform.position, guiStyle);

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

}
