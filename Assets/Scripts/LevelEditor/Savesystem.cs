using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Savesystem
{
	public const string version = "0.4.0";


	// Maybe move this into the LevelEditorManagerScript
	public static void SaveLevel(string levelName)
	{
		LevelData levelData = GetLevelData(levelName);
		SaveToFile(levelData);
	}

	public static void LoadLevel(string levelName, ObjectTemplate template)
	{
		LevelData levelData = LoadFromFile(levelName);
		//TODO: Ask User if he is sure here maybe -> Errorcheck the current Version vs. File Version
		ClearLevel();
		BuildLevel(levelData, template);
	}

	//Till here
	public static void ClearLevel()
	{
		GameObject parent = GameObject.FindGameObjectWithTag("Parent");
		int childCount = parent.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject.Destroy(parent.transform.GetChild(i).gameObject);
		}
	}

	private static void SaveToFile(LevelData level)
	{
		string path = Application.dataPath + "/Levels/" + level.name + ".lvl";

		//Json / Not Encrypted
		//string json = JsonUtility.ToJson(level);
		//File.WriteAllText(path, json);

		//Encrpted
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream steam = new FileStream(path, FileMode.Create);
		formatter.Serialize(steam, level);
		steam.Close();

		Debug.Log("Saved to: " + path);
	}

	private static LevelData LoadFromFile(string levelName)
	{
		string path = Application.dataPath + "/Levels/" + levelName + ".lvl";

		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			LevelData level = formatter.Deserialize(stream) as LevelData;
			return level;
		}
		else
		{
			Debug.LogWarning("No Level found at" + path);
			return null;
		}
	}

	private static LevelData GetLevelData(string levelName)
	{
		//Potentially Cut this down to just Pushing EVERYTHING into Leveldata
		//and let the Leveldata handle the sorting of Objects?

		/////////////////////////////////////////////////////
		///ONE OF THE PLACES I NEED TO ADD NEW OBJECTS TO ///
		/////////////////////////////////////////////////////

		GameObject parent = GameObject.FindGameObjectWithTag("Parent");
		int childCount = parent.transform.childCount;
		GameObject[] objArr = new GameObject[childCount];
		for (int i = 0; i < childCount; i++)
		{
			objArr[i] = parent.transform.GetChild(i).gameObject;
		}

		return new LevelData(levelName, objArr);
	}

	private static void BuildLevel(LevelData level, ObjectTemplate template)
	{
		if (template == null)
		{
			Debug.LogError("No Template file found for building the Level");
			return;
		}

		GameObject parent = GameObject.FindGameObjectWithTag("Parent");


		/////////////////////////////////////////////////////
		///ONE OF THE PLACES I NEED TO ADD NEW OBJECTS TO ///
		/////////////////////////////////////////////////////

		//Player
		foreach (PlayerData obj in level.player)
		{
			if (obj.variant < template.player.Length)
			{
				GameObject.Instantiate(template.player[obj.variant], new Vector3(obj.position[0], 0, obj.position[1]), Quaternion.Euler(0, obj.rotation, 0), parent.transform);
			}
			else
			{
				Debug.LogWarning("The requested variant does not exist in the template - defaulting to Variant 0");
				obj.variant = 0;
			}
		}

		//Walls
		foreach (WallData obj in level.walls)
		{
			if (obj.variant < template.wall.Length)
			{
				GameObject.Instantiate(template.wall[obj.variant], new Vector3(obj.position[0], 0, obj.position[1]), Quaternion.Euler(0, obj.rotation, 0), parent.transform);
			}
			else
			{
				Debug.LogWarning("The requested variant does not exist in the template - defaulting to Variant 0");
				obj.variant = 0;
			}
		}

		//Buttons
		foreach (ButtonData obj in level.buttons)
		{
			if (obj.variant < template.button.Length)
			{
				GameObject buttonObj = GameObject.Instantiate(template.button[obj.variant], new Vector3(obj.position[0], 0, obj.position[1]), Quaternion.Euler(0, obj.rotation, 0), parent.transform);

				Button button = buttonObj.GetComponent<Button>();
				button.allowBoxes = obj.allowBoxes;
				button.isInverted = obj.isInverted;
				button.channel = obj.channel;
			}
			else
			{
				Debug.LogWarning("The requested variant does not exist in the template - defaulting to Variant 0");
				obj.variant = 0;
			}
		}

		//Doors
		foreach (DoorData obj in level.doors)
		{
			if (obj.variant < template.door.Length)
			{
				GameObject doorObj = GameObject.Instantiate(template.door[obj.variant], new Vector3(obj.position[0], 0, obj.position[1]), Quaternion.Euler(0, obj.rotation, 0), parent.transform);
				
				Door door = doorObj.GetComponent<Door>();
				door.isInverted = obj.isInverted;
				door.channel = obj.channel;
			}
			else
			{
				Debug.LogWarning("The requested variant does not exist in the template - defaulting to Variant 0");
				obj.variant = 0;
			}
		}

		//Stairs
		foreach (StairData obj in level.stairs)
		{
			if (obj.variant < template.stair.Length)
			{
				GameObject stairObj = GameObject.Instantiate(template.stair[obj.variant], new Vector3(obj.position[0], 0, obj.position[1]), Quaternion.Euler(0, obj.rotation, 0), parent.transform);

				LevelChange stair = stairObj.GetComponent<LevelChange>();
				stair.nextScene = obj.nextScene;
			}
			else
			{
				Debug.LogWarning("The requested variant does not exist in the template - defaulting to Variant 0");
				obj.variant = 0;
			}
		}

		//Moveables
		foreach (MoveableData obj in level.moveables)
		{
			if (obj.variant < template.moveable.Length)
			{
				GameObject.Instantiate(template.moveable[obj.variant], new Vector3(obj.position[0], 0, obj.position[1]), Quaternion.Euler(0, obj.rotation, 0), parent.transform);
			}
			else
			{
				Debug.LogWarning("The requested variant does not exist in the template - defaulting to Variant 0");
				obj.variant = 0;
			}
		}

		//Sentry
		foreach (SentryData obj in level.sentrys)
		{
			if (obj.variant < template.wall.Length)
			{
				GameObject sentry = GameObject.Instantiate(template.sentry[obj.variant], new Vector3(obj.position[0], 0, obj.position[1]), Quaternion.Euler(0, obj.rotation, 0), parent.transform);

				EnemySight sight = sentry.GetComponent<EnemySight>();
				sight.viewDistance = obj.viewDistance;
				sight.isContinous = obj.isContinous;
				sight.onTime = obj.onTime;
				sight.offTime = obj.offTime;
				sight.channel = obj.channel;
				sight.isInverted = obj.isInverted;
				sight.listens = obj.listens;
			}
			else
			{
				Debug.LogWarning("The requested variant does not exist in the template - defaulting to Variant 0");
				obj.variant = 0;
			}
		}
	}
}