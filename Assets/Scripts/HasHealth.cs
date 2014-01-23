using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour {

	public float healthPoints = 100.0f;
	public float currentHealthPoints;

	public GameObject moneyPrefab;

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
		currentHealthPoints -= amount;
		if(currentHealthPoints <= 0.0f)
		{
			if(!isDead)
			{
				// if Object is an "Enemy", spawn Object of type "Money"
	//			if(gameObject.GetComponent<EnemyAI>() != null)
				if(gameObject.CompareTag("Enemy"))
				{
					// Money
					Vector3 pos = transform.position;
					pos.y = 2f;

					if(moneyPrefab != null)
			//			Instantiate(moneyPrefab, transform.position + transform.forward, Quaternion.identity);
						Instantiate(moneyPrefab, pos, Quaternion.identity);

//				Debug.Log ("spawned MONEY!");
				}
			
				currentHealthPoints = 0.0f;			// unneeded!
				Destroy(gameObject);
//				Debug.Log(this.gameObject.name + "is DEAD!");

				isDead = true;
			
			}
		}

//		else
//Debug.Log(this.gameObject.name + "has " + this.currentHealthPoints + " Health left. ");

	}
}
