using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Target;
    public Transform World;


    private Rigidbody m_Rigidbody = null;


	void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate()
    {
        AlignToPlayer();
        m_Rigidbody.AddForce(transform.forward * 3.5f * Time.deltaTime, ForceMode.VelocityChange);
	}


    public void AlignToPlayer()
    {
        transform.LookAt(Target, transform.position - World.transform.position);
    }
}
