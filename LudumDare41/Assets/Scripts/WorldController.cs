using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {
    public GameObject world;
    Rigidbody[] gravityObjects;
    public int pullForce = 1;
	// Use this for initialization
	void Start () {
        gravityObjects = world.GetComponentsInChildren<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
	private void FixedUpdate()
	{
        foreach (Rigidbody objectOnWorld in gravityObjects)
        {
            Vector3 forceDirection =  objectOnWorld.transform.position - transform.position;
            Vector3 objectUp = objectOnWorld.transform.up;
            objectOnWorld.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            Quaternion objectRotation = Quaternion.FromToRotation(objectUp, forceDirection) * objectOnWorld.transform.rotation;
            objectOnWorld.transform.rotation = Quaternion.Slerp(objectOnWorld.transform.rotation, objectRotation, 50f * Time.deltaTime);
        }
	}
}
