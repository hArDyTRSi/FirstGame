using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour
{
//-------------------------------------------------------------------------------------------------
//--- Public Fields

public static Global global;
	

public float levelSize = 500.0f;
	
public float collisionDamage = 10.0f;
public float invulnerabilityTimeSpan = 1.0f;

public bool gameOver = false;

public List<GameObject> enemiesAlive = null;

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++ Private Fields
	
	
//#################################################################################################
//### Constructor

void Awake()
{
	global = this;
		
	gameOver = true;

	enemiesAlive.Clear();	//.RemoveAll(GameObject);
}
	
//****************************************************************************************************
//*** Functions

}
