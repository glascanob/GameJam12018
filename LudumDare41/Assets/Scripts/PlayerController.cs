using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5;
    public float rotationSpeed = 2;
    public float jumpForce = 10f;
    public float maxSpeed = 5;
    public float SpellCD = 5f;
    bool canCast = true;
    public float SwordCD = 0.5f;
    bool canSlash = true;
    public GameObject spellPrefab;
    public GameObject castPoint;
    public GameObject planetContainer;
    Animator anim;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            if (canCast)
            {
                canCast = false;
                canSlash = false;
                CastSpell();
            }
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            if (canSlash)
            {
                canSlash = false;
                canCast = false;
                SlashSword();
            }
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
        rb.angularVelocity = Vector3.zero;
        if(Mathf.Approximately(z, 0))
        {
            Vector3 inverseVector = transform.InverseTransformVector(GetComponent<Rigidbody>().velocity);
            GetComponent<Rigidbody>().velocity *= 0.5f;
            GetComponent<Rigidbody>().velocity = transform.TransformVector(new Vector3(0, inverseVector.y, 0));
        }
        //Vector3 moveDirection = new Vector3(x, y, z);
        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0);
        Vector3 moveForce = transform.forward * z * moveSpeed;
        Vector3 jumpVector = transform.TransformVector(new Vector3(0, y, 0));
        moveForce += jumpVector;
        rb.AddForce(moveForce * Time.deltaTime, ForceMode.VelocityChange);

        //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection)* moveSpeed * Time.deltaTime);
        y = 0;
    }
    public void CastSpell()
    {
        anim.SetTrigger("Cast");
        GameObject orbe = Instantiate(spellPrefab, castPoint.transform.position, castPoint.transform.rotation, planetContainer.transform);
        orbe.GetComponent<Rigidbody>().velocity = rb.velocity;
        orbe.GetComponent<Rigidbody>().AddForce(orbe.transform.forward * 200f);
        StartCoroutine("CastCDTime");
    }
    public void SlashSword()
    {
        anim.SetTrigger("Slash");
        StartCoroutine("SlashCDTime");
    }
    IEnumerator CastCDTime()
    {
        yield return new WaitForSeconds(0.5f);
        canSlash = true;
        yield return new WaitForSeconds(SpellCD);
        canCast = true;
    }
    IEnumerator SlashCDTime()
    {
        yield return new WaitForSeconds(0.5f);
        canCast = true;
        yield return new WaitForSeconds(SwordCD);
        canSlash = true;
    }
}
