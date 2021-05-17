using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderInput : MonoBehaviour
{
    [SerializeField] Slider slider = null;
    [SerializeField] InputField input = null;

    public int MinValue { get => this.minValue; set => SetMin(value); }
    private int minValue = 0;
    public int MaxValue { get => this.maxValue; set => SetMax(value); }
    private int maxValue = 1;
    public int Value { get => this.minValue; }
    private int currentValue = 0;

    public UnityEngine.Events.UnityEvent<int> OnUpdate;

    private void SetMax(int value)
	{
        slider.maxValue = value;
        maxValue = value;
	}

    private void SetMin(int value)
    {
        slider.minValue = value;
        minValue = value;
    }

    public void UpdateNull()
	{
        currentValue = 0;
        input.text = 0.ToString();
        slider.value = 0;
	}

    public void SliderUpdate()
	{
        Debug.Log("Slider got Updated");
        if (slider.value <= maxValue && slider.value >= minValue)
		{
            currentValue = (int)slider.value;
            input.text = slider.value.ToString();
            OnUpdate.Invoke(currentValue);
        }
        else if (slider.value > maxValue)
		{
            currentValue = maxValue;
            input.text = maxValue.ToString();
            OnUpdate.Invoke(currentValue);
        }
        else if (slider.value < minValue)
		{
            currentValue = minValue;
            input.text = minValue.ToString();
            OnUpdate.Invoke(currentValue);
        }
	}

    public void InputUpdate(string text)
	{
        Debug.Log("Input got Updated");
        int value = int.Parse(text);
        if (value <= maxValue && value >= minValue)
        {
            currentValue = value;
            slider.value = value;
        }
        else if (value > maxValue)
        {
            currentValue = maxValue;
            slider.value = maxValue;
            input.text = maxValue.ToString();
        }
        else if (value < minValue)
        {
            currentValue = minValue;
            slider.value = minValue;
            input.text = minValue.ToString();
        }
    }
}
