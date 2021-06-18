using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelEditorManager : MonoBehaviour
{
	[SerializeField] LineRenderer cursorCrosshair = null;
	[SerializeField] LineRenderer selectionCrosshair = null;

	public ObjectTemplate Template { get => this.template; }
	[SerializeField] ObjectTemplate template = null;

	public GameObject CurrentObject { get => this.currentObject; set => this.currentObject = value; }
	private GameObject currentObject = null;

	public int CurrentVariant {set => this.currentVariant = value; }
	private int currentVariant = 0;

	public Quaternion CurrentRotation { get => this.currentRotation; set => this.currentRotation = value; }
	private Quaternion currentRotation = Quaternion.identity;

	public GameObject SelectedObject { get => this.selectedObject; } //Potentially need a different Set function?
	private GameObject selectedObject = null;

	private InputMaster inputMaster = null;
	private GameObject parentObject = null;

	void Awake()
	{
		inputMaster = new InputMaster();

		inputMaster.Editor.Place.performed += OnLeftclick;
		inputMaster.Editor.Remove.performed += OnRightclick;

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
		cursorCrosshair.transform.position = CalcPos();
		if (CheckGrid() == null)
		{
			cursorCrosshair.endColor = Color.green;
			cursorCrosshair.startColor = Color.green;
		}
		else
		{
			cursorCrosshair.endColor = Color.red;
			cursorCrosshair.startColor = Color.red;
		}
	}

	private void OnLeftclick(InputAction.CallbackContext context)
	{
		//Check the Bounds - Hardcoded for now until get around to make a nicer alternative (Maybe in a Constants Data)
		if (0 <= CalcPos().x && CalcPos().x <= 25 &&
			0 <= CalcPos().z && CalcPos().z <= 25)
		{
			if (CheckGrid() == null)
			{
				PlaceObject();
			}
			else
			{
				SelectObject();
			}
		}
	}

	private void PlaceObject()
	{
		GameObject obj = Instantiate(currentObject, CalcPos(), currentRotation, parentObject.transform);
		ObjectMarker marker = obj.GetComponent<ObjectMarker>();
		if (marker != null)
			marker.variant = currentVariant;
		Debug.Log("Object Placed successfully");
	}

	private void SelectObject()
	{
		GameObject obj = CheckGrid();
		selectionCrosshair.enabled = true;
		selectedObject = obj;
		selectionCrosshair.transform.position = obj.transform.position;
	}

	public void DeselectObject()
	{
		selectedObject = null;
		selectionCrosshair.enabled = false;
	}

	private void OnRightclick(InputAction.CallbackContext context)
	{
		DestroyObject();
	}

	private void DestroyObject()
	{
		GameObject obj = CheckGrid();
		if (obj == selectedObject)
		{
			selectedObject = null;
			selectionCrosshair.enabled = false;
		}
		GameObject.Destroy(obj);
	}

	public void RotateObject(int degrees)
	{
		if (selectedObject != null)
		selectedObject.transform.rotation = Quaternion.Euler(0, degrees, 0);
	}

	private GameObject CheckGrid()
	{
		RaycastHit hit;
		Physics.Raycast(CalcPos() + Vector3.down, Vector3.up * 3, out hit, 3f); //Check if Grid is free
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