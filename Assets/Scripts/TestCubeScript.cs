using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCubeScript : MonoBehaviour {

	public GameObject theCube;
	Rigidbody			rb;
	Vector3				vecToTheCube;
	Material			mat;
	// Use this for initialization
	Color originalColor;
	void Start () {
		rb = GetComponent<Rigidbody>();
		mat = GetComponent<Material>();
		originalColor = mat.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		vecToTheCube =  theCube.transform.position - transform.position;
		// Debug.Log(vecToTheCube.magnitude);
		float dist = vecToTheCube.magnitude;
		if (dist < 10f)
		{
			mat.SetColor("_Color", Color.Lerp(originalColor, Color.red, dist / 10));
			rb.AddForce(vecToTheCube.x , vecToTheCube.y, vecToTheCube.z);
		}
	}
}
