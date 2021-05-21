using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Tooltip("True = Closed | False = Open")]
    [SerializeField] public int channel = 0;
    [SerializeField] public bool isInverted = false;

    private bool currentState = true;
    private bool channelState = false;

	private void Start()
	{
        UpdateState();
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
        }
	}

    public void CloseDoor()
	{
        if (currentState != true)
        {
            this.transform.position -= Vector3.up * 2; //Temporary
            currentState = true;
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
