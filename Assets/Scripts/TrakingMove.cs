/*
	Скрипт вешается на объект который должен следовать за Target.
	Скрипт можно повесить на камеру, тогда она будет следовать за наблюдаемым объектом.
	Target					- цель к которой стремится объект
	Dist					- максимальная дистанция сближения, если не задана то запоминает дистанцию при старте
	SpeedMove				- позволяет регулировать постоянную скорость следования
	Acselerate				- позволяет регулировать скорость ускорения умножаемую на растояние до объекта, если значение 0 то ускорения от растояния не будет
	isFixX, isFixX, isFixX	- позволяют зафиксировать координату следящего объекта, например чтобы дрон не опускался к цели наблюдения

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrakingMove : MonoBehaviour
{
	/// <summary>
	/// Цель для сближения
	/// </summary>
	public Transform Target;

	/// <summary>
	/// максимальная дистанция сближения
	/// </summary>
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
	/// Фиксирование при сближении по координате
	/// </summary>
	public bool		isFixX			= false;

	/// <summary>
	/// Фиксирование при сближении по координате
	/// </summary>
	public bool		isFixY			= false;

	/// <summary>
	/// Фиксирование при сближении по координате
	/// </summary>
	public bool		isFixZ			= false;

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

	/// <summary>
	/// Изначальная разница высот
	/// </summary>
	private float height;

	void Awake()
    {
		// Запомним максимальную дистанцию сближения если не указали
		if(Dist == 0)
			Dist = Vector3.Distance(this.transform.position, Target.position); 

		height = Mathf.Abs(this.transform.position.y - Target.position.y);
    }

    void Update()
    {
		// Расчитаем параметры перемещения по настройкам
		dist		= Vector3.Distance(this.transform.position, Target.position);
		acselerate	= dist / Dist * Acselerate; 
		speedMove	= acselerate + SpeedMove;

		// Получаем следующую позицию следования с учетом фиксации координат
		Vector3 pos = GetPos(this.transform, Target, isFixX, isFixY, isFixZ);

		// Если объект далеко от цели заставляем объект двигаться к цели
        if(Dist < dist)
			this.transform.position = Vector3.MoveTowards(this.transform.position, pos, speedMove * Time.deltaTime);

		// Корректируем высоту наблюдателя если он ее меняет... (прыжки, телепорты)
		if(!isFixY && this.transform.position.y != Target.position.y+height)
			this.transform.position = new Vector3(this.transform.position.x, Target.position.y+height, this.transform.position.z);

		// Поворачиваем объект к цели
		this.transform.LookAt(Target);
    }

	/// <summary>
	/// Возвращает позицию с учетом фиксации координат
	/// </summary>
	/// <param name="thisObj">Стремящийся сблизиться объект</param>
	/// <param name="targetObj">Цель сближения</param>
	/// <param name="fix_x">Фиксация по координате</param>
	/// <param name="fix_y">Фиксация по координате</param>
	/// <param name="fix_z">Фиксация по координате</param>
	/// <returns>Позиция цели с учетом зафиксированных координат</returns>
	private Vector3 GetPos(Transform thisObj, Transform targetObj, bool fix_x, bool fix_y, bool fix_z)
	{
		float x,y,z;
		x = fix_x ? thisObj.position.x : targetObj.position.x;
		y = fix_y ? thisObj.position.y : targetObj.position.y;
		z = fix_z ? thisObj.position.z : targetObj.position.z;

		return new Vector3(x, y, z);
	}
}
