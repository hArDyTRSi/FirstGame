using UnityEngine;
using System.Collections;

public class MoneyTracker : MonoBehaviour
{
//-------------------------------------------------------------------------------------------------
//--- Public Fields

public int actualMoney = 0;


//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++ Private Fields


//#################################################################################################
//### UnityEngine
/*
void Start()
{
}
*/
/*
void Update()
{ 
} 
*/

void OnGUI()
{
	GUI.Box(new Rect(10f, 10f, 100f, 25f), "Money: " + actualMoney);
}


//****************************************************************************************************
//*** Functions

public void AddMoney(int amount)
{
	actualMoney += amount;
}

}
