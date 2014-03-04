using UnityEngine;
using System.Collections;

public class OnHitEvent : MonoBehaviour
{
	public float damage = 50.0f;
//	public float explosionRadius = 20.0f;

	public GameObject effectPrefab;

	public AudioClip audioExplosion;
	
//	FireRocketProjectile frp = null;
	public GameObject destination = null;

	private bool detonated = false;
	
	void Start()
	{
//		frp = GameObject.FindGameObjectWithTag("Player").GetComponent<FireRocketProjectile>();
//		re = frp.GetComponent<RocketEngine>();
	}
	
/*	
	void FixedUpdate()
	{
//		Ray ray = new Ray(transform.position, transform.forward);
//		if(Physics.Raycast(ray, re.rocketSpeed * Time.deltaTime))
//			Detonate();

	}
*/

/*
	void OnCollisionEnter()
	{
		Detonate();
Debug.Log ("OnCollisionEnter");
	}
*/
	void OnTriggerEnter(Collider objectHit)
	{
//Debug.Log("Hit Object of type " + objectHit.tag);
//		if(objectHit.tag != "Bullet")
//		if(objectHit.tag != "RocketProjectile")
//		if(objectHit.tag == "Enemy")
		if(objectHit.CompareTag("EnemyModel"))
				Detonate();
//Debug.Log ("OnTriggerEnter");
	}

	void Detonate()
	{

		if(!detonated)
		{
			Vector3 pos = transform.position;
			pos.y = 2f;

			// explosion
			if(effectPrefab != null)
				Instantiate(effectPrefab, pos, Quaternion.identity);

			detonated = true;
		}

//		!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 
//		PUT ME BACK!!!!!
//		AudioSource.PlayClipAtPoint(explosion, transform.position, 1.0f);
		
		// Decrease count of active Rockets
//		frp.actualCount--;

		// Decrease Health of Destination
		if(destination)
		{
			HasHealth h = destination.GetComponent<HasHealth>();
			if(h !=null)
			{
				h.ReceiveDamage(damage);
			}
		}

		// Destroy Rocket
 		Destroy(gameObject);
	}
}