using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] public AudioClip atmoLoop, buttonPress, buttonDeactivation, doorOpen, doorClosing, playerStep, stairs, boxPush, sentryShoot, sentryDrip, fireKindle, fireExtinguish = null;
	[SerializeField] public AudioClip[] backgroundMusic, sentrWhispers = null;
	[SerializeField] private AudioSource backgroundSource = null;

	private void Start()
	{
		if (backgroundSource != null)
			PlayBackgroundMusic();
		else
			Debug.LogWarning("No Audio Source Found");
	}

	private void Update()
	{
		if (backgroundSource.isPlaying == false)
		{
			PlayBackgroundMusic();
		}
	}

	private void PlayBackgroundMusic()
	{
		int clip = Random.Range(0, backgroundMusic.Length);
		backgroundSource.clip = backgroundMusic[clip];
		backgroundSource.Play();
	}

	//Just save the clips here, and make them available for everywhere else for local SoundSources (If the Design has some directional soulution)
}
