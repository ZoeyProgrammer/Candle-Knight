using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Tooltip("True = Closed | False = Open")]
    [SerializeField] bool startState = false;
    
    private bool currentState = true; 

    // Start is called before the first frame update
    void Start()
    {
        if (!startState)
            OpenDoor();
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
