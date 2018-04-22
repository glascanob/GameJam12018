using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    public int moveSpeed = 5;
    public int rotationSpeed = 2;
    public float jumpForce = 10f;
    public float maxSpeed = 5;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Cast");
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("Slash");
        }
	}
	private void FixedUpdate()
	{
        float y = 0;
        //if (CrossPlatformInputManager.GetButtonDown("Jump"))
        //{
        //    y = jumpForce;
        //}
        float x = CrossPlatformInputManager.GetAxis("Horizontal");
        float z = CrossPlatformInputManager.GetAxis("Vertical");
        anim.SetFloat("Velocity", z);
        Vector3 inverseVector = transform.InverseTransformVector(GetComponent<Rigidbody>().velocity);
        if(Mathf.Approximately(z, 0))
        {
            GetComponent<Rigidbody>().velocity *= 0.5f;
            GetComponent<Rigidbody>().velocity = transform.TransformVector(new Vector3(0, inverseVector.y, 0));
        }
        Vector3 moveDirection = new Vector3(x, y, z);
        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0);
        Vector3 moveForce = transform.forward * z * moveSpeed;
        Vector3 jumpVector = transform.TransformVector(new Vector3(0, y, 0));
        Debug.Log("JumpVector " + jumpVector + " moveForce "+ moveForce);
        moveForce += jumpVector;
        Debug.Log( "moveForce " + moveForce);
        GetComponent<Rigidbody>().AddForce(moveForce * Time.deltaTime, ForceMode.VelocityChange);

        //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection)* moveSpeed * Time.deltaTime);
        y = 0;
    }
}
