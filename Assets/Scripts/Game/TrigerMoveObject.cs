using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerMoveObject : MonoBehaviour
{
	// Теги на которые реагирует триггер
	public List<string> TegsList;

	/// <summary>
	/// Двигающийся объект
	/// </summary>
	public GameObject Target;
	
	/// <summary>
	/// Смещения координаты 
	/// </summary>
	public float MoveX = 0;
	public float MoveY = 0;
	public float MoveZ = 0;

	public float SpeedMove = 3f;

	private Vector3 posTarget;

	/// <summary>
	/// Если попали в тригер то сдвигаем объект цель.
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerStay(Collider other)
	{
		foreach(string teg in TegsList)
		{
			if(other.CompareTag(teg))
			{
				posTarget = Target.transform.position;
				posTarget.x = posTarget.x + MoveX;
				posTarget.y = posTarget.y + MoveY;
				posTarget.z = posTarget.z + MoveZ;

				Target.transform.position = posTarget;
			}
		}
	}
}
