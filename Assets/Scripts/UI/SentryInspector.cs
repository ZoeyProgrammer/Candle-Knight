using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentryInspector : MonoBehaviour
{
	[SerializeField] private Toggle listensToggle, isInvertedToggle, isContinous;
	[SerializeField] private SliderInput channelSlider, onTimeSlider, offTimeSlider, viewDistanceSlider;

	private LevelEditorManager manage = null;
	private EnemySight currentSelected = null;

	private void Awake()
	{
		manage = GameObject.FindObjectOfType<LevelEditorManager>();

		EnemySight sentry = manage.SelectedObject.GetComponent<EnemySight>();
		if (sentry != null)
		{
			currentSelected = sentry;
			Debug.Log(sentry.name);

			listensToggle.isOn = sentry.listens;
			isInvertedToggle.isOn = sentry.isInverted;
			isContinous.isOn = sentry.isContinous;

			channelSlider.Value = sentry.channel;
			onTimeSlider.Value = sentry.onTime;
			offTimeSlider.Value = sentry.offTime;
			viewDistanceSlider.Value = sentry.viewDistance;
		}
		else
			Debug.LogWarning("No Sentry Found");
	}


	public void ListensToggle(bool value)
	{
		currentSelected.listens = value;
		currentSelected.UpdateState();
	}
	public void IsInvertedToggle(bool value)
	{
		currentSelected.isInverted = value;
		currentSelected.UpdateState();
	}
	public void IsContinousToggle(bool value)
	{
		currentSelected.isContinous = value;
		currentSelected.UpdateState();
	}

	public void ChannelSlider(int value)
	{
		currentSelected.channel = value;
		currentSelected.UpdateState();
	}
	public void OnTimeSlider(int value)
	{
		currentSelected.onTime = value;
		currentSelected.UpdateState();
	}
	public void OffTimeSlider(int value)
	{
		currentSelected.offTime = value;
		currentSelected.UpdateState();
	}
	public void ViewDistanceSlider(int value)
	{
		currentSelected.viewDistance = value;
		currentSelected.UpdateState();
	}
}
