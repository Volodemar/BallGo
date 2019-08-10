using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMass : MonoBehaviour
{
	public GameObject gameObj;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag(gameObj.tag))
		{
			StartCoroutine(TimerAddMass(1f));
		}
	}

	IEnumerator TimerAddMass(float time)
	{
		yield return new WaitForSeconds(time);
		gameObj.GetComponent<Rigidbody>().mass = 15;
		this.gameObject.SetActive(false);
	}
}
