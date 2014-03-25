using UnityEngine;
using System.Collections;

public class ButtonQuit : MonoBehaviour
{
	public float zoomAmount = 2.5f;
	public float zoomSpeed = 5.0f;
	
	private float timer;
	private TextMesh tm;


	void Start()
	{
		tm = this.GetComponent<TextMesh>();
	}


	void OnMouseEnter()
	{
		timer = 1.5f;
		
	}
	
	
	void OnMouseOver()
	{
		timer += zoomSpeed * Time.deltaTime;
		
		tm.offsetZ = -zoomAmount * Mathf.Cos(timer);
	}
	

	void OnMouseExit()
	{
		tm.offsetZ = 0.0f;

	}

	
	void OnMouseDown()
	{
		Application.Quit();
	}
	
}
