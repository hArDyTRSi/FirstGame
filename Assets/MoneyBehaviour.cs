﻿using UnityEngine;
using System.Collections;

public class MoneyBehaviour : MonoBehaviour {
	
	public float lifeTime = 10.0f;

	private bool removed = false;


	void Start ()
	{
	}
	

	void Update ()
	{
		float rotAmount = Time.time * 100f;

		transform.rotation = Quaternion.Euler(0, rotAmount, 0);

		if(removed)
		{
			Vector3 pos= transform.position;
			pos.y += 50f * Time.deltaTime;
			transform.position = pos;

			Color col = renderer.material.color;
/*			col.r *= lifeTime/2f;
			col.g *= lifeTime/2f;
			col.b *= lifeTime/2f;
*/		col.a *= lifeTime/2f;
			renderer.material.color = col;
		}

		lifeTime -= Time.deltaTime;
		if(lifeTime < 0.0f)
			Destroy(gameObject);

	}


	public void Remove()
	{
		if(!removed)
		{
			// Add Money to Players Money
			GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyTracker>().AddMoney(1);

			lifeTime = 2f;
			removed = true;
		}
	}

}
