using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [Tooltip("The Layers which Interrupt the Beam including Player")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] bool startState = true;
    [SerializeField] int viewDistance = 5;
    [SerializeField] float onTime = 1f;
    [SerializeField] float offTime = 1f;

    public UnityEngine.Events.UnityEvent OnDetection;

    private bool currentState = false;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        currentState = startState;
        if (currentState)
            timer = onTime;
        else
            timer = offTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!currentState)
			{
                timer = onTime;
                currentState = true;
            }
            else
			{
                timer = offTime;
                currentState = false;
            }
        }

        //Player Detection - Need to change this up so it only detects the player once until he gets back out again
        RaycastHit hit;
        if (currentState && Physics.Raycast(transform.position + Vector3.up * 0.5f, this.transform.forward, out hit, viewDistance, layerMask))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                OnDetection.Invoke();
            }
        }
    }
}
