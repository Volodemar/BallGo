/*
	Выключает объект, но включает другой объект.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerOffOnBorder : MonoBehaviour
{
	// Теги на которые реагирует триггер
	public List<string> TegsList;

	// Отключаемый объект
	public List<GameObject> TargetOffList;

	// Включаемый объект
	public List<GameObject> TargetOnList;

	// Время до события
	public float Timer = 0;

	private void OnTriggerEnter(Collider other)
	{
		foreach(string teg in TegsList)
		{
			if(other.CompareTag(teg))
			{
				for(int i=0; i<TargetOffList.Count; i++)
				{
					GameObject go = TargetOffList[i];

					if(Timer == 0)
					{
						go.gameObject.SetActive(false);
					}
					else
					{
						StartCoroutine(OnOff(go, false, Timer));
					}
				}

				for(int i=0; i<TargetOnList.Count; i++)
				{
					GameObject go = TargetOnList[i];

					if(Timer == 0)
					{
						go.gameObject.SetActive(true);
					}
					else
					{
						StartCoroutine(OnOff(go, true, Timer));
					}
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
