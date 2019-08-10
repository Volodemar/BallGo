using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerExit : MonoBehaviour
{
	public GameController GameController;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			GameController.OnGameWin();
		}
	}
}
