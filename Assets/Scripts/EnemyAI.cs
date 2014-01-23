using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float speedRotation = 5.0f;
	public float speedMovement = 5.0f;

	GameObject player = null;
	

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	
	}
	
	
	void Update()
	{
		if(player !=null)
		{
//Debug.Log("AI moving...");
			
			// Look at Player
			Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);

			if(lookRotation != Quaternion.identity)
				transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speedRotation * Time.deltaTime);

			// Move towards Player
			transform.Translate(transform.forward * speedMovement * Time.deltaTime, Space.World);
//Debug.DrawLine(player.transform.position, transform.position, Color.red);
		}
		
	}
}
