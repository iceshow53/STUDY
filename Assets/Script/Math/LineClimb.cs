using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineClimb : MonoBehaviour
{
	private float Speed;
	private float Degree;
	private Line line;
	
	private void Awake()
	{
		gameObject.AddComponent<MyGizmo>();
		line = GameObject.Find("Line").GetComponent<Line>();
	}

	void Start()
	{
		Speed = 3.0f;
	}

	void Update()
	{
	}
}
