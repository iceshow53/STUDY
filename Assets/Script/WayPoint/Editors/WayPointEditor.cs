#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : EditorWindow
{
	[MenuItem("CustomEditor/WayPoint")]
	public static void ShowWindows()
	{
		GetWindow<WayPointEditor>("WayPoint");
	}

	private GameObject Parent;

	private void OnGUI()
	{
		Parent = (GameObject)EditorGUILayout.ObjectField("Parent", Parent, typeof(GameObject), true);

		// 라벨 필드
		// EditorGUILayout.LabelField("부모", Parent.name);

		GUILayout.BeginHorizontal();
		{
			GUILayout.FlexibleSpace();

			if(GUILayout.Button("Add Node",GUILayout.Width(350),GUILayout.Height(25),GUILayout.MinWidth(0),GUILayout.MinHeight(0)))
			{
				if(Parent != null)
				{
					GameObject NodeObject = new GameObject();
					NodeObject.transform.name = "Node_" + Parent.transform.childCount;


					Node node = NodeObject.AddComponent<Node>();

					// ** Node의 설정

					SphereCollider coll = node.GetComponent<SphereCollider>();
					coll.radius = 0.05f;

					if(0 < Parent.transform.childCount)
					{
						Parent.transform.GetChild(Parent.transform.childCount - 1).GetComponent<Node>().Next = node;
						node.Next = Parent.transform.GetChild(0).GetComponent<Node>();
					}

					NodeObject.transform.SetParent(Parent.transform);

					NodeObject.transform.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));





				}
				
			}
				
			GUILayout.FlexibleSpace();
		}
		GUILayout.EndHorizontal();
	}

}

#endif