using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	[Tooltip("The Layers which get detected as Walls.")]
	[SerializeField] private LayerMask layerMask;

	public void Push(Vector3 direction)
	{
		if (!Physics.Raycast(transform.position + Vector3.up, direction, 1, layerMask))
		{
			this.gameObject.transform.position += direction;
		}
	}
}
