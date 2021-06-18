using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		Savesystem.LoadLevel(levelList[GameManger.currentLevel], levelTemplate);
	}
}
