/*
	Скрипт вешется на триггер, когда в триггере находится другой объект триггер вместе с объектом поднимается на заданную высоту или движется к заданной точке
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTracking : MonoBehaviour
{
	public List<string> TegsList;
	public Vector3 TargetPoint;
	public Transform TargetObject;
	public float Speed = 1.0f;
	public Camera cam;

	private Vector3 defaultPos;
	private bool isUp = false;
	private Vector3 targetPoint;

	void Start()
    {
        defaultPos = this.transform.position;
		TargetPoint = TargetObject != null ? TargetObject.position : TargetPoint;
    }

    void Update()
    {
        if(!isUp && this.transform.position != defaultPos)
		{
			this.transform.position  = Vector3.MoveTowards(this.transform.position, defaultPos, Speed * Time.deltaTime); 
		}

		// Позиция может быть изменена
		TargetPoint = TargetObject != null ? TargetObject.position : TargetPoint;
    }

	private void OnTriggerStay(Collider other)
	{
		foreach(string teg in TegsList)
		{
			if(other.CompareTag(teg))
			{
				if(!Input.anyKey)
				{
					// Захватываем объект и поднимаемся с лифтом
					this.transform.position  = Vector3.MoveTowards(this.transform.position, TargetPoint, Speed * Time.deltaTime); 
					other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z); 
					other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
					other.gameObject.GetComponent<Rigidbody>().useGravity = false;
					isUp = true;
				}
				else
				{
					other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		other.gameObject.GetComponent<Rigidbody>().useGravity = true;
		isUp = false;
	}
}
