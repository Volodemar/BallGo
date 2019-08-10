using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
	/// <summary>
	/// Если указано, то телепортируемся к координатам объекта
	/// </summary>
	public GameObject Object;

	/// <summary>
	/// Куда телепортируется объект
	/// </summary>
	public GameObject Target;

	/// <summary>
	/// Зафиксировать тело объекта убрав силы
	/// </summary>
	public bool FixBody = false;

	private void OnTriggerEnter(Collider other)
	{
		// Проверяем теги объектов для распределения что в триггере телепортируется что нет
		if(other.CompareTag("Player") || other.CompareTag("Teleported"))
		{
			if(Object != null)
			{
				// Был передан объект телепортируемся в него
				Object.transform.position = Target.transform.position;
				if(FixBody && Object.GetComponent<Rigidbody>())
				{
					Object.GetComponent<Rigidbody>().isKinematic = true;
					Object.GetComponent<Rigidbody>().isKinematic = false;
				}
					
			}
		}
	}
}
