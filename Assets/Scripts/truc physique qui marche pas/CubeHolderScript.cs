﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CubeHolderScript : MonoBehaviour {

	public float simTime = 13f;
	float			timeSinceStart = 0f;
	Component[]   transformTab;
	string		theBigString;
	bool		boucheriedone = false;

	// Use this for initialization
	void Start () {
Physics.autoSimulation = false;
	}

	void FixedUpdate()
	{
		timeSinceStart += Time.fixedDeltaTime;
		if (timeSinceStart > simTime  && boucheriedone == false)
		{
			Boucherie();
		}
		else if (boucheriedone == false)
			Physics.Simulate(Time.fixedDeltaTime);
	}

	void Boucherie()
	{
		Debug.Log("preboucherie");
		transformTab = this.GetComponentsInChildren<Transform>();

//Debug.Log(transformTab.Cast< Transform >().Select(t => t.position.ToString() + t.rotation.ToString()).Aggregate((n1, n2) => n1 + n2).GetHashCode());
		foreach (Transform t in transformTab)
		{
			theBigString = string.Concat(theBigString, /*string.Concat(*/t.position.ToString("F5")/*, t.rotation.ToString("F5"))*/);
		}
		Debug.Log(theBigString);
		
		Debug.Log(theBigString.GetHashCode());
		
		Debug.Log("postboucherie");
		boucheriedone = true;
	}
}