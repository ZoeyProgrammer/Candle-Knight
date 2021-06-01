using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
	public string name;
	public string version;
	public string date;
	public PlayerData[] player;
	public WallData[] walls;
	public ButtonData[] buttons;
	public DoorData[] doors;
	public StairData[] stairs;
	public MoveableData[] moveables;
	public SentryData[] sentrys;

	public LevelData(string levelName,WallData[] wallArr, SentryData[] sentryArr)
	{
		name = levelName;
		version = Savesystem.version;
		date = System.DateTime.Now.ToString("MM/dd/yyyy");
		walls = wallArr;
		sentrys = sentryArr;
	}

	public LevelData(string levelName, GameObject[] objArr)
	{
		name = levelName;
		version = Savesystem.version;
		date = System.DateTime.Now.ToString("MM/dd/yyyy");
		
		List<PlayerData> playerList = new List<PlayerData>();
		List<WallData> wallsList = new List<WallData>();
		List<ButtonData> buttonsList = new List<ButtonData>();
		List<DoorData> doorsList = new List<DoorData>();
		List<StairData> stairsList = new List<StairData>();
		List<MoveableData> moveablesList = new List<MoveableData>();
		List<SentryData> sentryList = new List<SentryData>();
		foreach (GameObject obj in objArr)
		{
			if (obj.tag == "Player")
				playerList.Add(new PlayerData(obj));
			if (obj.tag == "Wall")
				wallsList.Add(new WallData(obj));
			if (obj.tag == "Button")
				buttonsList.Add(new ButtonData(obj));
			if (obj.tag == "Door")
				doorsList.Add(new DoorData(obj));
			if (obj.tag == "Stair")
				stairsList.Add(new StairData(obj));
			if (obj.tag == "Moveables")
				moveablesList.Add(new MoveableData(obj));
			if (obj.tag == "Sentry")
				sentryList.Add(new SentryData(obj));
		}
		player = playerList.ToArray();
		walls = wallsList.ToArray();
		buttons = buttonsList.ToArray();
		doors = doorsList.ToArray();
		stairs = stairsList.ToArray();
		moveables = moveablesList.ToArray();
		sentrys = sentryList.ToArray();
	}
}

[System.Serializable]
public class ObjectData
{
	public int variant = 0;
	public float[] position = new float[2];
	public int rotation;

	public ObjectData(GameObject obj)
	{
		position[0] = obj.transform.position.x;
		position[1] = obj.transform.position.z;
		rotation = (int)obj.transform.rotation.eulerAngles.y;

		ObjectMarker data = obj.GetComponent<ObjectMarker>();
		if (data != null)
			variant = data.data.variant;
	}
}

/////////////////////////////////////////////////////
///ONE OF THE PLACES I NEED TO ADD NEW OBJECTS TO ///
/////////////////////////////////////////////////////

[System.Serializable]
public class PlayerData : ObjectData
{
	public PlayerData(GameObject obj) : base(obj)
	{
	}
}

[System.Serializable]
public class WallData : ObjectData
{
	public WallData(GameObject obj) : base(obj)
	{
	}
}

[System.Serializable]
public class ButtonData : ObjectData
{
	public bool allowBoxes = true;
	public int channel = 0;
	public bool isInverted = false;
	
	public ButtonData(GameObject obj) : base(obj)
	{
		Button button = obj.GetComponent<Button>();
		if (button != null)
		{
			allowBoxes = button.allowBoxes;
			channel = button.channel;
			isInverted = button.isInverted;
		}
	}
}

[System.Serializable]
public class DoorData : ObjectData
{
	public int channel = 0;
	public bool isInverted = false;

	public DoorData(GameObject obj) : base(obj)
	{
		Door door = obj.GetComponent<Door>();
		if (door != null)
		{
			channel = door.channel;
			isInverted = door.isInverted;
		}
	}
}

[System.Serializable]
public class StairData : ObjectData
{
	public string nextScene = null;
	public StairData(GameObject obj) : base(obj)
	{
		LevelChange stair = obj.GetComponent<LevelChange>();
		if (stair != null)
		{
			nextScene = stair.nextScene;
		}
	}
}

[System.Serializable]
public class MoveableData : ObjectData
{
	public MoveableData(GameObject obj) : base(obj)
	{
	}
}

[System.Serializable]
public class SentryData : ObjectData
{
	public bool startState;
	public int viewDistance;
	public float onTime;
	public float offTime;

	public SentryData(GameObject obj) : base(obj)
	{
		EnemySight sight = obj.GetComponent<EnemySight>();
		if (sight != null)
		{
			startState = sight.startState;
			viewDistance = sight.viewDistance;
			onTime = sight.onTime;
			offTime = sight.offTime;
		}
	}
}
