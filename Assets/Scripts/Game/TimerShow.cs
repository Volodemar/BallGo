using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerShow : MonoBehaviour
{
	/// <summary>
	/// Время отображения
	/// </summary>
	public float Timer = 1;

	private float timer;
	private bool isShow;

	private void Start()
	{
		timer = Timer;
	}

	void Update()
    {
		timer = timer - Time.fixedDeltaTime;

        if(timer < 0)
		{
			this.gameObject.GetComponent<Renderer>().enabled = isShow;
			isShow = !isShow;
			timer = Timer;
		}
    }
}
