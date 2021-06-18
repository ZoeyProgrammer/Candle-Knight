using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectTemplate", menuName = "LevelEditor/ObjectTemplate", order = 1)]
public class ObjectTemplate : ScriptableObject
{
    /////////////////////////////////////////////////////
    ///ONE OF THE PLACES I NEED TO ADD NEW OBJECTS TO ///
    /////////////////////////////////////////////////////
    
    public GameObject[] player = null;
    public GameObject[] wall = null;
    public GameObject[] button = null;
    public GameObject[] door = null;
    public GameObject[] stair = null;
    public GameObject[] moveable = null;
    public GameObject[] sentry = null;
    public GameObject[] firepit = null;

    public GameObject[][] Contains()
	{
        List<GameObject[]> list = new List<GameObject[]>();

        if (player != null)
            list.Add(player);

        if (wall != null)
            list.Add(wall);

        if (button != null)
            list.Add(button);

        if (door != null)
            list.Add(door);

        if (stair != null)
            list.Add(stair);

        if (moveable != null)
            list.Add(moveable);

        if (sentry != null)
            list.Add(sentry);

        if (firepit != null)
            list.Add(firepit);

        return list.ToArray();
	}
}