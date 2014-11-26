using UnityEngine;
using System.Collections;

public class FireRocketProjectile : MonoBehaviour
{
//-------------------------------------------------------------------------------------------------
//--- Public Fields

public float spawnProjectileOffset = 0.5f;
public float fireDelay = 1.0f;

public GameObject rocketPrefab;


//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++ Private Fields

private float fireTimer;


//#################################################################################################
//### UnityEngine

void Start()
{
	

	// TODO:	keep track of which # of instance this is and take
	//			#instance / #launchers * fireDelay
	//			as initial delay ;)
	// static #instances = 0;	-> add #1 for every instance of this class at Awake();
	// private #thisinstance;	-> move #instances here at Awake();


	fireTimer = Random.Range(0f, fireDelay);
//Debug.Log ("Delay = " + fireTimer);
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

	GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
	foreach(GameObject t in targets)
	{
		float dist = Vector3.Distance(transform.position, t.transform.position);
		if(dist < closestDist)
		{
			target = t;
			closestDist = dist;
		}
	}

	if(target != null)
	{
		GameObject rocketInstance = Instantiate(rocketPrefab, transform.position + transform.forward * spawnProjectileOffset, transform.rotation) as GameObject;
		rocketInstance.GetComponent<RocketEngineSeeker>().destination = target;
		rocketInstance.GetComponent<OnHitEvent>().destination = target;
//			count++;
//			actualCount++;

		//TODO: check if rocket will kill enemy, if so, set its deathFlag and remove it from "enemiesAlive"
	}

}

}
