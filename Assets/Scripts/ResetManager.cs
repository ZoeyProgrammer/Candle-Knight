using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour
{
	private InputMaster inputMaster = null;

	void Awake()
	{
		inputMaster = new InputMaster();

		inputMaster.Player.Reset.performed += Reset;
		inputMaster.Player.Menu.performed += Menu;
	}

	private void OnEnable()
	{
		inputMaster.Enable();
	}

	private void OnDisable()
	{
		inputMaster.Disable();
	}

	private void Reset(InputAction.CallbackContext context)
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
	}

	private void Menu(InputAction.CallbackContext context)
	{
		SceneManager.LoadSceneAsync("Title");
	}
}
