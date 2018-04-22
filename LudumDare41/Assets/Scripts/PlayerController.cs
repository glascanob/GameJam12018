﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    public int moveSpeed = 5;
    public int rotationSpeed = 2;
    public float jumpForce = 10f;
    public float maxSpeed = 5;
    public GameObject spellPrefab;
    public GameObject castPoint;
    public GameObject planetContainer;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            CastSpell();
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            SlashSword();
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
        moveForce += jumpVector;
        GetComponent<Rigidbody>().AddForce(moveForce * Time.deltaTime, ForceMode.VelocityChange);

        //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection)* moveSpeed * Time.deltaTime);
        y = 0;
    }
    public void CastSpell()
    {
        anim.SetTrigger("Cast");
        GameObject orbe = Instantiate(spellPrefab, castPoint.transform.position, castPoint.transform.rotation, planetContainer.transform);
        orbe.GetComponent<Rigidbody>().AddForce(orbe.transform.forward * 200f);
    }
    public void SlashSword()
    {
        anim.SetTrigger("Slash");
    }
}
