using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField] string gameStartScene = null;

	public void StartGame()
	{
		SceneManager.LoadSceneAsync(gameStartScene);
	}

	public void ResumeGame()
	{
		//Load newest Level from Game Manager?
	}

	public void LevelEditor()
	{
		SceneManager.LoadSceneAsync("LevelEditor");
	}

	public void Settings()
	{
		//Open the settings menu
	}

	public void Credits()
	{
		//Open the Credits menu
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
