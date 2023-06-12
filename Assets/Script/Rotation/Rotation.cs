using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
	[Range(0.0f,180.0f)]
	public float Angle;

	[Range(0.0f,1.0f)]
	public float dis;

	private Vector3 temp;
	private Vector3 dest;
	private int check;
	private bool move;

	private void Start()
	{
		Angle = 0.0f;
		dis = 0.0f;

		temp = new Vector3(0.0f, 0.0f, 0.0f);
		dest = new Vector3(100.0f, 0.0f, 0.0f);
		check = 0;
		move = false;
		
	}
	
	void Update()
	{
		// transform.eulerAngles = new Vector3(0.0f, Angle, 0.0f);

		Quaternion rotation = transform.rotation;

		// transform.rotation = Quaternion.Lerp(transform.rotation,)

		transform.rotation = rotation;

		if(Input.GetMouseButtonDown(0) && !move)
		{
			move = true;
			StartCoroutine(SetMove());
		}


	}

	IEnumerator SetMove()
	{
		float time = 0.0f;

		check = (check == 0) ? 1 : 0;

		while(time < 1.0f)
		{
			if(check == 0)
				transform.position = Vector3.Lerp(dest, temp, time);			
			else
				transform.position = Vector3.Lerp(temp, dest, time);

			time += Time.deltaTime;

			yield return null;
		}
		
		move = false;
	}
}
