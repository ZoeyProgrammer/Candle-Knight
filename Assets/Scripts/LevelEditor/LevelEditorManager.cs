using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEditorManager : MonoBehaviour
{
	[SerializeField] GameObject parentObject;
	[SerializeField] GameObject testObject;
	[SerializeField] LineRenderer crosshair;

	private InputMaster inputMaster = null;
	private GameObject currentObject = null;

	void Awake()
	{
		inputMaster = new InputMaster();

		inputMaster.Editor.Place.performed += PlaceObject;
		inputMaster.Editor.Remove.performed += RemoveObject;

		//DEBUG
		currentObject = testObject;
	}

	private void OnEnable()
	{
		inputMaster.Enable();
	}

	private void OnDisable()
	{
		inputMaster.Disable();
	}

	private void Update()
	{ //Potentially only check this whenever an actual grid-change occured?
		crosshair.transform.position = CalcPos();
		if (CheckGrid(currentObject) == null)
		{
			crosshair.endColor = Color.green;
			crosshair.startColor = Color.green;
		}
		else
		{
			crosshair.endColor = Color.red;
			crosshair.startColor = Color.red;
		}
		
	}

	public void SaveLevel() //For the Button
	{
		Savesystem.SaveLevel("test");
	}

	public void LoadLevel() //For the Button
	{
		Savesystem.LoadLevel("test");
	}

	private void PlaceObject(InputAction.CallbackContext context)
	{
		if (CheckGrid(currentObject) == null)
		{
			GameObject obj = Instantiate(currentObject, CalcPos(), Quaternion.identity, parentObject.transform);
			Debug.Log("Object Placed successfully");
		}
	}

	private void RemoveObject(InputAction.CallbackContext context)
	{
		GameObject obj = CheckGrid(currentObject);
		GameObject.Destroy(obj);
	}

	private GameObject CheckGrid(GameObject obj)
	{
		RaycastHit hit;
		Physics.Raycast(CalcPos() + Vector3.down, Vector3.up * 2, out hit, 2f); //Check if Grid is free
		if (hit.collider != null)
			return hit.collider.gameObject;
		else
			return null;
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