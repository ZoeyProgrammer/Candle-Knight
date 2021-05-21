using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInspector : MonoBehaviour
{
	[SerializeField] private Toggle allowBoxesToggle, isInvertedToggle = null;
	[SerializeField] private SliderInput channelSlider = null;

	private LevelEditorManager manage = null;
	private Button currentSelected = null;

	private void Awake()
	{
		manage = GameObject.FindObjectOfType<LevelEditorManager>();

		Button button = manage.SelectedObject.GetComponent<Button>();
		if (button != null)
		{
			currentSelected = button;
			allowBoxesToggle.isOn = button.allowBoxes;
			isInvertedToggle.isOn = button.isInverted;
			channelSlider.Value = button.channel;
		}
		else
			Debug.LogWarning("No Button Found");
	}

	public void AllowBoxesToggle(bool value)
	{
		currentSelected.allowBoxes = value;
		currentSelected.UpdateState();
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
