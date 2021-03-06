using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	[Tooltip("The Layers which get detected as Walls.")]
	[SerializeField] private LayerMask layerMask;

	public void Push(Vector3 direction)
	{
		Debug.DrawRay(transform.position, direction, Color.red, 1f);
		if (!Physics.Raycast(transform.position, direction, 1, layerMask))
		{
			this.gameObject.transform.position += direction;
		}
	}
}
