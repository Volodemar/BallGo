#region Описание скипта
/*
  Скрипт отвечает за физическое перемещение персонажа с использованием RigidBody и AddForce
  На персонаже должен быть компонент CharacterController в разделе тело
*/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	/// <summary>
	/// Камера следящая за персонажем, если не указано то управление будет в глобальных координатах иначе зависит от направления камеры
	/// </summary>
	public Camera		Cam;

	/// <summary>
	/// Тело персонажа
	/// </summary>
	public Rigidbody	Body;

	/// <summary>
	/// Максимальная скорость движения в секунду
	/// </summary>
	public float		MaxSpeed	= 8.0f;

	public float		StopForce   = 10f;

	/// <summary>
	/// Максимальная сила воздействия на персонажа в секунду
	/// </summary>
	public float		Force		= 200.0f;

	/// <summary>
	/// Сила прыжка противодействующая гравитации
	/// </summary>
	public float		JumpForce	= 250.0f;

	/// <summary>
	/// Для хранения расчетной силы воздействия
	/// </summary>
	float force;

	private void Awake()
	{
		// Инициализация тела если не указано иное
		if(Body == null)
			Body = this.gameObject.GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		// Прикладываемая сила
		force = Force * Time.fixedDeltaTime;

		// Ограничиваем скорость противодействием если скорость стала слишком высокая
		if (Body.velocity.magnitude > MaxSpeed)
		{
			Body.AddForce(new Vector3(-Body.velocity.x, 0, -Body.velocity.z) * StopForce);			
		}
	}

	void Update()
    {
		if(Cam == null)
		{
			// Движение односительно глобальных координат
			if(Input.GetKey(KeyCode.W))
				Body.AddForce(Vector3.forward * force);
			if(Input.GetKey(KeyCode.S))
				Body.AddForce(Vector3.back * force);
			if(Input.GetKey(KeyCode.A))
				Body.AddForce(Vector3.left * force);
			if(Input.GetKey(KeyCode.D))
				Body.AddForce(Vector3.right * force);

			if(Input.GetKeyDown(KeyCode.Space) && OnGround())
				Body.AddForce(Vector3.up * JumpForce);		
		}
		else
		{
			// Движение односительно направления наблюдателя
			if(Input.GetKey(KeyCode.W))
			{
				Vector3 dirCam = new Vector3(Cam.transform.forward.x, Vector3.forward.y, Cam.transform.forward.z);
				Body.AddForce(dirCam * force);
			}
			if(Input.GetKey(KeyCode.S))
			{
				Vector3 dirCam = new Vector3(-Cam.transform.forward.x, Vector3.forward.y, -Cam.transform.forward.z);
				Body.AddForce(dirCam * force);
			}
			if(Input.GetKey(KeyCode.A))
			{
				Vector3 dirCam = new Vector3(-Cam.transform.right.x, Vector3.right.y, -Cam.transform.right.z);
				Body.AddForce(dirCam * force);
			}
			if(Input.GetKey(KeyCode.D))
			{
				Vector3 dirCam = new Vector3(Cam.transform.right.x, Vector3.right.y, Cam.transform.right.z);
				Body.AddForce(dirCam * force);
			}

			if(Input.GetKeyDown(KeyCode.Space) && OnGround())
				Body.AddForce(Vector3.up * JumpForce);	
		}
    }

	/// <summary>
	/// Возвращает true если игрок близко к земле
	/// </summary>
	/// <returns></returns>
	private bool OnGround()
	{
		RaycastHit hit;

		if(Physics.Raycast(this.transform.position, Vector3.down, out hit, 1f))
		{
			return true;
		}

		return false;
	}
}
