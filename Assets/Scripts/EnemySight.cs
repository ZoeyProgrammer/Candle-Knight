using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [Tooltip("The Layers which Interrupt the Beam including Player")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] public bool startState = true;
    [SerializeField] public int viewDistance = 5;
    [SerializeField] public float onTime = 1f;
    [SerializeField] public float offTime = 1f;

    private LineRenderer laser = null;

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

        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, transform.position + Vector3.up * 0.5f);
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
        if (currentState)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, this.transform.forward, out hit, viewDistance, layerMask))
			{
                laser.SetPosition(1, hit.point);
                if (hit.collider.gameObject.tag == "Player")
                {
                    OnDetection.Invoke();
                }
            }
            else
			{
                laser.SetPosition(1, (transform.position + Vector3.up * 0.5f) + transform.forward * viewDistance);
            }

            laser.enabled = true;
        }
        else
		{
            laser.enabled = false;
		}
    }
}
