using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnPress;
    public UnityEngine.Events.UnityEvent OnRemoved;


    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Box" || other.tag == "Player")
		{
			OnPress.Invoke();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Box" || other.tag == "Player")
		{
			OnRemoved.Invoke();
		}
	}
}
