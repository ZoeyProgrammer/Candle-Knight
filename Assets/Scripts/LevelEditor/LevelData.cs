using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
	public string name;
	public string version;
	public string date;
	public Wall[] walls;
	public Sentry[] sentrys;

	public LevelData(string levelName, Wall[] wallArr, Sentry[] sentryArr)
	{
		name = levelName;
		version = Savesystem.version;
		date = System.DateTime.Now.ToString("MM/dd/yyyy");
		walls = wallArr;
		sentrys = sentryArr;
	}
}

[System.Serializable]
public class Object
{
	public int variant = 0;
	public float[] position = new float[2];
}

[System.Serializable]
public class Wall : Object
{
	public Wall(GameObject wall)
	{
		position[0] = wall.transform.position.x;
		position[1] = wall.transform.position.z;

		ObjectMarker data = wall.GetComponent<ObjectMarker>();
		if (data != null)
			variant = data.data.variant;
	}
}

[System.Serializable]
public class Sentry : Object
{
	public bool startState;
	int viewDistance;
	float onTime;
	float offTime;

	public Sentry(GameObject sentry)
	{
		position[0] = sentry.transform.position.x;
		position[1] = sentry.transform.position.z;

		ObjectMarker data = sentry.GetComponent<ObjectMarker>();
		if (data != null)
			variant = data.data.variant;

		EnemySight sight = sentry.GetComponent<EnemySight>();
		if (sight != null)
		{
			startState = sight.startState;
			viewDistance = sight.viewDistance;
			onTime = sight.onTime;
			offTime = sight.offTime;
		}
	}
}
