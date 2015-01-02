using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------
	//--- Public Fields

	public float healthPoints = 100.0f;
	public float currentHealthPoints;
	public float incomingDamage = 0.0f;

	public int lootAmount = 1;
	public float dropOffset = 1.0f;
	public GameObject moneyPrefab;
	public GameObject shipExplosion = null;
	public AudioClip audioExplosion = null;


	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	//+++ Private Fields

	private bool isDead = false;
	private bool isAlmostDead = false;
	private bool amEnemy = false;
	private float invulnerable = 1.0f;

	private static SpawnEnemies spawner = null;

	//#################################################################################################
	//### UnityEngine

	void Start()
	{
		currentHealthPoints = healthPoints;

		if(gameObject.CompareTag("Enemy"))
		{
			amEnemy = true;
		}

		if(!spawner)
		{
			spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnEnemies>();
		}

	}


	void Update()
	{
		invulnerable -= Time.deltaTime;

		//	CheckForAlmostDead();
	}


	void OnGUI()
	{
		// show player health on screen (dont show for enemies)
		if(!amEnemy)
		{
			GUI.Box(new Rect(10.0f, Screen.height - 40.0f, 100.0f, 25.0f), "Life: " + currentHealthPoints + " / " + healthPoints);
		}

	}


	//****************************************************************************************************
	//*** Functions

	public void ReceiveDamage(float amount)
	{
		// if this is the players health, check if still in invulnerable time period
		if(!amEnemy)
		{
			// still invulnerable? -> do nothing
			if(invulnerable > 0.0f)
			{
				return;
			}
			else
			// else receive damage!
			{
				currentHealthPoints -= amount;

				// reset invulnerability time span
				invulnerable = Global.global.invulnerabilityTimeSpan;
				return;
			}
		}

		// decrease HP
		currentHealthPoints -= amount;

		// check if needs to be removed from global list of enemies
		CheckForAlmostDead();


		// check if dead:
		if(currentHealthPoints <= 0.0f)
		{
			// if this is the players health the game is over!
			if(!amEnemy)
			{
				Global.global.gameOver = true;
				return;
			}

			if(!isDead)
			{
				// if Object is an "Enemy"
				//			if(amEnemy)
				//			{
				// Money spawn position
				Vector3 pos = transform.position;
				pos.y = 2f;

				// spawn Object of type "Money"
				if(moneyPrefab != null)
				{
					// spawn 1 to "lootAmount" instances of money
					int randomLootAmount = (int)(Random.Range(1, lootAmount));

					for(int i = 0; i < randomLootAmount; i++)
					{
						Vector3 destPos = pos;
						destPos.x += Random.Range(-dropOffset, dropOffset);
						destPos.z += Random.Range(-dropOffset, dropOffset);

						GameObject m = Instantiate(moneyPrefab, pos, Quaternion.identity) as GameObject;
						m.GetComponent<Repositioner>().destination = destPos;
						m.transform.parent = Global.global.instanceFolder;

					}
					//				Debug.Log ("spawned MONEY!");
				}

				// EXPLOSION (particles)
				if(shipExplosion)
				{
					GameObject explosion = Instantiate(shipExplosion, transform.position, Quaternion.identity) as GameObject;
					explosion.transform.parent = Global.global.instanceFolder;

					// play explosion sound
					AudioSource.PlayClipAtPoint(audioExplosion, transform.position, 1.0f);

				}

				Die();

				// prevents this code to be triggered more than once
				isDead = true;

				//			}
			}
		}
	}


	public void CheckForAlmostDead()
	{
		if(isAlmostDead)
		{
			return;
		}

		//	if(amEnemy && currentHealthPoints - incomingDamage <= 0.0f)

		float futureHealth = currentHealthPoints - incomingDamage;
		if(futureHealth <= 0.0f)
		{
			// remove this enemy from global list of targetable enemies
			Global.global.targetableEnemies.Remove(gameObject);

			isAlmostDead = true;
			//		Debug.Log("removed enemy from list: " + gameObject.name.ToString());
		}
	}


	public void Die()
	{
		spawner.enemiesAlive--;

		Destroy(gameObject);
		//	Destroy(gameObject, 0.1f);
	}

}
