using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	[SerializeField] public bool allowBoxes = true;
	[SerializeField] public int channel = 0;
	[SerializeField] public bool isInverted = false;

	private bool isPressed = false;

	private void Start()
	{
		GameManger.channels[channel] = isInverted;
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
			UpdateState();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" || (allowBoxes && other.tag == "Moveables"))
		{
			isPressed = false;
			UpdateState();
		}
	}
}
