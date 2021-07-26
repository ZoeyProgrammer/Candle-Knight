using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Tooltip("True = Closed | False = Open")]
   //[SerializeField] public int channel = 0;
    [SerializeField] public int[] channels = new int[] { };
    [SerializeField] public bool isInverted = false; // Standard: True = Closed | False = Open

    private bool currentState = true;
    //private bool channelState = false;
    private bool[] channelStates = new bool[] { };

    private AudioSource audioSrc = null;
    private AudioManager audioMng = null;

    private void Start()
	{
        if (channels != null)
            channelStates = new bool[channels.Length];

        UpdateState();
        audioMng = GameObject.FindObjectOfType<AudioManager>();
        audioSrc = this.GetComponent<AudioSource>();
    }

	private void Update()
	{
        if (channels == null)
            return;

        if (channels.Length != channelStates.Length)
            channelStates = new bool[channels.Length];

        //Update if Channel state has changed
        for (int i = 0; i < channels.Length; i++)
		{
            if (GameManger.channels[channels[i]] != channelStates[i])
            {
                UpdateState();
            }
        }
	}

    public void UpdateState()
    {
        if (channels == null)
            return;

        for (int i = 0; i < channels.Length; i++)
        {
            channelStates[i] = GameManger.channels[channels[i]];
        }

        if (AreAllTrue(channelStates)) //Check if all ChannelStates are True here
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
            if (audioMng != null && audioMng.doorOpen != null)
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
            if (audioMng != null && audioMng.doorClosing != null)
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

    public static bool AreAllTrue(bool[] array)
    {
        foreach (bool b in array)
        {
            if (!b)
                return false;
        }
        return true;
    }
}
