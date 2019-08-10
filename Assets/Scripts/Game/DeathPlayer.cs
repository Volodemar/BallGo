using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
	public GameController GameController;

	/// <summary>
	/// Персонаж для взятия начальной позиции
	/// </summary>
	public GameObject Player;

	/// <summary>
	/// Куда телепортировать если нету сохранения
	/// </summary>
	public GameObject Target;

	/// <summary>
	/// Зафиксировать тело объекта убрав силы
	/// </summary>
	public bool FixBody = true;

	private Vector3 savePos;

	// Сохраним первичную позицию объекта
	private void Start()
	{
		savePos = Player.transform.position;
	}

	private void OnTriggerEnter(Collider other)
	{
		// Проверяем теги объектов для распределения что в триггере телепортируется что нет
		if(other.CompareTag("Player"))
		{
			GameController.OnPlayerDeath();

			if(PlayerPrefs.HasKey("PosX"))
			{
				// Если сохранены координаты чекпоинта то перемещаемся к ним после смерти
				Vector3 LoadPosPlayer = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
				other.transform.position = LoadPosPlayer;
				if(FixBody && other.GetComponent<Rigidbody>())
				{
					other.GetComponent<Rigidbody>().isKinematic = true;
					other.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
			else if (Target != null)
			{
				// Был передан объект телепортируемся в него
				other.transform.position = Target.transform.position;
				if(FixBody && other.GetComponent<Rigidbody>())
				{
					other.GetComponent<Rigidbody>().isKinematic = true;
					other.GetComponent<Rigidbody>().isKinematic = false;
				}			
			}
			else
			{
				// Ничего еще не сохранено перемещаемся в позицию старта игрока
				other.transform.position = savePos;
				if(FixBody && other.GetComponent<Rigidbody>())
				{
					other.GetComponent<Rigidbody>().isKinematic = true;
					other.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
		}
	}
}
