using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float speedRotation = 5.0f;
	public float speedMovement = 5.0f;

	public AIMovement movement = 0;
	public enum AIMovement
	{
		AtoB,
		FollowPlayer
	}

	public Vector3 destination = Vector3.zero;

	private float range = 0.0f;
	private GameObject player = null;
	private SpawnEnemies spawner = null;

//##############################################
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		range = GameObject.FindGameObjectWithTag("Level").GetComponent<GenerateLevel>().levelSize / 2.0f;
		spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnEnemies>();
	}

//##############################################
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
//Debug.Log("AI moving...");
		}
	}

//##############################################
//#### FUNCTIONS
	void Movement_AtoB()
	{
		Quaternion lookRotation;// = Quaternion.identity;
		lookRotation = Quaternion.LookRotation(destination - transform.position);

		if(lookRotation != Quaternion.identity)
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRotation * Time.deltaTime);

		// Move towards Destination
		transform.Translate(transform.forward * speedMovement * Time.deltaTime, Space.World);

		// Remove Enemy if having left the playfield
		if(Mathf.Abs(transform.position.x) > range + 1f || Mathf.Abs(transform.position.z) > range + 1f)
			Die();
	}

	void Movement_FollowPlayer()
	{
		Quaternion lookRotation;// = Quaternion.identity;
		// Look at Player
		lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);

		if(lookRotation != Quaternion.identity)
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRotation * Time.deltaTime);

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