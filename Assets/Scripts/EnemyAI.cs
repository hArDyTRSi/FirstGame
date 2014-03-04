using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float speedRotation = 5.0f;
	public float speedMovement = 5.0f;

	public AIMovement movement = 0;
	public enum AIMovement
	{
		AtoB,
		FollowPlayer
	};

//	public Transform destination = null;
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
		if(player !=null)
		{
//Debug.Log("AI moving...");

			Quaternion lookRotation = Quaternion.identity;

//			int intMovement = (int)movement;

			switch((int)movement)
			{
			case 0:
				// Move to Destination
				lookRotation = Quaternion.LookRotation(destination - transform.position);

				if(lookRotation != Quaternion.identity)
					transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRotation * Time.deltaTime);

				// Move towards Destination
				transform.Translate(transform.forward * speedMovement * Time.deltaTime, Space.World);

				// Remove Enemy if having left the playfield
				if(Mathf.Abs(transform.position.x) > range + 1f || Mathf.Abs(transform.position.z) > range + 1f)
					Die();

				break;
			case 1:
				// Look at Player
				lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);
				
				if(lookRotation != Quaternion.identity)
					transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRotation * Time.deltaTime);
				
				// Move towards Player
				transform.Translate(transform.forward * speedMovement * Time.deltaTime, Space.World);
//Debug.DrawLine(player.transform.position, transform.position, Color.red);
				break;

/*			default:
				// Look at Player
				Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);
				
				if(lookRotation != Quaternion.identity)
					transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRotation * Time.deltaTime);
				
				// Move towards Player
				transform.Translate(transform.forward * speedMovement * Time.deltaTime, Space.World);
				//Debug.DrawLine(player.transform.position, transform.position, Color.red);
*/			}
		}
	}

	public void Die()
	{
		spawner.enemiesAlive--;
		Destroy(gameObject);
	}

}