using UnityEngine;
using System.Collections;

public class Repositioner : MonoBehaviour
{

	public float repositionSpeed = 1f;
	public Vector3 destination = Vector3.zero;

/*	void Start ()
	{
	
	}
*/	

	void Update ()
	{
	
		transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * repositionSpeed);
	}
}
