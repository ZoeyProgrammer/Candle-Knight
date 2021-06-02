using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [Tooltip("The Layers which Interrupt the Beam including Player")]
    [SerializeField] private LayerMask layerMask;

    [SerializeField] public int viewDistance = 5;
    [SerializeField] public bool isContinous = false;
    [SerializeField] public int onTime = 1;
    [SerializeField] public int offTime = 1;

    [SerializeField] public int channel = 0;
    [SerializeField] public bool isInverted = false; // Standard: True = SightOff | False = SightOn
    [SerializeField] public bool listens = true;

    private LineRenderer sightLine = null;

    public UnityEngine.Events.UnityEvent OnDetection;

    private bool currentState = true;
    private bool channelState = false;
    private bool isEnabled = true;
    private int lastTick = 0;
    private int tickTimer = 0;

    private void Start()
    {
        sightLine = GetComponent<LineRenderer>();
        sightLine.SetPosition(0, transform.position + Vector3.up * 1.5f);

        if (listens)
            UpdateState();
    }

    public void UpdateState()
    {
        channelState = GameManger.channels[channel];
        if (channelState && listens)
        {
            if (isInverted)
                isEnabled = true;
            else
                isEnabled = false;
        }
        else if (listens)
        {
            if (isInverted)
                isEnabled = false;
            else
                isEnabled = true;
        }
        else
		{
            isEnabled = true;
		}
    }

    void Update()
    {
        //Update if Channel state has changed
        if (listens && GameManger.channels[channel] != channelState)
        {
            UpdateState();
        }

        //Tick System
        if (!isContinous && GameManger.tick != lastTick)
		{
            tickTimer += 1;
            lastTick = GameManger.tick;

            if (currentState && tickTimer >= onTime)
			{
                // Deactivate the Sightline
                tickTimer = 0;
                currentState = false;
            }
            else if (!currentState && tickTimer >= offTime)
			{
                // Activate the Sightline
                tickTimer = 0;
                currentState = true;
            }
		}

        //Player Detection - Need to change this up so it only detects the player once until he gets back out again
        if (currentState && isEnabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 1.5f, this.transform.forward, out hit, viewDistance, layerMask))
			{
                sightLine.SetPosition(1, hit.point);
                if (hit.collider.gameObject.tag == "Player")
                {
                    //OnDetection.Invoke(); //Change this so it just does something to the Player
                }
            }
            else
			{
                sightLine.SetPosition(1, (transform.position + Vector3.up * 1.5f) + transform.forward * viewDistance);
            }

            sightLine.enabled = true;
        }
        else
		{
            sightLine.enabled = false;
		}
    }
}
