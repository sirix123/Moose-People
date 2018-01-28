using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAttack : MonoBehaviour {

	public float projectileSpeed;
	public float timeBetweenAttack;

	public Transform spawn;

	float timer;

	void Update () 
	{
		timer += Time.deltaTime;
		
			if (Input.GetMouseButtonDown(0) & timer > timeBetweenAttack)
			{
				timer = 0f;
				Fire();
			}
		
	}

	void Fire() 
	{
		GameObject seaBolt = PooledObjectsGeneric.SharedInstance.GetPooledObject("Seabolt");

        if (seaBolt != null)
   		{
			Rigidbody rb = seaBolt.GetComponent<Rigidbody>();

			rb.transform.position = spawn.transform.position;
			rb.transform.rotation = spawn.transform.rotation;

			rb.AddForce(transform.forward * projectileSpeed);

			seaBolt.SetActive(true);
		}
	}
}
