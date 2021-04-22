using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEditorManager : MonoBehaviour
{
	[SerializeField] GameObject parentObject;
	[SerializeField] GameObject testObject;

	private InputMaster inputMaster = null;

	void Awake()
	{
		inputMaster = new InputMaster();

		inputMaster.Editor.Place.performed += PlaceObject;
	}

	private void OnEnable()
	{
		inputMaster.Enable();
	}

	private void OnDisable()
	{
		inputMaster.Disable();
	}

	private void PlaceObject(InputAction.CallbackContext context)
	{
		Instantiate(testObject, CalcPos(), Quaternion.identity, parentObject.transform);
	}

	// For Calculating the current Mouse Position on the Grid
	private Vector3 CalcPos()
	{
		Vector2 mousePos = inputMaster.UI.Point.ReadValue<Vector2>();
		Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
		Vector3 pos = new Vector3(Mathf.FloorToInt(objectPos.x) + 0.5f, 0, Mathf.FloorToInt(objectPos.z) + 0.5f);

		return pos;
	}
}
