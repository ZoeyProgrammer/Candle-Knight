using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	[Tooltip("The Layers which get detected as Walls.")]
	[SerializeField] private LayerMask layerMask;

	private AudioSource audioSrc = null;
	private AudioManager audioMng = null;

	private void Start()
	{
		audioMng = GameObject.FindObjectOfType<AudioManager>();
		audioSrc = this.GetComponent<AudioSource>();
	}

	public void Push(Vector3 direction)
	{
		Debug.DrawRay(transform.position + Vector3.up * 0.5f, direction, Color.red, 1f);
		if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, 1, layerMask))
		{
			this.gameObject.transform.position += direction;
			if (audioMng.boxPush != null)
			{
				audioSrc.clip = audioMng.boxPush;
				audioSrc.Play();
			}
		}
	}
}
