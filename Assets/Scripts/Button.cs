using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	[SerializeField] bool allowBoxes = true;

	public UnityEngine.Events.UnityEvent OnPress;
    public UnityEngine.Events.UnityEvent OnRemoved;

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" || (allowBoxes && other.tag == "Box" ))
		{
			OnPress.Invoke();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" || (allowBoxes && other.tag == "Box"))
		{
			OnRemoved.Invoke();
		}
	}
}
