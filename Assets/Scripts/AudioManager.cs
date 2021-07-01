using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioClip backgroundMusic, tickSound = null;
	[SerializeField] private AudioSource backgroundSource, SFXSource1, SFXSource2 = null;

	private void Start()
	{
		PlayBackgroundMusic();
	}

	private void PlayBackgroundMusic()
	{
		backgroundSource.clip = backgroundMusic;
		backgroundSource.Play();
	}

	// Depending on how the Design works:
	//Either: A SoundSource for every kind of Soundeffect here (If all should play at the same time)
	//Or: A few SoundSources for different kinds of SFX to Layer them over another (If the design just says that only a few should play at a time)
	//Or: Just save the clips here, and make them available for everywhere else for local SoundSources (If the Design has some directional soulution)
}
