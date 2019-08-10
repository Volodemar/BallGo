/*
	Запускает объект в направлении столкновения с игроком
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTriger : MonoBehaviour
{
	// Теги на которые реагирует триггер
	public List<string> TegsList;

	private void OnTriggerEnter(Collider other)
	{
		foreach(string teg in TegsList)
		{
			if(other.CompareTag(teg))
			{
				Vector3 direction = other.transform.position - this.transform.position;
				this.GetComponent<Rigidbody>().AddForce(-direction * 1000 * this.GetComponent<Rigidbody>().mass);
			}
		}		
	}
}
