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

    [SerializeField] private int minRandomInterval = 2;
    [SerializeField] private int maxRandomInterval = 10;
    [SerializeField] private AudioSource whispersAudioSrc, mainAudioSrc;

    private LineRenderer sightLine = null;
    private AudioManager audioMng = null;

    public UnityEngine.Events.UnityEvent OnDetection;

    private bool currentState = true;
    private bool channelState = false;
    private bool isEnabled = true;
    private int lastTick = 0;
    private int tickTimer = 0;
    private int whisperTimer = 0;
    private int whisperTime = 0;

    private void Start()
    {
        sightLine = GetComponent<LineRenderer>();
        sightLine.SetPosition(0, transform.position + Vector3.up * 1.5f);

        audioMng = GameObject.FindObjectOfType<AudioManager>();
        whisperTime = Random.Range(minRandomInterval, maxRandomInterval + 1);

        if (listens)
            UpdateState();
    }


    void Update()
    {
        //Update if Channel state has changed
        if (listens && GameManger.channels[channel] != channelState)
        {
            UpdateState();
        }

        TickSystem();
        PlayerDetection();
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

    private void PlayWhispers()
	{
        if (audioMng.sentrWhispers != null)
        {
            int clip = Random.Range(0, audioMng.sentrWhispers.Length);
            whispersAudioSrc.clip = audioMng.sentrWhispers[clip];
            whispersAudioSrc.Play();
        }

        whisperTime = Random.Range(minRandomInterval, maxRandomInterval + 1);
        whisperTimer = 0;
    }

    private void TickSystem()
	{
        //For Sightline Turining
        if (!isContinous && GameManger.tick != lastTick)
        {
            lastTick = GameManger.tick;

            //For Sightlines
            if (!isContinous)
			{
                tickTimer += 1;
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

            //For Whispers
            whisperTimer += 1;
            if (whisperTimer >= whisperTime)
			{
                if (audioMng != null && audioMng.sentrWhispers != null)
                {
                    PlayWhispers();
                }
            }
        }
    }

    //Player Detection - Need to change this up so it only detects the player once until he gets back out again
    private void PlayerDetection()
	{
        if (currentState && isEnabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 1.5f, this.transform.forward, out hit, viewDistance, layerMask))
            {
                sightLine.SetPosition(1, hit.point);
                if (hit.collider.gameObject.tag == "Player")
                {
                    CharacterMovement player = hit.collider.gameObject.GetComponent<CharacterMovement>();
                    if (player != null)
                        player.DisableFlame();
                }
            }
            else
            {
                sightLine.SetPosition(1, (transform.position + Vector3.up * 1.5f) + transform.forward * viewDistance);
            }

            if (!sightLine.enabled)
			{
                sightLine.enabled = true;
                if (audioMng != null && audioMng.sentryShoot != null)
                {
                    mainAudioSrc.clip = audioMng.sentryShoot;
                    mainAudioSrc.Play();
                }
            }
        }
        else
        {
            if (sightLine.enabled)
			{
                sightLine.enabled = false;
                if (audioMng != null && audioMng.sentryDrip != null)
                {
                    mainAudioSrc.clip = audioMng.sentryDrip;
                    mainAudioSrc.Play();
                }
            }
        }
    }
}
