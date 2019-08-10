using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
	public GameController GameController = null;

	private void Awake()
	{
		//Удаляем сохранения при старте игры
		PlayerPrefs.DeleteAll();
	}

	private void Start()
	{
		//Записываем начальную позицию при старте игры
		Vector3 SavePosPlayer = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		SavePlayerPos(SavePosPlayer);

		if(GameController != null)
			GameController.OnDialogTextShow("Синий: Что где я?... Неужели это сон, это совсем не похоже на знакомый бильярдный стол.");
	}

	private void Update()
	{
		//Если координата меньше 0 то мы упали
		if(this.transform.position.y < 0)
		{
			this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

			if(PlayerPrefs.HasKey("PosX"))
			{
				Vector3 LoadPosPlayer = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
				this.transform.position = LoadPosPlayer;
			}

			this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	private void SavePlayerPos(Vector3 pos)
	{
		PlayerPrefs.SetFloat("PosX", pos.x);
		PlayerPrefs.SetFloat("PosY", pos.y);
		PlayerPrefs.SetFloat("PosZ", pos.z);
	}
}
