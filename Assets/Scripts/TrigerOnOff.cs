using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerOnOff : MonoBehaviour
{
	// Тип триггера отключение или включение объектов
	public bool isTurnOn = false;

	// Теги на которые реагирует триггер
	public List<string> TegsList;

	public List<GameObject> TargetList;

	public bool isOnlyChecked = false;

	public float Timer = 0;

	private void OnTriggerEnter(Collider other)
	{
		foreach(string teg in TegsList)
		{
			if(other.CompareTag(teg))
			{
				for(int i=0; i<TargetList.Count; i++)
				{
					GameObject go = TargetList[i];

					if(Timer == 0)
					{
						go.gameObject.SetActive(isTurnOn);
					}
					else
					{
						StartCoroutine(OnOff(go, isTurnOn, Timer));
					}
				}

				if(isOnlyChecked)
				{
					if(Timer == 0)
						this.gameObject.SetActive(false);
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
