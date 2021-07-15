using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class CharacterMovement : MonoBehaviour
{
	[Tooltip("The Layers which get detected as Walls.")]
	[SerializeField] private LayerMask layerMask;

	[SerializeField] private VisualEffect headFlame = null;
	
	private InputMaster inputMaster = null;
	public bool isAllowedToMove = true;
	public bool isLit = true;

	private Vector2 controllerPos = Vector2.zero;
	[SerializeField] private AudioSource stepAudioSrc, flameAudioSrc = null;
	private AudioManager audioMng = null;

	void Awake()
	{
		inputMaster = new InputMaster();

		inputMaster.Player.Forwards.performed += Forwards;
		inputMaster.Player.Backwards.performed += Backwards;
		inputMaster.Player.Right.performed += Right;
		inputMaster.Player.Left.performed += Left;
		inputMaster.Player.TurnLeft.performed += TurnLeft;
		inputMaster.Player.TurnRight.performed += TurnLeft;
	}

	private void Start()
	{
		audioMng = GameObject.FindObjectOfType<AudioManager>();
	}

	private void OnEnable()
	{
		inputMaster.Enable();
	}

	private void OnDisable()
	{
		inputMaster.Disable();
	}

	public void DisableFlame()
	{
		if (audioMng != null && audioMng.fireExtinguish != null)
		{
			flameAudioSrc.clip = audioMng.fireExtinguish;
			flameAudioSrc.Play();
		}

		isLit = false;
		headFlame.Stop();
	}

	public void EnableFlame()
	{
		if (audioMng != null && audioMng.fireKindle != null)
		{
			flameAudioSrc.clip = audioMng.fireKindle;
			flameAudioSrc.Play();
		}

		isLit = true;
		headFlame.Play();
	}

	private void PlayStepSound()
	{
		if (audioMng != null && audioMng.playerStep != null)
		{
			stepAudioSrc.clip = audioMng.playerStep;
			stepAudioSrc.Play();
		}
	}

	void Forwards(InputAction.CallbackContext context)
	{
		RaycastHit hit;
		if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.forward, out hit, 1, layerMask))
		{
			if (isAllowedToMove)
			{
				this.gameObject.transform.position += Vector3.forward;
				GameManger.tick++;
				PlayStepSound();
			}
		}
		else if (hit.collider.gameObject.GetComponent<Box>() != null)
		{
			hit.collider.gameObject.GetComponent<Box>().Push(Vector3.forward);
		}
	}

	void Backwards(InputAction.CallbackContext context)
	{
		RaycastHit hit;
		if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, -Vector3.forward, out hit, 1, layerMask))
		{
			if (isAllowedToMove)
			{
				this.gameObject.transform.position -= Vector3.forward;
				GameManger.tick++;
				PlayStepSound();
			}
		}
		else if (hit.collider.gameObject.GetComponent<Box>() != null)
		{
			hit.collider.gameObject.GetComponent<Box>().Push(-Vector3.forward);
		}
	}

	void Right(InputAction.CallbackContext context)
	{
		RaycastHit hit;
		if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right, out hit, 1, layerMask))
		{
			if (isAllowedToMove)
			{
				this.gameObject.transform.position += Vector3.right;
				GameManger.tick++;
				PlayStepSound();
			}
		}
		else if (hit.collider.gameObject.GetComponent<Box>() != null)
		{
			hit.collider.gameObject.GetComponent<Box>().Push(Vector3.right);
		}
	}

	void Left(InputAction.CallbackContext context)
	{
		RaycastHit hit;
		if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, -Vector3.right, out hit, 1, layerMask))
		{
			if (isAllowedToMove)
			{
				this.gameObject.transform.position -= Vector3.right;
				GameManger.tick++;
				PlayStepSound();
			}
		}
		else if (hit.collider.gameObject.GetComponent<Box>() != null)
		{
			hit.collider.gameObject.GetComponent<Box>().Push(-Vector3.right);
		}
	}

	void TurnLeft(InputAction.CallbackContext context)
	{
		//this.transform.rotation = Quaternion.Euler(this.transform.rotation.x,this.transform.rotation.y +90,this.transform.rotation.z);
	}

	void TurnRight(InputAction.CallbackContext context)
	{
		//this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y -90, this.transform.rotation.z);
	}
}
