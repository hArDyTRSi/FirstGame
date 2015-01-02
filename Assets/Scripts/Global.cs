using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------
	//--- Public Fields

	public static Global global;

	public Transform instanceFolder = null;

	public float levelSize = 500.0f;

	public float collisionDamage = 10.0f;
	public float invulnerabilityTimeSpan = 1.0f;

	public bool gameOver = false;

	[Range(0.0f, 1.0f)]
	public float audioVolume = 1.0f;

	public List<GameObject> targetableEnemies = null;


	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	//+++ Private Fields



	//#################################################################################################
	//### UnityEngine

	void Awake()
	{
		global = this;

		instanceFolder = GameObject.FindGameObjectWithTag("instanceFolder").transform;

		gameOver = true;

		targetableEnemies.Clear();
	}


	void Update()
	{
		// debug-hack for audio volume
		AudioListener.volume = audioVolume;

		/*
		foreach(GameObject t in Global.global.enemiesAlive)
		{
			if(t == null)
			{
				enemiesAlive.Remove(t);
			}
		}
		*/
	}

	/*
	void OnGUI()
	{
		if(AudioListener.volume != 0)
		{
			if(GUI.Button(new Rect(10, 10, 60, 30), "Off"))
			{
				AudioListener.volume = 0;
			}
		}
		else
		if(AudioListener.volume == 0)
		{
			if(GUI.Button(new Rect(10, 10, 60, 30), "On"))
			{
				AudioListener.volume = 100;
			}
		}

	}
	*/


	//****************************************************************************************************
	//*** Functions
	/*
	public void RemoveEnemyFromList(GameObject enemy)
	{
		enemiesAlive.Remove(enemy);

	}
	*/
}
