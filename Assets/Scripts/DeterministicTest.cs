using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeterministicTest
{
	public static Vector3 RoundVec3(this Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
	//	v.x = 1;
		return v;
	}

	public static float RoundFloat(this float f)
	{
		f = Mathf.Round(f);
		return f;
	}

	public static Quaternion RoundQuat(this Quaternion q)
	{
		q = Quaternion.Euler(q.eulerAngles.RoundVec3());
		return q;
	}

	public static void RoundTransform(this Transform t)
	{
		t.position = t.position.RoundVec3();
		t.rotation = t.rotation.RoundQuat();
	}
}
