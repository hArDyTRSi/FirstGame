using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------
	//--- Public Fields

	public int howManyMobs = 20;
	public int enemiesAlive;

	public GameObject enemy = null;


	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	//+++ Private Fields

	private float range;


	//#################################################################################################
	//### UnityEngine

	void Start()
	{
		range = Global.global.levelSize / 2.0f;

		for(int i=0; i<howManyMobs; i++)
		{
			SpawnEnemy();
		}
	}


	void Update()
	{
		if(enemiesAlive < howManyMobs)
		{
			SpawnEnemy();
		}
	}


	void OnGUI()
	{
		// show how many enemies are in the scene
		GUI.Box(new Rect(10.0f, Screen.height / 2.0f, 100.0f, 25.0f), "Enemies: " + enemiesAlive);
	}


	//****************************************************************************************************
	//*** Functions

	void SpawnEnemy()
	{
		// find a spawn position
		bool sideOrUpways = System.Convert.ToBoolean((int)(Random.value * 99.0f) % 2);
		bool upOrDown = System.Convert.ToBoolean((int)(Random.value * 99.0f) % 2);
		bool leftOrRight = System.Convert.ToBoolean((int)(Random.value * 99.0f) % 2);

		Vector3 startPosition = (sideOrUpways) ?
			new Vector3((upOrDown) ? -range : range,
							0.0f,
							Random.Range(-range, range)
		) :
			new Vector3(Random.Range(-range, range),
							0.0f,
							(leftOrRight) ? -range : range
		);

		Vector3 destinatedPosition = (sideOrUpways) ?
			new Vector3((upOrDown) ? range : -range,
							0.0f,
							Random.Range(-range, range)
		) :
			new Vector3(Random.Range(-range, range),
							0.0f,
							(leftOrRight) ? range : -range
		);

		// spawn at newly found position
		GameObject enemyInstance = Instantiate(enemy, startPosition, Quaternion.identity) as GameObject;
		enemyInstance.GetComponent<EnemyAI>().destination = destinatedPosition;
		enemyInstance.transform.parent = Global.global.instanceFolder;

		// keep track of number of enemies in the scene
		enemiesAlive++;

		// add this enemy to the global list of targetable enemies
		Global.global.targetableEnemies.Add(enemyInstance);
		//	Debug.Log("added enemy to list");

	}
}
