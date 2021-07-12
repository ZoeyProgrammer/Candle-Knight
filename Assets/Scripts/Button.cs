using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	[SerializeField] public bool allowBoxes = true;
	[SerializeField] public int channel = 0;
	[SerializeField] public bool isInverted = false;

	private bool isPressed = false;
	private AudioSource audioSrc = null;
	private AudioManager audioMng = null;

	private void Start()
	{
		GameManger.channels[channel] = isInverted;
		audioMng = GameObject.FindObjectOfType<AudioManager>();
		audioSrc = this.GetComponent<AudioSource>();
	}

	public void UpdateState()
	{
		if (isPressed)
			GameManger.channels[channel] = !isInverted;
		else
			GameManger.channels[channel] = isInverted;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" || (allowBoxes && other.tag == "Moveables" ))
		{
			isPressed = true;
			if (audioMng != null && audioMng.buttonPress != null)
			{
				audioSrc.clip = audioMng.buttonPress;
				audioSrc.Play();
			}
			else
			{
				Debug.LogWarning("Not found sound ButtonPress");
			}
			UpdateState();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" || (allowBoxes && other.tag == "Moveables"))
		{
			isPressed = false;
			if (audioMng != null && audioMng.buttonDeactivation != null)
			{
				audioSrc.clip = audioMng.buttonDeactivation;
				audioSrc.Play();
			}
			else
			{
				Debug.LogWarning("Not found sound ButtonDeactivation");
			}
			UpdateState();
		}
	}
}
