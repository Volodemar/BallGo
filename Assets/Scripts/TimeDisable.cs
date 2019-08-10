using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisable : MonoBehaviour
{
	/// <summary>
	/// Теги на которые реагирует триггер
	/// </summary>
	public List<string> TegsList;

	/// <summary>
	/// Время через которое исчезнет объект
	/// </summary>
	public float Timer = 0.1f;

	/// <summary>
	/// Переключает время до исчезновения идет после прикосновения или до прикосновения
	/// </summary>
	public bool isPostAction = true;


	private void OnTriggerEnter(Collider other)
	{
		if(!isPostAction)
		{
			foreach(string teg in TegsList)
			{
				if(other.CompareTag(teg))
				{
					if(Timer == 0)
						this.gameObject.gameObject.SetActive(false);
					else
						StartCoroutine(OnOff(this.gameObject, false, Timer));
				}
			}		
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(isPostAction)
		{
			foreach(string teg in TegsList)
			{
				if(other.CompareTag(teg))
				{
					if(Timer == 0)
						this.gameObject.gameObject.SetActive(false);
					else
						StartCoroutine(OnOff(this.gameObject, false, Timer));
				}
			}
		}
	}

	IEnumerator OnOff(GameObject go, bool isActive,  float time)
	{
		yield return new WaitForSeconds(time);
		go.gameObject.SetActive(isActive);
	}
}
