using UnityEngine;
using System.Collections;

public class ButtonStart : MonoBehaviour
{
	public float zoomAmount = 3.0f;
	public float zoomSpeed	= 5.0f;

	private float timer;
	private TextMesh textMesh;


	void Start()
	{
		textMesh = this.GetComponent<TextMesh>();
	}


	void OnMouseEnter()
	{
		timer = 1.5f;
	}


	void OnMouseOver()
	{
		timer += zoomSpeed * Time.deltaTime;

		textMesh.offsetZ = -zoomAmount * Mathf.Cos(timer);
	}


	void OnMouseExit()
	{
		textMesh.offsetZ = 0.0f;
	}


	void OnMouseDown()
	{
		//	Global.global.gameOver = false;
		Application.LoadLevel(1);
	}

}
