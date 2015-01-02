using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------
	//--- Public Fields

	public Material material;


	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	//+++ Private Fields


	//#################################################################################################
	//### UnityEngine

	void Start()
	{
		GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//ground.transform.localScale = new Vector3(levelSize, 1.0f, levelSize);
		ground.transform.localScale = new Vector3(Global.global.levelSize, 1.0f, Global.global.levelSize);
		//		ground.transform.localScale = new Vector3(Global.levelSize, 1.0f, Global.levelSize);
		ground.collider.enabled = false;
		ground.renderer.material = material;
		ground.transform.Translate(new Vector3(0.0f, -2.0f, 0.0f));
	}

	/*
		void Update()
		{
		}
	*/

	//****************************************************************************************************
	//*** Functions

}
