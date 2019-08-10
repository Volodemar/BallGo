using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTracking : MonoBehaviour
{
	public GameObject Target;
	public float TrakingDist;

	/// <summary>
	/// максимальная дистанция сближения
	/// </summary>
	[Tooltip("Максимальная дистанция сближения.")]
	public float	Dist			= 0f;

	/// <summary>
	/// Постоянная скорость сближения
	/// </summary>
	public float	SpeedMove		= 1.0f;

	/// <summary>
	/// Скорость ускорения от растояния при 0 влияния растояния на скорость не будет
	/// </summary>
	public float	Acselerate		= 1.0f;

	/// <summary>
	/// Используется для получения текущей дистанции при расчетах
	/// </summary>
	private float dist;

	/// <summary>
	/// Хранит рассчитанную итоговую скорость
	/// </summary>
	private float speedMove;

	/// <summary>
	/// Хранит рассчитанное от растояния ускорения
	/// </summary>
	private float acselerate;

    private Vector3 defPos;
	private Quaternion defRot;

    void Start()
    {
        defPos = this.transform.position;
		defRot = this.transform.rotation;
    }

    void Update()
    {
		// Если цель близко начинаем преследование
        if(ActivDist(TrakingDist))
		{
			// Смотрим на цель
			this.transform.LookAt(Target.transform.position);

			// Если цель видна не спряталась
			if(OnVisible())
			{
				// Расчитаем параметры перемещения по настройкам
				dist		= Vector3.Distance(this.transform.position, Target.transform.position);
				acselerate	= dist / Dist * Acselerate; 
				speedMove	= acselerate + SpeedMove;

				// Получаем следующую позицию следования с учетом фиксации координат
				Vector3 pos = new Vector3(Target.transform.position.x, this.transform.position.y, Target.transform.position.z);

				// Если объект далеко от цели заставляем объект двигаться к цели
				if(Dist < dist)
					this.transform.position = Vector3.MoveTowards(this.transform.position, pos, speedMove * Time.deltaTime);
			}
			else
			{
				// Смотрим на точку возврата
				this.transform.LookAt(defPos);

				if(this.transform.position != defPos)
					this.transform.position = Vector3.MoveTowards(this.transform.position, defPos, speedMove * Time.deltaTime);
			}
		}
		else
		{
				if(this.transform.position != defPos)
				{
					// Смотрим на точку возврата
					this.transform.LookAt(defPos);
					this.transform.position = Vector3.MoveTowards(this.transform.position, defPos, speedMove * Time.deltaTime);
				}
				else
				{
					// Смотрим на точку возврата
					this.transform.rotation = defRot;
				}
		}
    }

	bool ActivDist(float dist)
	{
		if(Vector3.Distance(Target.transform.position, this.transform.position) < dist)
			return true;
		else
			return false;
	}

	private bool OnVisible()
	{
		RaycastHit hit;

		if(Physics.Raycast(this.transform.position, this.transform.forward, out hit, TrakingDist))
		{
			return true;
		}

		return false;
	}
}
