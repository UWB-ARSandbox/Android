using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour {


    public Vector3 MoveVector;
    public float MoveSpeed;
    public VirtualJoystick joystick;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void FixedUpdate()
    {
        MoveVector = PoolInput();

        Move();
    }

    private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;


        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();

        if (dir.magnitude > 1)
            dir.Normalize();

        return dir;
    }

    private void Move()
    {
        //thisRigidbody.MovePosition(transform.position + (MoveVector * MoveSpeed) );
        transform.position = transform.position + (Vector3.Scale(MoveVector, transform.forward) * MoveSpeed);
        //transform.forward = transform.position;// + (MoveVector * MoveSpeed);
    }
}
