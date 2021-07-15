using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Firepit : MonoBehaviour
{
	private bool isLit = false;
	[SerializeField] private VisualEffect flameVFX = null;

	private AudioSource audioSrc = null;
	private AudioManager audioMng = null;

	private void Start()
	{
		audioMng = GameObject.FindObjectOfType<AudioManager>();
		audioSrc = this.GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
			if (player != null && player.isLit && !isLit)
			{
				isLit = true;
				flameVFX.Play();
				if (audioMng != null && audioMng.fireKindle != null)
				{
					audioSrc.clip = audioMng.fireKindle;
					audioSrc.Play();
				}
			}
			else if (player != null && isLit && !player.isLit)
			{
				player.EnableFlame();
			}
		}
	}
}
