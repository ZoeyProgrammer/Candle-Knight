using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelEditorManager : MonoBehaviour
{
	[SerializeField] LineRenderer crosshair = null;

	public ObjectTemplate Template { get => this.template; }
	[SerializeField] ObjectTemplate template = null;

	public GameObject CurrentObject { get => this.currentObject; set => this.currentObject = value; }
	private GameObject currentObject = null;

	private InputMaster inputMaster = null;
	private GameObject parentObject = null;

	void Awake()
	{
		inputMaster = new InputMaster();

		inputMaster.Editor.Place.performed += PlaceObject;
		inputMaster.Editor.Remove.performed += RemoveObject;

		parentObject = GameObject.FindGameObjectWithTag("Parent");
		
		//DEBUG
		currentObject = template.Contains()[0][0];
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