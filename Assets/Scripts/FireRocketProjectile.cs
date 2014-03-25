using UnityEngine;
using System.Collections;

public class FireRocketProjectile : MonoBehaviour
{
	public float spawnProjectileOffset = 0.5f;
	public float fireDelay = 1.0f;

	private float fireTimer;

	public GameObject rocketPrefab;


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


	void Fire()
	{
		GameObject target = null;

		float closestDist = Mathf.Infinity;
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
		}

	}

}
