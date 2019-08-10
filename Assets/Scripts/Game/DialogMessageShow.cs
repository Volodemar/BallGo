using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogMessageShow : MonoBehaviour
{
	//Контроллер игры
	public GameController GameController;

	//Текст при столкновении
	public string DialogText;

	//Время на отображение одного символа
	public float ShowTimeToChar = 0.1f;

	//Показать 1 раз
	public bool isShowOne = true;

	private int numShow = 0;

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if(isShowOne && numShow > 0)
			{
				return;
			}

			GameController.OnDialogTextShow(DialogText, ShowTimeToChar);
			numShow++;
		}
	}
}
