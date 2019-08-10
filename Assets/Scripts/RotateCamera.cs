/*
	Скрипт вешается на камеру чтобы вращать вокруг target, дистанция запоминается при старте, если объект не видно из-за препятствий то камера резко перемещается за препятствие.
	SpeedX, SpeedY	- чувствительность мышки при обзоре.
	obstacles		- сюда перечисляются слои объекты которых будут расцениваться как препятствие видимости
	FixedY			- запрещает обзор по вертикали и включает режим возврата на высоту наблюдения после коллизий видимости
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
	/// <summary>
	/// Цель наблюдения вокруг которой вращается камера
	/// </summary>
	public Transform Target;

	/// <summary>
	/// Скорость камеры по оси Х
	/// </summary>
	public float SpeedX = 360f;
	
	/// <summary>
	/// Скорость камеры по оси Y
	/// </summary>
	public float SpeedY = 240f;

	/// <summary>
	/// Слои препятствие
	/// </summary>
	public LayerMask obstacles;

	/// <summary>
	/// Фиксирует камеру по координате Y
	/// </summary>
	public bool FixedY = false;

	/// <summary>
	/// Дистанция до цели или длина луча проверки препятствий видимости
	/// </summary>
	private float Dist;

	/// <summary>
	/// Хранит координату Y, чтобы к ней вернуться
	/// </summary>
	private float fixedY;

	private void Start()
	{
		// Запоминаем начальную дистанцию и координату по Y
		Dist	= Vector3.Distance(this.transform.position, Target.position);
		fixedY	= this.transform.position.y;
	}

	void Update()
    {
		// Вращаем камеру
		CameraRotation();

		// Обрабатываем колизии пересечения видимости с объектами
		ObstaclesReact();
    }

	/// <summary>
	/// Поворот камеры
	/// </summary>
	private void CameraRotation()
	{
		// Получим перемещение мыши по осям X и Y
		float h = Input.GetAxis("Mouse X");
		float v = !FixedY ? Input.GetAxis("Mouse Y") : 0;
		Vector3 targetPos = new Vector3(Target.position.x, Target.position.y, Target.position.z);

		// Если мышь была перемещена по вертикали
		if(v != 0)
		{
			Vector3 last = this.transform.position;
			
			// Поворот по оси Y вокруг цели
			this.transform.RotateAround(targetPos, Vector3.right, v * SpeedX * Time.deltaTime);

			// Запрет опускать камеру под пол
			if(this.transform.position.y < Target.position.y)
				this.transform.position = last;

		}

		// Если мышь была перемещена по горизонтали
		if(h != 0)
		{
			this.transform.RotateAround(targetPos, Vector3.up, h * SpeedX * Time.deltaTime);
		}

		// Поворачиваем камеру на цель
		transform.LookAt(Target);
	}

	/// <summary>
	/// Реагирование на препятствия
	/// </summary>
	private void ObstaclesReact()
	{
		// Текущая дистранция до цели
		float distance	 = Vector3.Distance(this.transform.position, Target.position);
		//float y			 = defaultPos.y;

		RaycastHit hit;
		if(Physics.Raycast(Target.position, this.transform.position - Target.position, out hit, Dist, obstacles))
		{
			// Двигаем камеру в точку за препятствием между камерой и целью
			this.transform.position = hit.point;
		}
		else if(distance < Dist && !Physics.Raycast(this.transform.position, -this.transform.forward, 0.1f, obstacles))
		{
			// Двигаем камеру к нужной позиции по Y
			if (FixedY && fixedY != this.transform.position.y)
			{
				Vector3 returnPos = new Vector3(this.transform.position.x, fixedY, this.transform.position.z);
				this.transform.position = Vector3.MoveTowards(this.transform.position, returnPos, Mathf.Abs(this.transform.position.y - fixedY) * Time.deltaTime);
			}
		}
	}
}
