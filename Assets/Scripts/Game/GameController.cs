using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject Player;
	public GameObject TNumLifes;
	public GameObject TDialogText;
	public GameObject Menu;
	public GameObject GameOver;
	public GameObject GameUI;
	public GameObject GameWin;

	// Индикатор паузы
	private bool paused = false;

	public delegate void Methods();

	// Количество жизней
	private int lifes = 5;

	// Время отображения диалогового текста.
	private float TimeShowTDialogText;

	// Родитель текста диалога (панель)
	private GameObject ParentTDialogText;

	private void Start()
	{
		// Отобразим количество жизней
		TNumLifes.GetComponent<Text>().text = lifes.ToString();

		// Кешируем родителя
		ParentTDialogText = TDialogText.transform.parent.gameObject;

		// Включаем гуи
		GameUI.SetActive(true);
	}

	private void FixedUpdate()
	{
		// Таймер истекает и может быть событием пополнен
		if(TimeShowTDialogText > 0)
			TimeShowTDialogText = TimeShowTDialogText - Time.fixedDeltaTime;

		// Таймер истек, скрываем диалог
		if(TimeShowTDialogText <= 0 && ParentTDialogText.activeSelf)
			ParentTDialogText.SetActive(false);
	}

	private void Update()
	{
		// Регистрирует вызов паузы в игре останавливая время
		OnTrigerPaused();
	}

	// Регистрирует вызов паузы в игре останавливая время
	private void OnTrigerPaused()
	{
		//ESC вызывает и убирает меню
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(paused)
			{
				OnContinueGame();
			}
			else
			{
				//Пауза в игре
				Time.timeScale = 0;
				Menu.SetActive(true);
				paused = true;
			}
		}
	}

	// Игрок умирает
	public void OnPlayerDeath()
	{
		// Уменьшим количество жизней
		if(lifes > 0)
		{
			// Уменьшаем количество жизней
			lifes--;

			// Отобразим количество жизней
			TNumLifes.GetComponent<Text>().text = lifes.ToString();
		}

		// Жизни кончились игра начинается заного
		if(lifes == 0)
		{
			OnGameOver();
		}
	}

	// Игра проиграна
	private void OnGameOver()
	{
		paused = true;
		Time.timeScale = 0;
		GameOver.SetActive(true);
	}

	public void OnGameWin()
	{
		paused = true;
		Time.timeScale = 0;
		GameWin.SetActive(true);
	}

	/// <summary>
	/// Показать диалоговый текст
	/// </summary>
	/// <param name="newText">Текст</param>
	/// <param name="timer">Время на чтение одного символа, по умолчанию 0.055f.</param>
	public void OnDialogTextShow(string newText, float timer = 0.1f)
	{
		TDialogText.GetComponent<Text>().text = newText;	
		ParentTDialogText.SetActive(true);
		TDialogText.SetActive(true);
		TimeShowTDialogText = newText.Length * timer;
		//StartCoroutine(TimerOffTDialogText(newText.Length * timer));
	}

	// Скрыть диалоговое сообщение (не подходит надо сбивать таймер...)
	//IEnumerator TimerOffTDialogText(float timer)
	//{	
	//	yield return new WaitForSeconds(timer);
	//	TDialogText.transform.parent.gameObject.SetActive(false);
	//}

	// Начать игру сначала
	public void OnStartGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Level1");
	}

	// Продолжить
	public void OnContinueGame()
	{
		//Снять паузу
		Menu.SetActive(false);
		Time.timeScale = 1;
		paused = false;
	}

	// Выход из игры
	public void OnGameExit()
	{
		Application.Quit();
	}
}
