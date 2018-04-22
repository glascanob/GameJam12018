using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {
    public Rigidbody rgbd;
    public GameObject fieryParticle;
    public GameObject smokeParticle;
    public GameObject explosionParticle;
    bool notHit = true;
    // Use this for initialization
	void Start () {
        StartCoroutine("LifeTimeCD");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (notHit)
        {
            rgbd.AddForce(transform.forward * 5f * Time.deltaTime);
        }
        else{
            rgbd.velocity = Vector3.zero;
        }
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DeadState();
            ExplodeOnEnemy();
        }
        else{
            Debug.Log("enter!!!");
            rgbd.velocity = Vector3.zero;
            ExplodeOnEnemy();
        }
	}
    IEnumerator ExplodeOnEnemy()
    {
        Explode();
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => !explosionParticle.GetComponent<ParticleSystem>().isPlaying);
        Destroy(gameObject);
    }
    IEnumerator LifeTimeCD()
    {
        yield return new WaitForSeconds(3f);
        Explode();
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => !explosionParticle.GetComponent<ParticleSystem>().isPlaying);
        Destroy(gameObject);
    }
    void Explode()
    {
        notHit = false;
        rgbd.constraints = RigidbodyConstraints.FreezeAll;
        fieryParticle.SetActive(false);
        smokeParticle.SetActive(false);
        explosionParticle.SetActive(true);
    }
}
