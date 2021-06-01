using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StairInspector : MonoBehaviour
{
	[SerializeField] private InputField nextLevelInput;

	private LevelEditorManager manage = null;
	private LevelChange currentSelected = null;

	private void Awake()
	{
		manage = GameObject.FindObjectOfType<LevelEditorManager>();

		LevelChange stair = manage.SelectedObject.GetComponent<LevelChange>();
		if (stair != null)
		{
			currentSelected = stair;
			nextLevelInput.text = stair.nextScene;
		}
		else
			Debug.LogWarning("No Button Found");
	}

	public void NextLevelInput(string value)
	{
		currentSelected.nextScene = value;
	}

}
