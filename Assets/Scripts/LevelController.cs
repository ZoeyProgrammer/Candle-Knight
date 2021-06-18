using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
	[SerializeField] private string[] levelList = null;
	[SerializeField] private ObjectTemplate levelTemplate = null;
	
	private void Start()
	{
		LoadLevel();
	}

	public void LoadLevel()
	{
		if (GameManger.currentLevel < levelList.Length)
			Savesystem.LoadLevel(levelList[GameManger.currentLevel], levelTemplate);
		else
			SceneManager.LoadSceneAsync("Title");
	}
}
