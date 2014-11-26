using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour
{
//-------------------------------------------------------------------------------------------------
//--- Public Fields

public float healthPoints = 100.0f;
public float currentHealthPoints;
public float collisionDamage = 10.0f;
public int lootAmount = 1;
public float dropOffset = 1f;
public GameObject moneyPrefab;
public GameObject shipExplosion = null;
public AudioClip audioExplosion = null;


//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++ Private Fields

private bool isDead = false;
private bool amEnemy = false;
private float invulnerable = 1.0f;


//#################################################################################################
//### UnityEngine

void Start()
{
	currentHealthPoints = healthPoints;

	if(gameObject.CompareTag("Enemy"))
	{
		amEnemy = true;
	}
}


void Update()
{
	invulnerable -= Time.deltaTime;
}


void OnGUI()
{
	if(gameObject.CompareTag("Player"))
	{
		GUI.Box(new Rect(10f, Screen.height - 40f, 100f, 25f), "Life: " + currentHealthPoints + " / " + healthPoints);
	}
		
}


//****************************************************************************************************
//*** Functions

public void ReceiveDamage(float amount)
{
	// if this is the players health, check if still in invulnerable time period
//	if(!amEnemy)
//	{
	if(invulnerable > 0.0f)
	{
		return;
	}
	else
	{
		currentHealthPoints -= collisionDamage;
		invulnerable = Global.global.invulnerabilityTimeSpan;
	}
//	}

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
				// Money
				Vector3 pos = transform.position;
				pos.y = 2f;
			
				// spawn Object of type "Money"
				if(moneyPrefab != null)
				{ 
					int randomLootAmount = (int)(Random.Range(1, lootAmount));
				
					//						for(int i = 0; i<lootAmount; i++)
					for(int i = 0; i<randomLootAmount; i++)
					{
						/*							Vector3 offPos = pos;
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
			
				// EXPLOSION
				if(shipExplosion)
				{
					Instantiate(shipExplosion, transform.position, Quaternion.identity);
				
					AudioSource.PlayClipAtPoint(audioExplosion, transform.position, 1.0f);
				}
				//					currentHealthPoints = 0.0f;			// unneeded!
			
				gameObject.GetComponent<EnemyAI>().Die();
				//					Destroy(gameObject);
				//				Debug.Log(this.gameObject.name + "is DEAD!");
			
				// prevents this code to be triggered more than once
				isDead = true;
			
			
			}
		
		
		}
	}
}
}
