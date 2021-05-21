using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInspector : MonoBehaviour
{
	[SerializeField] private Toggle isInvertedToggle = null;
	[SerializeField] private SliderInput channelSlider = null;

	private LevelEditorManager manage = null;
	private Door currentSelected = null;

	private void Awake()
	{
		manage = GameObject.FindObjectOfType<LevelEditorManager>();

		Door door = manage.SelectedObject.GetComponent<Door>();
		if (door != null)
		{
			currentSelected = door;
			isInvertedToggle.isOn = door.isInverted;
			channelSlider.Value = door.channel;
		}
		else
			Debug.LogWarning("No Door Found");
	}

	public void IsInvertedToggle(bool value)
	{
		currentSelected.isInverted = value;
		currentSelected.UpdateState();
	}

	public void ChannelSlider(int value)
	{
		currentSelected.channel = value;
		currentSelected.UpdateState();
	}
}
