using UnityEngine;
using System.Collections;

public class FireRocketProjectile : MonoBehaviour
{
//-------------------------------------------------------------------------------------------------
//--- Public Fields

public float spawnProjectileOffset = 0.5f;
public float fireDelay = 1.0f;
public float fireTimer = 1.0f;

public GameObject rocketPrefab;

public AudioClip audioRocketlaunch = null;

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++ Private Fields

//private float fireTimer;


//#################################################################################################
//### UnityEngine

void Start()
{
//	fireTimer = Random.Range(0f, fireDelay);
}

	
void Update()
{
	fireTimer -= Time.deltaTime;
	if(fireTimer < 0.0f)
	{
		fireTimer = fireDelay;
		Fire();
	}
}

//****************************************************************************************************
//*** Functions

void Fire()
{
	GameObject target = null;

	float closestDist = Mathf.Infinity;
		
//TODO: keep list of "enemiesAlive" (!!!) to avoid the next line! -> global
// "isDead" in HasHealth maybe useful

//	GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
//	foreach(GameObject t in targets)
	foreach(GameObject t in Global.global.enemiesAlive)
	{
		//TODO: watch out for missing objects in Global-enemiesAlive-list in inspector
		// make sure the object still exists
		// Theres a bug, sometimes an object gets missing, most likely when it leaves the playfield
		//   check EnemyAI.Movement_AtoB -> Die()
		if(t)
		{

			float dist = Vector3.Distance(transform.position, t.transform.position);
			if(dist < closestDist)
			{
				target = t;
				closestDist = dist;
			}
		}
			
	}

	if(target != null)
	{
		GameObject rocketInstance = Instantiate(rocketPrefab, transform.position + transform.forward * spawnProjectileOffset, transform.rotation) as GameObject;
		rocketInstance.GetComponent<RocketEngineSeeker>().destination = target;
		rocketInstance.GetComponent<OnHitEvent>().destination = target;

		// play launch-sound
		AudioSource.PlayClipAtPoint(audioRocketlaunch, transform.position, 1.0f);


		//TODO: check if rocket will kill enemy, if so remove it from "enemiesAlive"-list
		target.GetComponent<HasHealth>().realHealthPoints -= rocketInstance.GetComponent<OnHitEvent>().damage;

		if(target.GetComponent<HasHealth>().realHealthPoints <= 0.0f)
		{
			Global.global.enemiesAlive.Remove(target);
		}
/*
		float targetRealHealth = target.GetComponent<HasHealth>().realHealthPoints;

		targetRealHealth -= rocketInstance.GetComponent<OnHitEvent>().damage;
		if(targetRealHealth)
*/
	}

}

}
