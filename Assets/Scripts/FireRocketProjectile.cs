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
//public GameObject audioRocketlaunch = null;	// change, so this can be instantiated in "_INSTANCES"-folder

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
		
	foreach(GameObject t in Global.global.targetableEnemies)
	{
		if(t != null)
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
		rocketInstance.transform.parent = Global.global.instanceFolder;


		// play launch-sound
		//TODO: instantiate as prefab
		AudioSource.PlayClipAtPoint(audioRocketlaunch, transform.position, 1.0f);
//		GameObject audio = Instantiate(audioRocketlaunch, transform.position, Quaternion.identity);
//		audio.transform.parent = Global.global.instanceFolder;


		// check if rocket will kill enemy, if so remove it from "enemiesAlive"-list
		HasHealth targetHasHealth = target.GetComponent<HasHealth>();
		targetHasHealth.incomingDamage += rocketInstance.GetComponent<OnHitEvent>().damage;
		targetHasHealth.CheckForAlmostDead();

	}

}

}
