using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Physics : MonoBehaviour {

	public Vector3Int startingVec;
	
	Vector3Int		pos; // ca cest oukskile
	Vector3Int		rot; // ca cst comment cest touner

	Vector3Int		force; // somme des force sur le truc , ajouter la gravite si ya
	int				mass; // la masse que tu decide sur le machin
	Vector3Int		acceleration; // force / masse
	Vector3Int 		velocity; // acceleration * temps
	Vector3Int		moveAmount; // velocity * temps

	Vector3Int		reflectDir; // vector3.reflect(normal, vector dariver(normalized))
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
