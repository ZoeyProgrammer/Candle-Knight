using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
	[Tooltip("The Layers which get detected as Walls.")]
	[SerializeField] private LayerMask layerMask;

	private InputMaster inputMaster = null;
	private bool isAllowedToMove = true;

	private Vector2 controllerPos = Vector2.zero;

	void Awake()
	{
		inputMaster = new InputMaster();

		inputMaster.Player.Forwards.performed += Forwards;
		inputMaster.Player.Backwards.performed += Backwards;
		inputMaster.Player.Right.performed += Right;
		inputMaster.Player.Left.performed += Left;
	}

	private void OnEnable()
	{
		inputMaster.Enable();
	}

	private void OnDisable()
	{
		inputMaster.Disable();
	}

	void Forwards(InputAction.CallbackContext context)
	{
		if (!Physics.Raycast(transform.position + Vector3.up, Vector3.forward, 1, layerMask))
		{
			this.gameObject.transform.position += Vector3.forward;
		}
	}

	void Backwards(InputAction.CallbackContext context)
	{
		if (!Physics.Raycast(transform.position + Vector3.up, -Vector3.forward, 1, layerMask))
		{
			this.gameObject.transform.position -= Vector3.forward;
		}
	}

	void Right(InputAction.CallbackContext context)
	{
		if (!Physics.Raycast(transform.position + Vector3.up, Vector3.right, 1, layerMask))
		{
			this.gameObject.transform.position += Vector3.right;
		}
	}	

	void Left(InputAction.CallbackContext context)
	{
		if (!Physics.Raycast(transform.position + Vector3.up, -Vector3.right, 1, layerMask))
		{
			this.gameObject.transform.position -= Vector3.right;
		}
	}
}
