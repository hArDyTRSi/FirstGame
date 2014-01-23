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
//		Vector3 offset = new Vector3(Random.Range(-range, range), 0.0f, Random.Range(-range, range));
//			Instantiate(enemy, offset, Quaternion.identity);

			float distance = Random.Range(100.0f, range);
//			Vector3 angle = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
			Vector3 angle = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;
			Instantiate(enemy, angle * distance, Quaternion.identity);
		}
	}
/*	
	void Update()
	{

	}
*/
}
 