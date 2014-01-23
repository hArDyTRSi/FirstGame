using UnityEngine;
using System.Collections;

public class MoneyTracker : MonoBehaviour {

	public int actualMoney = 0;

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

	void OnGUI()
	{
		GUI.Box(new Rect(10f, 10f,100f,25f), "Money: " + actualMoney);

	}

	public void AddMoney(int amount)
	{
		actualMoney += amount;
	}

}
