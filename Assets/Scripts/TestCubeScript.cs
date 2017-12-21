﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCubeScript : MonoBehaviour {

	public GameObject theCube;
	Rigidbody			rb;
	Vector3				vecToTheCube;
	Material			mat;
	MaterialPropertyBlock props;
	MeshRenderer renderer;

	// Use this for initialization
	Color originalColor;
	void Start () {
		rb = GetComponent<Rigidbody>();
		props = new MaterialPropertyBlock();
		renderer = GetComponent<MeshRenderer>();
		originalColor = renderer.sharedMaterial.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		vecToTheCube =  theCube.transform.position - transform.position;
		// Debug.Log(vecToTheCube.magnitude);
		float dist = vecToTheCube.magnitude;
		if (dist < 500f)
		{
			// mat.SetColor("_Color", Color.Lerp(originalColor, Color.red, 10 / dist));
			rb.AddForce(vecToTheCube.x * 2, vecToTheCube.y * 2, vecToTheCube.z * 2);
		}
  		props.SetColor("_Color", Color.Lerp(originalColor, Color.red, 10 / dist));
		renderer.SetPropertyBlock(props);
	}

// MaterialPropertyBlock props = new MaterialPropertyBlock();
// MeshRenderer renderer;

}