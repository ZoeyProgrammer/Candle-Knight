using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Savesystem
{
	public const string version = "0.3.0";


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

		//Walls
		GameObject[] wallObj = GameObject.FindGameObjectsWithTag("Wall");
		Wall[] walls = new Wall[wallObj.Length];
		for (int i = 0; i < wallObj.Length; i++)
		{
			walls[i] = new Wall(wallObj[i]);
		}

		//Sentrys
		GameObject[] sentryObj = GameObject.FindGameObjectsWithTag("Sentry");
		Sentry[] sentrys = new Sentry[sentryObj.Length];
		for (int i = 0; i < sentryObj.Length; i++)
		{
			sentrys[i] = new Sentry(sentryObj[i]);
		}

		return new LevelData(levelName, walls, sentrys);
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

		//Walls
		foreach (Wall obj in level.walls)
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

		//TODO Fo Sentrys etc. as well
		foreach (Sentry obj in level.sentrys)
		{
			if (obj.variant < template.wall.Length)
			{
				GameObject sentry = GameObject.Instantiate(template.sentry[obj.variant], new Vector3(obj.position[0], 0, obj.position[1]), Quaternion.Euler(0, obj.rotation, 0), parent.transform);

				EnemySight sight = sentry.GetComponent<EnemySight>();
				sight.offTime = obj.offTime;
				sight.onTime = obj.onTime;
				sight.startState = obj.startState;
				sight.viewDistance = obj.viewDistance;
			}
			else
			{
				Debug.LogWarning("The requested variant does not exist in the template - defaulting to Variant 0");
				obj.variant = 0;
			}
		}
	}
}