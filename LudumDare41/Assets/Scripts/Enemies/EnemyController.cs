using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Target;
    public Transform World;
    public float velocity = 50;

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
            m_Rigidbody.angularVelocity = Vector3.zero;
            AlignToPlayer();
            m_Rigidbody.AddForce(transform.forward * velocity * Time.deltaTime, ForceMode.VelocityChange);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null)
            return;

        player.HurtPlayer(5.0f);
        m_Rigidbody.AddForce((transform.position - player.transform.position) * 20.0f, ForceMode.VelocityChange);
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
            //transform.localEulerAngles = new Vector3(90f, 0, 0);
            anim.SetTrigger("Dead");
            StartCoroutine("WaitToDy");

            PlayerController player = Target.GetComponentInChildren<PlayerController>();

            if (player != null)
                player.HealPlayer(7.5f);
        }
    }
    IEnumerator WaitToDy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
