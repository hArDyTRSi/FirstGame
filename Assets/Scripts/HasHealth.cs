using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour
{
//-------------------------------------------------------------------------------------------------
//--- Public Fields

public float healthPoints = 100.0f;
public float currentHealthPoints;
public float realHealthPoints;

public int lootAmount = 1;
public float dropOffset = 1f;
public GameObject moneyPrefab;
public GameObject shipExplosion = null;
public AudioClip audioExplosion = null;

//TODO: check why this is autoformatted that strange!
//TODO: Turn on Sound again!!!!
[Range(0.0f,1.0f)]
public float
	audioVolume = 1.0f;

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++ Private Fields

private bool isDead = false;
private bool amEnemy = false;
private float invulnerable = 1.0f;

private static SpawnEnemies spawner = null;

//#################################################################################################
//### UnityEngine

void Start()
{
	currentHealthPoints = healthPoints;
	realHealthPoints = healthPoints;

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
/*
	if(amEnemy && realHealthPoints <= 0.0f)
//	if(realHealthPoints <= 0.0f)
	{
		// remove this enemy from global list of enemies
		Global.global.enemiesAlive.Remove(gameObject);
	}
*/
}


void OnGUI()
{
//	if(gameObject.CompareTag("Player"))
	if(!amEnemy)
	{
		GUI.Box(new Rect(10f, Screen.height - 40f, 100f, 25f), "Life: " + currentHealthPoints + " / " + healthPoints);


		AudioListener.volume = audioVolume;
/*
		if(AudioListener.volume != 0)
		{
			if(GUI.Button(new Rect(10, 10, 60, 30), "Off"))
			{
				AudioListener.volume = 0;
			}
		}
		else
		if(AudioListener.volume == 0)
		{
			if(GUI.Button(new Rect(10, 10, 60, 30), "On"))
			{
				AudioListener.volume = 100;
			}
		}
*/	
	}

}


//****************************************************************************************************
//*** Functions

public void ReceiveDamage(float amount)
{
	// if this is the players health, check if still in invulnerable time period
	if(!amEnemy)
	{
		if(invulnerable > 0.0f)
		{
			return;
		}
		else
		{
//			currentHealthPoints -= collisionDamage;
			currentHealthPoints -= amount;
			invulnerable = Global.global.invulnerabilityTimeSpan;
			return;
		}
	}

	// decrease HP
	currentHealthPoints -= amount;

	// if Enemy dead:
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
			//				if(gameObject.CompareTag("Enemy"))
			if(amEnemy)
			{
				// Money spawn position
				Vector3 pos = transform.position;
				pos.y = 2f;
			
				// spawn Object of type "Money"
				if(moneyPrefab != null)
				{
					// spawn 1 to "lootAmount" instances of money
					int randomLootAmount = (int)(Random.Range(1, lootAmount));
				
//					for(int i = 0; i<lootAmount; i++)
					for(int i = 0; i<randomLootAmount; i++)
					{
/*						Vector3 offPos = pos;
						pos.x += Random.Range(-dropOffset, dropOffset);
						pos.z += Random.Range(-dropOffset, dropOffset);
							
						Instantiate(moneyPrefab, offPos, Quaternion.identity);
*/
						Vector3 destPos = pos;
						destPos.x += Random.Range(-dropOffset, dropOffset);
						destPos.z += Random.Range(-dropOffset, dropOffset);
					
						GameObject m = Instantiate(moneyPrefab, pos, Quaternion.identity) as GameObject;
						m.GetComponent<Repositioner>().destination = destPos;
					}
//				Debug.Log ("spawned MONEY!");
				}
			
				// EXPLOSION (particles)
				if(shipExplosion)
				{
					Instantiate(shipExplosion, transform.position, Quaternion.identity);
				
					//TODO: maybe set loudness of explosion to amount of damage
					//       if 3D-Audio does not work out!
					// play explosion sound
					AudioSource.PlayClipAtPoint(audioExplosion, transform.position, 1.0f);
				}
				
				//TODO: check if code in "Die()" can be executed from here!
//				gameObject.GetComponent<EnemyAI>().Die();
				Die();

				// prevents this code to be triggered more than once
				isDead = true;

			}
		}
	}
}


private void Die()
{
	spawner.enemiesAlive--;
		
	// remove this enemy from global list of enemies
	//	Global.global.enemiesAlive.Remove(gameObject);
		
	Destroy(gameObject);
	//	Destroy(gameObject, 0.1f);
}

}
