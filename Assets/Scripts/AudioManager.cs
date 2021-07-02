using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] public AudioClip backgroundMusic, buttonPress, buttonDeactivation, playerStep, stairs, boxPush, sentryShoot, sentryDrip, fireCrackle, fireKindle, fireExtinguish = null;
	[SerializeField] private AudioSource backgroundSource = null;

	private void Start()
	{
		if (backgroundSource != null)
			PlayBackgroundMusic();
	}

	private void PlayBackgroundMusic()
	{
		backgroundSource.clip = backgroundMusic;
		backgroundSource.Play();
	}

	//Just save the clips here, and make them available for everywhere else for local SoundSources (If the Design has some directional soulution)
}
