using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Savesystem
{
	public const string version = "0.2.0";


	// Maybe move this into the LevelEditorManagerScript
	public static void SaveLevel(string levelName)
	{
		LevelData levelData = GetLevelData(levelName);
		SaveToFile(levelData);
	}

	public static void LoadLevel(string levelName)
	{
		LevelData levelData = LoadFromFile(levelName);
		//TODO: Ask User if he is sure here maybe -> Errorcheck the current Version vs. File Version
		BuildLevel(levelData);
	}
	//Till here

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
			sentrys[i] = new Sentry(sentryObj[i].GetComponent<EnemySight>());
		}

		return new LevelData(levelName, walls, sentrys);
	}

	private static void BuildLevel(LevelData level)
	{
		//Walls
		foreach (Wall wall in level.walls)
		{
			//TODO:

			//If case variant exists
			//Instantiate file.wall[variant]

			//That Template file would be similar to last semester Enemy Patrol Path Save thing
			//Multiple ones, easy to switch out
			//Arrays for Variants
			Debug.Log("X:" + wall.position[0] + " Z:" + wall.position[1]);
		}

		//TODO Fo Sentrys etc. as well
	}
}