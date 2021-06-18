using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField] string gameStartScene = null;

	[SerializeField] private GameObject mainMenu, settingsMenu, creditsMenu, controlsMenu = null;

	public void StartGame()
	{
		SceneManager.LoadSceneAsync(gameStartScene);
	}

	public void ResumeGame()
	{
		//Load newest Level from Game Manager? Have list in gameManager with all the levels in array and a int which tells the newst level
	}

	public void LevelEditor()
	{
		SceneManager.LoadSceneAsync("LevelEditor");
	}

	public void OpenMenu(int menu)
	{
		switch (menu)
		{
			case 0: //Main Menu
				mainMenu.SetActive(true);
				settingsMenu.SetActive(false);
				creditsMenu.SetActive(false);
				controlsMenu.SetActive(false);
				break;
			case 1: //Settings Menu
				mainMenu.SetActive(false);
				settingsMenu.SetActive(true);
				creditsMenu.SetActive(false);
				controlsMenu.SetActive(false);
				break;
			case 2: //Credits Menu
				mainMenu.SetActive(false);
				settingsMenu.SetActive(false);
				creditsMenu.SetActive(true);
				controlsMenu.SetActive(false);
				break;
			case 3: //Controls Menu
				mainMenu.SetActive(false);
				settingsMenu.SetActive(false);
				creditsMenu.SetActive(false);
				controlsMenu.SetActive(true);
				break;
			default:
				break;
		}
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void BrightnessSlider(int value)
	{
		//do Brightness
	}

	public void ContrastSlider(int value)
	{
		//Do Contrast
	}

	public void GammaSlider(int value)
	{
		//Do Gamma
	}

	public void VolumeSlider(int value)
	{
		//Do Volume
	}
}
