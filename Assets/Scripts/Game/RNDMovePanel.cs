using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNDMovePanel : MonoBehaviour
{
	public GameObject DeathPlatform;

	/// <summary>
	/// Список панелей 
	/// </summary>
	public List<GameObject> listPanels;

	private System.Random rnd = new System.Random();

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			for(int i=0; i<listPanels.Count; i++)
			{
				int x = rnd.Next(1, 15);
				int z = rnd.Next(1, 7);
				float fx = rnd.Next(0,2) == 1 ? DeathPlatform.transform.position.x + x : DeathPlatform.transform.position.x - x;
				float fz = rnd.Next(0,2) == 1 ? DeathPlatform.transform.position.z + z : DeathPlatform.transform.position.z - z;

				Vector3 newPos = new Vector3(fx, 0.26f, fz);
				listPanels[i].transform.position = newPos; //Vector3.MoveTowards(listPanels[i].transform.position, newPos, 1);
			}
		}
	}
}
