using UnityEngine;
using System.Collections;

public class OnHitEvent : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------
	//--- Public Fields

	public float damage = 50.0f;

	public GameObject effectPrefab;

	public AudioClip audioExplosion;

	public GameObject destination = null;
	public HasHealth destinationHealth = null;


	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	//+++ Private Fields

	//private bool detonated = false;


	//#################################################################################################
	//### UnityEngine

	/*
	void Start()
	{
	}
	*/
	/*	
	void FixedUpdate()
	{
	//	Ray ray = new Ray(transform.position, transform.forward);
	//	if(Physics.Raycast(ray, re.rocketSpeed * Time.deltaTime))
	//		Detonate();
	}
	*/

	/*
	void OnCollisionEnter()
	{
		Detonate();
	Debug.Log ("OnCollisionEnter");
	}
	*/

	/*
	void OnTriggerEnter(Collider objectHit)
	{
	//Debug.Log("Hit Object of type " + objectHit.tag);
	//	if(objectHit.tag != "Bullet")
	//	if(objectHit.tag != "RocketProjectile")
	//	if(objectHit.tag == "Enemy")
		if(objectHit.CompareTag("EnemyModel"))
			Detonate();

	//	Debug.Log("OnTriggerEnter");
	}
	*/


	//****************************************************************************************************
	//*** Functions

	public void Detonate()
	{

		Vector3 pos = transform.position;
		pos.y = 2.0f;

		// explosion -> particles
		if(effectPrefab != null)
		{
			GameObject explosionInstance = Instantiate(effectPrefab, pos, Quaternion.identity) as GameObject;
			explosionInstance.transform.parent = Global.global.instanceFolder;
		}


		// SOUND
		AudioSource.PlayClipAtPoint(audioExplosion, transform.position, 1.0f);


		// Decrease Health of Destination
		if(destination)
		{
			HasHealth h = destination.GetComponent<HasHealth>();
			if(h != null)
			{
				h.ReceiveDamage(damage);
			}
		}

		// Destroy Rocket
		Destroy(gameObject);
	}
}