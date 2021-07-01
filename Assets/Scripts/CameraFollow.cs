using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] bool followX, followY, followZ = false;
    [SerializeField] int offsetX, offsetY, offsetZ = 0;

    private Transform player = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("No Player Found for Camera to Attach to.. searching..");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
		{
            if (followX)
                transform.position = new Vector3(player.position.x + offsetX, transform.position.y, transform.position.z);
            if (followY)
                transform.position = new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);
            if (followZ)
                transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z + offsetZ);
        }
        else
		{
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }
}
