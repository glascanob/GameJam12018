using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {
	// Use this for initialization
	void Start () {
        StartCoroutine("LifeTimeCD");
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddForce(transform.forward * 5f * Time.deltaTime);
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DeadState();
            Destroy(gameObject);
        }
	}
    IEnumerator LifeTimeCD()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
