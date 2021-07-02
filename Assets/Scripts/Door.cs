using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Tooltip("True = Closed | False = Open")]
    [SerializeField] public int channel = 0;
    [SerializeField] public bool isInverted = false; // Standard: True = Closed | False = Open

    private bool currentState = true;
    private bool channelState = false;

    private AudioSource audioSrc = null;
    private AudioManager audioMng = null;

    private void Start()
	{
        UpdateState();
        audioMng = GameObject.FindObjectOfType<AudioManager>();
        audioSrc = this.GetComponent<AudioSource>();
    }

	private void Update()
	{
        //Update if Channel state has changed
		if (GameManger.channels[channel] != channelState)
		{
            UpdateState();
        }
	}

    public void UpdateState()
	{
        channelState = GameManger.channels[channel];
        if (channelState)
        {
            if (isInverted)
                CloseDoor();
            else
                OpenDoor();
        }
        else
        {
            if (isInverted)
                OpenDoor();
            else
                CloseDoor();
        }
    }

	public void OpenDoor()
	{
        if (currentState != false)
		{
            this.transform.position += Vector3.up * 2; //Temporary
            currentState = false;
            if (audioMng.doorOpen != null)
            {
                audioSrc.clip = audioMng.doorOpen;
                audioSrc.Play();
            }
            else
            {
                Debug.LogWarning("Not found sound DoorOpen");
            }
        }
	}

    public void CloseDoor()
	{
        if (currentState != true)
        {
            this.transform.position -= Vector3.up * 2; //Temporary
            currentState = true;
            if (audioMng.doorClosing != null)
            {
                audioSrc.clip = audioMng.doorClosing;
                audioSrc.Play();
            }
            else
            {
                Debug.LogWarning("Not found sound DoorClosing");
            }
        }
    }

    public void ToggleDoor()
	{
        if (!currentState)
            CloseDoor();
        else
            OpenDoor();
    }
}
