using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Viuw_Vuforia {

	private Camera arCamera;
	public static int numPos = 0;

	public Viuw_Vuforia()
	{
		try {
			GameObject go = GameObject.Find("ARCamera");
			arCamera = go.GetComponent<Camera>();
		}
		catch (Exception e)
		{
			Debug.Log(e);
		}


	}

	public object GetPosition()
	{
		if (arCamera == null)
		{
			return null;
		}
		Vector3 pos = arCamera.transform.position;

		numPos++;
		object posObj = (object)pos;
		return posObj;

	}

	public object GetRotation()
	{
		if (arCamera == null)
		{
			return null;
		}

		Quaternion rot = arCamera.transform.rotation;
		object rotObj = (object)rot;
		return rotObj;

	}


}
