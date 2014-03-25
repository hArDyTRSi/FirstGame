using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	
	public float shipSpeed = 3.0f;
	public float shipRotationSpeed = 10.0f;
	public int magnetLevel = 1;
	public float magnetSpeed = 1.0f;
	public float camRotationX = 22.5f;
	public float camDistance = 75.0f;

	static Camera playerCamera = null;
	static GameObject shipModel = null;
	
	private float range = 0.0f;


	void Start()
	{
		playerCamera = Camera.main;
		playerCamera.transform.localPosition = new Vector3(0.0f, camDistance, -camDistance);
		shipModel = GameObject.FindGameObjectWithTag("ShipModel");
		range = GameObject.FindGameObjectWithTag("Level").GetComponent<GenerateLevel>().levelSize / 2;
	}


	void Update()
	{
		// Check for MousePosition vs. World
		Plane playerPlane = new Plane(Quaternion.Euler(camRotationX, 0, 0) * Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hitdist = 0.0f;
		if(playerPlane.Raycast(ray, out hitdist))
		{
			Vector3 targetPoint = ray.GetPoint(hitdist);
			targetPoint.y = 0;

			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

			if(shipModel != null)
			{
				// Rotate towards MouseCursor
//				shipModel.transform.rotation = Quaternion.Euler (-90f, 0f, 0f) * shipModel.transform.rotation;
				shipModel.transform.rotation = Quaternion.Slerp(shipModel.transform.rotation, targetRotation, shipRotationSpeed * Time.deltaTime);
//				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, shipRotationSpeed * Time.deltaTime);

				// Move towards MouseCursor
				transform.Translate(shipModel.transform.forward * shipSpeed * Time.deltaTime * (Mathf.Clamp(hitdist - camDistance * 1.4f, 0, 20) / 10), Space.World);
//				transform.Translate(transform.forward * shipSpeed * Time.deltaTime * (Mathf.Clamp(hitdist-camDistance*1.4f, 0f, 20f) / 10f), Space.World);
			}	
		}


		// Clip Player-Position to Level-Size
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -range, range),
	                                		transform.position.y,
		                                 	Mathf.Clamp(transform.position.z, -range, range));


		CollectMoney();

	}

	void CollectMoney()
	{
		// Find all Objects with Tag="Money" and collect if close enough
		GameObject[] collectable = GameObject.FindGameObjectsWithTag("Money");
		foreach(GameObject c in collectable)
		{
			float dist = Vector3.Distance(transform.position, c.transform.position);

			// MAGNET
			if(dist < 25f * magnetLevel)
			{
				if(!c.GetComponent<MoneyBehaviour>().removed)
					c.transform.position = Vector3.Lerp(c.transform.position, transform.position, Time.deltaTime * magnetSpeed);
			}
			
			if(dist < 10f)
			{
				c.GetComponent<MoneyBehaviour>().Remove();
			}
		}

	}
}
