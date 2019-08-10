#region Описание скипта
/*
  Скрипт назначается объекту с колайдером типа isTriger для телепортации при соприкосновении с колайденом объекта имеющего тег Player или Teleported
  Для работы скрипта надо задать Target цель куда будет телепортирован объект 
  Если Target не указан, то должно быть указано направление и дистанция куда будет телепортирован объект Player или Object объект в который телепортируемся.
  Для регистрации коллизии столкновения объект c тегом Player должен иметь компонент RigidBody.
*/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
	/// <summary>
	/// Если указано, то телепортируемся к координатам объекта
	/// </summary>
	public GameObject Object;

	/// <summary>
	/// Если указаны координаты и не указан объект то телепортируемся в координаты
	/// </summary>
	public Vector3 Target;

	/// <summary>
	/// Если указано направление, то телепортируемся в данном направлении
	/// </summary>
	public Vector3 Direction;

	/// <summary>
	/// Дистанция на которую телепортируемся при указанном направлении
	/// </summary>
	public float   Dist;

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
				other.transform.position = Object.transform.position;
				if(FixBody && other.GetComponent<Rigidbody>())
				{
					other.GetComponent<Rigidbody>().isKinematic = true;
					other.GetComponent<Rigidbody>().isKinematic = false;
				}
					
			}
			else if(Target != Vector3.zero)
			{
				// Были переданы координаты телепортируемся в координаты
				other.transform.position = Target;
				if(FixBody && other.GetComponent<Rigidbody>())
				{
					other.GetComponent<Rigidbody>().isKinematic = true;
					other.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
			else
			{
				// Задано направление и дистанция туда и телепортируемся
				other.transform.position = other.transform.position + Direction * Dist; 
				if(FixBody && other.GetComponent<Rigidbody>())
				{
					other.GetComponent<Rigidbody>().isKinematic = true;
					other.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
		}
	}
}
