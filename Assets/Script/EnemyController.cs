using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
	public Node Target;

	private Camera camera;

	private float Speed;

	Vector3 LeftCheck;
	Vector3 RightCheck;

	[Range(0.0f,180.0f)]
	public float Angle;

	private bool move;
	private bool is_rotation;

	private bool View;

	private Vector3 offset;
	private Vector3 rayoffset;

	private void Awake()
	{
		camera = Camera.main;

		SphereCollider coll = GetComponent<SphereCollider>();
		coll.radius = 0.05f;
		coll.isTrigger = true;

		Rigidbody rigid = GetComponent<Rigidbody>();
		rigid.useGravity = false;

		Target = GameObject.Find("ParentObject").transform.GetChild(0).GetComponent<Node>();

		offset = new Vector3(0.0f, 10.0f, 10.0f);
	}

	private void Start()
	{
		Speed = 5.0f;

		float x = 2.5f;
		float z = 3.5f;

		LeftCheck = transform.position + (new Vector3(-x, 0.0f, z));
		RightCheck = transform.position + (new Vector3(x, 0.0f, z));

		move = true;
		is_rotation = false;
		View = false;

		rayoffset = new Vector3(5.0f, 0.0f, 5.0f);
	}

	private void Update()
	{
		View = Input.GetKey(KeyCode.Tab) ? true : false;

		if(View)
		{
			offset = new Vector3(0.0f, 5.0f, -3.0f);
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 130.0f, Time.deltaTime);
		}
		else
		{
			offset = new Vector3(0.0f, 10.0f, -10.0f);
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 60.0f, Time.deltaTime);
		}

		camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + offset, 0.016f);

		camera.transform.LookAt(transform.position);

		if(Target)
		{
			if(move)
			{
				Vector3 Direction = (Target.transform.position - transform.position).normalized;
				transform.position += Direction * Speed * Time.deltaTime;
			}
			else
			{
				if (!is_rotation)
					RotationStart();
			}
		}
	}

	private void FixedUpdate()
	{
		// RaycastHit hit;

		Vector3 direction = transform.position + rayoffset;

		float direction1 = Vector3.Distance(transform.position, direction);

		Angle = Mathf.Cos((direction.x - transform.position.x) / direction1);

		Debug.DrawRay(transform.position, new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(Angle * Mathf.Deg2Rad)), Color.green);

		Debug.DrawRay(transform.position, new Vector3(Mathf.Sin(-Angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(-Angle * Mathf.Deg2Rad)), Color.green);

		for (float f = -Angle; f <= Angle; f += 5.0f)
		{
			Debug.DrawRay(transform.position, new Vector3(Mathf.Sin(f * Mathf.Deg2Rad), 0.0f, Mathf.Cos(f * Mathf.Deg2Rad)), Color.red);
		}
	}

	private void LateUpdate()
	{
		
	}

	private void RotationStart()
	{
		is_rotation = true;
		StartCoroutine(SetRotation());
	}

	IEnumerator SetRotation()
	{
		float time = 0.0f;

		while (time < 180.0f)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.back), Time.deltaTime * 2);

			time += Time.deltaTime * 2;

			yield return null;
		}

		move = true;
		is_rotation = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(Target.transform.name == other.transform.name)
		{
			Target = Target.Next;
			move = false;
		}
	}
}
