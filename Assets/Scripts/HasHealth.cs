using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour {

	public float healthPoints = 100.0f;
	public float currentHealthPoints;

	public int lootAmount = 1;
	public float dropOffset = 1f;
	public GameObject moneyPrefab;
	public GameObject shipExplosion = null;

	private bool isDead = false;


	void Start()
	{
			currentHealthPoints = healthPoints;
	}
/*	
	void Update()
	{
	
	}
*/


	public void ReceiveDamage(float amount)
	{
		// decrease HP
		currentHealthPoints -= amount;

		// if Enemy dead:
		if(currentHealthPoints <= 0.0f)
		{
			if(!isDead)
			{
				// if Object is an "Enemy"
				if(gameObject.CompareTag("Enemy"))
				{
					// Money
					Vector3 pos = transform.position;
					pos.y = 2f;

					// spawn Object of type "Money"
					if(moneyPrefab != null)
					{
						int randomLootAmount = (int)(Random.Range (1f, lootAmount));

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
						Instantiate(shipExplosion, transform.position, Quaternion.identity);

//					currentHealthPoints = 0.0f;			// unneeded!

					gameObject.GetComponent<EnemyAI>().Die();
//					Destroy(gameObject);
					//				Debug.Log(this.gameObject.name + "is DEAD!");
					
					// prevents this code to be triggered more than once
					isDead = true;

				
				}
			
			
			}
		}

//		else
//Debug.Log(this.gameObject.name + "has " + this.currentHealthPoints + " Health left. ");

	}

	void OnGUI()
	{
		if(gameObject.CompareTag("Player"))
			GUI.Box(new Rect(10f, Screen.height - 40f, 100f,25f), "Life: " + currentHealthPoints + " / " + healthPoints);

	}
}
