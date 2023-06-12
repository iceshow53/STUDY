using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines
{
	public Vector3 StartPoint;
	public Vector3 EndPoint;
}  

public class Line : MonoBehaviour
{
	public List<Lines> LineList = new List<Lines>();

	[SerializeField] private float Width;
	[SerializeField] private float Height;

	private void Start()
	{
		Vector3 OldPoint = new Vector3(0.0f, 0.0f, 0.0f);

		for(int i = 0; i < 20; ++i)
		{
			Lines lines = new Lines();

			lines.StartPoint = OldPoint;

			OldPoint = new Vector3(
				OldPoint.x + Random.Range(1.0f, 5.0f),
				OldPoint.y + Random.Range(-5.0f, 5.0f),
				0.0f);

			lines.EndPoint = OldPoint;

			LineList.Add(lines);
		}

		Height = 0.0f;
		Width = 0.0f;
	}

	void Update()
	{
		float hor = Input.GetAxis("Horizontal") * Time.deltaTime * 2.0f;

		foreach (Lines element in LineList)
		{
			Debug.DrawLine(element.StartPoint, element.EndPoint, Color.green);

			if(element.StartPoint.x <= transform.position.x && transform.position.x < element.EndPoint.x)
			{
				Height = element.EndPoint.y - element.StartPoint.y;
				Width = element.EndPoint.x - element.StartPoint.x;

				float relativeX = transform.position.x - element.StartPoint.x;

				transform.position = new Vector3(
					transform.position.x + hor,
					(Height / Width) * relativeX + element.StartPoint.y,
					0.0f);
			}
		}

		if(Height == 0 || Width == 0)
		{

		}
		//else
		//	transform.position = new Vector3(
		//		transform.position.x,
		//		(Height / Width) * transform.position.x,
		//		0.0f);
	}
}
