using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerSavePlayer : MonoBehaviour
{
	// Теги на которые реагирует триггер
	public List<string> TegsList;

	// Таймер активации чекпоинта
	public float Timer = 5;

	// Разрешено сохранить игру
	private bool isAllovedToSave = false;

	private void Start()
	{
		// При старте игры запрещаем пересохранение и скрываем со сцены
		this.gameObject.GetComponent<Renderer>().enabled = false;
		StartCoroutine(TimerActiveCheckpoint(Timer));
	}

	// Сохраняем данные позиции игрока
	private void OnTriggerEnter(Collider other)
	{
		foreach(string teg in TegsList)
		{
			if(other.CompareTag(teg))
			{
				if(isAllovedToSave)
				{
					SavePlayerPos(this.transform.position);
					this.gameObject.GetComponent<Renderer>().enabled = false;
					StartCoroutine(TimerActiveCheckpoint(Timer));
				}
			}
		}
	}

	private void SavePlayerPos(Vector3 pos)
	{
		PlayerPrefs.SetFloat("PosX", pos.x);
		PlayerPrefs.SetFloat("PosY", pos.y);
		PlayerPrefs.SetFloat("PosZ", pos.z);
	}

	private IEnumerator TimerActiveCheckpoint(float time)
	{
		yield return new WaitForSeconds(time);
		this.gameObject.GetComponent<Renderer>().enabled = true;
		isAllovedToSave = true;
	}
}


