using UnityEngine;
using System.Collections;

public class ButtonStart : MonoBehaviour
{
	public float characterSize = 0.42f;
	public float zoomSize = 0.1f;
	public float zoomSpeed = 5.0f;

	private static TextMesh start = null ;
	private float timer;

	void Start()
	{
		start = this.GetComponent<TextMesh>();

	}


	void OnMouseEnter()
	{
		timer = 1.5f;

//Debug.Log ("MouseEnter");
	}


	void OnMouseOver()
	{
		timer += zoomSpeed * Time.deltaTime;

		if(!start)
Debug.Log ("No TextMesh found!");
		else
			start.characterSize = characterSize + zoomSize * Mathf.Cos(timer);


//Debug.Log ("MouseOver");
	}


	void OnMouseExit()
	{
		start.characterSize = characterSize;

//Debug.Log ("MouseExit");
	}


	void OnMouseDown()
	{
		Application.LoadLevel(1);
//		Application.Quit();
	}
	
}
