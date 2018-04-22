using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Target;
    public Transform World;

    private Rigidbody m_Rigidbody = null;
    Animator anim;
    bool following = true;

	void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
	}
	
	void FixedUpdate()
    {
        if (following)
        {
            AlignToPlayer();
            m_Rigidbody.AddForce(transform.forward * 3.5f * Time.deltaTime, ForceMode.VelocityChange);
        }
	}


    public void AlignToPlayer()
    {
        transform.LookAt(Target, transform.position - World.transform.position);
    }

    public void DeadState()
    {
        if (following)
        {
            m_Rigidbody.velocity = Vector3.zero;
            following = false;
            transform.localEulerAngles = new Vector3(90f, 0, 0);
            anim.SetTrigger("Dead");
            StartCoroutine("WaitToDy");
        }
    }
    IEnumerator WaitToDy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
