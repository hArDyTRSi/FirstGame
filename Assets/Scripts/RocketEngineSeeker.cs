	using UnityEngine;
using System.Collections;

public class RocketEngineSeeker : MonoBehaviour
{
//-------------------------------------------------------------------------------------------------
//--- Public Fields

//	public int moveSpeed = 1;
public float rotSpeed = 5.0f;
public float rotSpeedPlus = 5.0f;
public float rocketSpeed = 1.0f;
public float lifeTime = 10.0f;

//	public float rocketSlowDown = 20.0f;
public float rocketSpeedUp = 1.3333333f;
public float rocketSpeedMax = 50.0f;
public float rocketSpeedLaunchExtra = 50.0f;
	
//	FireRocketProjectile frp;
	
public GameObject destination = null;


//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++ Private Fields


//#################################################################################################
//### UnityEngine

/*	
void Awake()
{
}
*/


void Start()
{
//		transform.rotation = Quaternion.Euler (0f, 0f, 180f) * transform.rotation;
//		transform.rotation = Quaternion.Euler (0f, -90f, 0f) * transform.rotation;

		
//		frp = GameObject.FindGameObjectWithTag("Player").GetComponent<FireRocketProjectile>();

//		if(destination == null)
//			Destroy(gameObject);
}

	
void Update()
{
		
//Debug.Log(destination.name + "moving...");


	// make more rotateable over time after launch
	rotSpeed += rotSpeedPlus * Time.deltaTime;


	//  speed up Rockets up to maximum speed
	rocketSpeed *= rocketSpeedUp;
	if(rocketSpeed >= rocketSpeedMax)
	{
		rocketSpeed = rocketSpeedMax;
	}


	rocketSpeedLaunchExtra -= Time.deltaTime * 1000.0f;
	if(rocketSpeedLaunchExtra < 0.0f)
	{
		rocketSpeedLaunchExtra = 0.0f;
	}


	// Move towards Target
	float rocketSpeedActual = rocketSpeed + rocketSpeedLaunchExtra;

	if(destination != null)
	{
		// rotate towards Target
		//			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination.transform.position - transform.position), rotSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination.transform.position - transform.position), rotSpeed * Time.deltaTime);
			
//			transform.rotation = Quaternion.Euler(90,0,0) * transform.rotation;

		transform.Translate(transform.forward * rocketSpeedActual * Time.deltaTime, Space.World);
//			transform.Translate(transform.up * rocketSpeedActual * Time.deltaTime, Space.World);

			
			

			
//Debug.DrawLine(destination.transform.position, transform.position, Color.yellow);
	}
	else
	{
//			Destroy(gameObject);
//			frp.actualCount--;
		transform.Translate(transform.forward * rocketSpeedActual * Time.deltaTime, Space.World);
	}
		
		
	lifeTime -= Time.deltaTime;
		
	if(lifeTime < 0.0f)
	{
		Destroy(gameObject);
	}

}


//****************************************************************************************************
//*** Functions


}