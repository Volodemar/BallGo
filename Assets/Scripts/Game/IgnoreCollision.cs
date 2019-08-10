/*
	Игнорирование коллизий с объектами зарание переданными в набор
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
	public List<Collider> ignoreColliders; 

    private void Start()
    {
		Collider thisCollider = this.gameObject.GetComponent<Collider>();

        if(ignoreColliders != null)
		{
			foreach(Collider collider in ignoreColliders)
			{
				Physics.IgnoreCollision(collider, thisCollider);
			}
		}
    }
}
