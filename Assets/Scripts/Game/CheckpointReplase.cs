using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReplase : MonoBehaviour
{
	/// <summary>
	/// Игровые объекты которые будут переразмещаться.
	/// </summary>
	public List<GameObject> ReplaceGameObjects;

	private List<Vector3> PositionGameObjects = new List<Vector3>();
	private List<bool>	  ActiveGameObjects  = new List<bool>();

	private void Start()
	{
		foreach (GameObject go in ReplaceGameObjects)
		{
			PositionGameObjects.Add(go.transform.position);
			ActiveGameObjects.Add(go.activeSelf);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			for(int i=0; i<ReplaceGameObjects.Count; i++)
			{
				if(ReplaceGameObjects[i].GetComponent<Rigidbody>())
				{
					ReplaceGameObjects[i].GetComponent<Rigidbody>().isKinematic = true;
					ReplaceGameObjects[i].GetComponent<Rigidbody>().isKinematic = false;
				}
				ReplaceGameObjects[i].transform.position = PositionGameObjects[i];
				ReplaceGameObjects[i].SetActive(ActiveGameObjects[i]);
			}
		}	
	}
}
