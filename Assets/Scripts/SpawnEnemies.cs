using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
	
	public int howManyMobs = 100;
	
	
	public GameObject enemy;
	// Use this for initialization
	void Start ()
	{
		float range = GameObject.FindGameObjectWithTag("Level").GetComponent<GenerateLevel>().levelSize / 2;

		for(int i=0; i<howManyMobs; i++)
		{
/*
			//		Vector3 offset = new Vector3(Random.Range(-range, range), 0.0f, Random.Range(-range, range));
//			Instantiate(enemy, offset, Quaternion.identity);

			float distance = Random.Range(100.0f, range);
//			Vector3 angle = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
			Vector3 angle = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;
			Instantiate(enemy, angle * distance, Quaternion.identity);
*/
			bool sideOrUpways = System.Convert.ToBoolean((int)(Random.value*99.0f) % 2);
Debug.Log("sideOrUpways =" + sideOrUpways);
			bool upOrDown = System.Convert.ToBoolean((int)(Random.value*99.0f) % 2);
Debug.Log("upOrDown =" + upOrDown);
			bool leftOrRight = System.Convert.ToBoolean((int)(Random.value*99.0f) % 2);
Debug.Log("leftOrRight =" + leftOrRight);

			Vector3 startPosition = (sideOrUpways)?
				new Vector3((upOrDown)?-range:range,
				            0.0f,
				            Random.Range(-range, range)
				            ):
				new Vector3(Random.Range(-range, range),
				            0.0f,
				            (leftOrRight)?-range:range
				            );

			Vector3 destinatedPosition = (sideOrUpways)?
				new Vector3((upOrDown)?range:-range,
				            0.0f,
				            Random.Range(-range, range)
				            ):
				new Vector3(Random.Range(-range, range),
				            0.0f,
				            (leftOrRight)?range:-range
				            );

			GameObject e = Instantiate(enemy, startPosition, Quaternion.identity) as GameObject;
			e.GetComponent<EnemyAI>().destination = destinatedPosition;
//			e.GetComponent<EnemyAI>().movement = 0; // AtoB --> globalize AIMovement-enum to use here aswell!
		}
	}


/*	
	void Update()
	{

	}
*/
}
 