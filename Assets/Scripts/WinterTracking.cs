/*
	Поднимает объект если нажата 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterTracking : MonoBehaviour
{
	// Теги на которые реагирует ветер
	public List<string> TegsList;

	/// <summary>
	/// Сила ветра
	/// </summary>
	public float WindForce = 600f;

	/// <summary>
	/// Направление ветра, по локальным координатам
	/// </summary>
	public Transform WindDirection;

	/// <summary>
	/// Тело попавшее в триггер
	/// </summary>
	private Rigidbody Body = null;

	/// <summary>
	/// Действует ли ветер
	/// </summary>
	private bool isUp = false;

	private void Update()
	{
		if(isUp && Body != null)
		{
			if(WindDirection != null)
				Body.AddForce(WindDirection.forward * WindForce * Time.deltaTime);
			else
				Body.AddForce(Vector3.up * WindForce * Time.deltaTime);
		}
	}

	// Если вошли в триггер и не нажата ни одна клавиша то поднимаем объект медленно вверх
	private void OnTriggerEnter(Collider other)
	{
		foreach(string teg in TegsList)
		{
			if(other.CompareTag(teg))
			{
				Body = other.gameObject.GetComponent<Rigidbody>();
				isUp = true;
			}
		}		
	}

	private void OnTriggerExit(Collider other)
	{
		Body = null;
		isUp = false;
	}
}
