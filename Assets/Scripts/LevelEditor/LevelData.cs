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
	public float[] position = new float[2];
}

[System.Serializable]
public class Wall : Object
{
	public int variant;
	public Wall(GameObject wall)
	{
		variant = 0; //TODO: Actually implement having Variants
		position[0] = wall.transform.position.x;
		position[1] = wall.transform.position.z;
	}
}

[System.Serializable]
public class Sentry : Object
{
	public bool startState;
	int viewDistance;
	float onTime;
	float offTime;

	public Sentry(EnemySight sentry)
	{
		startState = sentry.startState;
		viewDistance = sentry.viewDistance;
		onTime = sentry.onTime;
		offTime = sentry.offTime;
		position[0] = sentry.transform.position.x;
		position[1] = sentry.transform.position.z;
	}
}
