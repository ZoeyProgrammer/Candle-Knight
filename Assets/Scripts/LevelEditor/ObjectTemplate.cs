using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectTemplate", menuName = "LevelEditor/ObjectTemplate", order = 1)]
public class ObjectTemplate : ScriptableObject
{
    public GameObject[] wall = null;
    public GameObject[] sentry = null;
    //All other Objects here

    // Function which puts everything into a Array
    public GameObject[][] Contains()
	{
        List<GameObject[]> list = new List<GameObject[]>();

        if (wall[0] != null)
            list.Add(wall);

        if (sentry[0] != null)
            list.Add(sentry);

		return list.ToArray();
	}
}