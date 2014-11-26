using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
//-------------------------------------------------------------------------------------------------
//--- Public Fields

public float speedRotation = 5.0f;
public float speedMovement = 5.0f;

public enum AIMovement
{
	AtoB,
	FollowPlayer
}
public AIMovement movement = 0;

public Vector3 destination = Vector3.zero;


//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++ Private Fields

private float range = 0.0f;
private GameObject player = null;
private HasHealth playerHealth = null;
private HasHealth health = null;
private SpawnEnemies spawner = null;


//#################################################################################################
//### UnityEngine

void Start()
{
	player = GameObject.FindGameObjectWithTag("Player");
	playerHealth = player.GetComponent<HasHealth>();
	health = gameObject.GetComponent<HasHealth>();
	range = Global.global.levelSize / 2.0f;
	spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnEnemies>();
}


void Update()
{
	if(player != null)
	{
		switch((int)movement)
		{
			default:
			case 0:		// AtoB
				Movement_AtoB();
				break;
			case 1:		// FollowPlayer
				Movement_FollowPlayer();
				break;
		}
	}
}


void OnTriggerEnter(Collider objectHit)
{
	// collided with player
	if(objectHit.CompareTag("Player"))
	{
		// receive collision damage
		health.ReceiveDamage(Global.global.collisionDamage);

		// let player receive collision damage
		playerHealth.ReceiveDamage(Global.global.collisionDamage);
	}


	// got hit by rocket
	if(objectHit.CompareTag("RocketProjectile"))
	{
		objectHit.gameObject.GetComponent<OnHitEvent>().Detonate();
	}

	// got hit by laser

	
}


//****************************************************************************************************
//*** Functions

void Movement_AtoB()
{
	Quaternion lookRotation = Quaternion.LookRotation(destination - transform.position);

	if(lookRotation != Quaternion.identity)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRotation * Time.deltaTime);
	}

	// Move towards Destination
	transform.Translate(transform.forward * speedMovement * Time.deltaTime, Space.World);

	// Remove Enemy if having left the playfield
	if(Mathf.Abs(transform.position.x) > range + 1f || Mathf.Abs(transform.position.z) > range + 1f)
	{
		Die();
	}
}

void Movement_FollowPlayer()
{
	Quaternion lookRotation;// = Quaternion.identity;
	// Look at Player
	lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);

	if(lookRotation != Quaternion.identity)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRotation * Time.deltaTime);
	}

	// Move towards Player
	transform.Translate(transform.forward * speedMovement * Time.deltaTime, Space.World);
//Debug.DrawLine(player.transform.position, transform.position, Color.red);
}


public void Die()
{
	spawner.enemiesAlive--;
	Destroy(gameObject);
}

}