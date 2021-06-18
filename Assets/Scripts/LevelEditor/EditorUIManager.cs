using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorUIManager : MonoBehaviour
{
    [SerializeField] private Dropdown objectSelection = null;
    [SerializeField] private SliderInput variantSelection, rotationSelection = null;
    [SerializeField] private InputField inputX, inputZ, saveInput, loadInput = null;
    [SerializeField] private Transform contextMenus = null;
    [SerializeField] private GameObject sentryMenu, doorMenu, stairMenu, buttonMenu, editorContext, saveContext, loadContext = null;

    private ObjectTemplate template = null;
    private LevelEditorManager manage = null;
    private int currentObject = 0;
    private int currentVariant = 0;
    private int currentRotation = 0;
    private string currentLevelName = null;
    private GameObject selectedObject = null;

    private GameObject currentContextMenu = null;

	private void Awake()
	{
        manage = this.GetComponent<LevelEditorManager>();
        template = manage.Template;

        //Fill the Dropdown with Template Data
        objectSelection.options.Clear();
        foreach (GameObject[] obj in template.Contains())
		{
            objectSelection.options.Add(new Dropdown.OptionData(obj[0].name)); 
        }

        variantSelection.UpdateNull();
        variantSelection.MaxValue = template.Contains()[currentObject].Length - 1;
    }

    public void MenuChange(int menu)
	{
		switch (menu)
		{
            case 0: //Save Menu
                saveContext.SetActive(true);
                loadContext.SetActive(false);
                editorContext.SetActive(false);
                break;
            case 1: //Load Menu
                saveContext.SetActive(false);
                loadContext.SetActive(true);
                editorContext.SetActive(false);
                break;
            case 2: //Editor Menu
                saveContext.SetActive(false);
                loadContext.SetActive(false);
                editorContext.SetActive(true);
                break;
            case 3: //Preview Menu
                break;
            case 4: //Play Menu
                break;
            default:
				break;
		}
	}

    public void SaveLevel() //For the Button
    {
        Savesystem.SaveLevel(currentLevelName);
    }

    public void LoadLevel() //For the Button
    {
        Savesystem.LoadLevel(currentLevelName, template);
        //Maybe actually make this a Selection Menu which first scans for all .lvl data it can find?
    }

    public void ClearLevel() //For the Button
	{
        manage.DeselectObject();
        Savesystem.ClearLevel();
        //TODO: Maybe Question the Dumb Dumb User because he is Dumb? This is a good TODO text, becauase its bad if anyone sees this >.<
	}

    //Select Object Dropdown and give that data to LevelEditorManager
    public void SelectObject(int option)
	{
        if (template.Contains()[option] != null)
        {
            currentObject = option;
            currentVariant = 0;
            variantSelection.UpdateNull();
            variantSelection.MaxValue = template.Contains()[option].Length - 1;
            PushCurrentObject();
        }
	}

    public void SelectVariant(int option)
	{
        if (option >= template.Contains()[currentObject].Length)
		{
            //This Variant is too high!
		}
        else
		{
            currentVariant = option;
            PushCurrentObject();
		}
	}

    public void SelectRotation(int option)
    {
        currentRotation = IntToRot(option);
        PushCurrentRotation();
    }

    public void ChangeRotation(int option)
	{
        manage.RotateObject(IntToRot(option));
    }

    public void SetLevelName(string lvlName)
	{
        currentLevelName = lvlName;
        loadInput.text = lvlName;
        saveInput.text = lvlName;
	}

    private int IntToRot(int option) //option * 90
	{
        switch (option)
        {
            case 0:
                return 0;
            case 1:
                return 90;
            case 2:
                return 180;
            case 3:
                return 270;
            default:
                return 0;
        }
    }

    private int RotToInt(int degree) //option / 90
    {
        switch (degree)
        {
            case 0:
                return 0;
            case 90:
                return 1;
            case 180:
                return 2;
            case 270:
                return 3;
            default:
                return 0;
        }
    }

    private void PushCurrentObject()
	{
        manage.CurrentObject = template.Contains()[currentObject][currentVariant];
        manage.CurrentVariant = currentVariant;
	}

    private void PushCurrentRotation()
	{
        manage.CurrentRotation = Quaternion.Euler(0, currentRotation, 0);
    }

	private void Update()
	{
        //Only do the update if the selected Object has changed and its not null

		if (manage.SelectedObject != null && manage.SelectedObject != selectedObject)
		{
            UpdateInspector();
            selectedObject = manage.SelectedObject;
		}
	}

	private void UpdateInspector()
	{
        inputX.text = manage.SelectedObject.transform.position.x.ToString();
        inputZ.text = manage.SelectedObject.transform.position.z.ToString();

        rotationSelection.Value = RotToInt((int)manage.SelectedObject.transform.rotation.eulerAngles.y);

        /////////////////////////////////////////////////////
        ///ONE OF THE PLACES I NEED TO ADD NEW OBJECTS TO ///
        /////////////////////////////////////////////////////

        Destroy(currentContextMenu);
        currentContextMenu = null;

        //Change the Inspector depending on what kind of Selected Object to show Object Specific Data
        if (manage.SelectedObject.GetComponent<Button>() != null)
        {
            currentContextMenu = Instantiate(buttonMenu, contextMenus);
        }

        if (manage.SelectedObject.GetComponent<LevelChange>() != null)
        {
            currentContextMenu = Instantiate(stairMenu, contextMenus);
        }

        if (manage.SelectedObject.GetComponent<Door>() != null)
        {
            currentContextMenu = Instantiate(doorMenu, contextMenus);
        }

        if (manage.SelectedObject.GetComponent<EnemySight>() != null)
		{
            currentContextMenu = Instantiate(sentryMenu, contextMenus);
		}
    }


}
