using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorUIManager : MonoBehaviour
{
    [SerializeField] private Dropdown objectSelection = null;
    [SerializeField] private SliderInput variantSelection, rotationSelection = null;
    [SerializeField] private InputField inputX, inputZ = null;
    
    private ObjectTemplate template = null;
    private LevelEditorManager manage = null;
    private int currentObject = 0;
    private int currentVariant = 0;
    private int currentRotation = 0;

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

    public void SaveLevel() //For the Button
    {
        Savesystem.SaveLevel("test");
        //TODO: Actually make this take a String too, and save it under that name
    }

    public void LoadLevel() //For the Button
    {
        Savesystem.LoadLevel("test", template);
        //TODO: Actually make this take a String too, and search
        //Maybe actually make this a Selection Menu which first scans for all .lvl data it can find?
    }

    public void ClearLevel() //For the Button
	{
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

    private int IntToRot(int option)
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

    private int RotToInt(int degree)
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
	}

    private void PushCurrentRotation()
	{
        manage.CurrentRotation = Quaternion.Euler(0, currentRotation, 0);
    }

	private void Update()
	{
        //Only do the update if the selected Object has changed and its not null

		if (manage.SelectedObject != null)
		{
            UpdateInspector();
		}
	}

	private void UpdateInspector()
	{
        inputX.text = manage.SelectedObject.transform.position.x.ToString();
        inputZ.text = manage.SelectedObject.transform.position.z.ToString();

        rotationSelection.Value = RotToInt((int)manage.SelectedObject.transform.rotation.eulerAngles.y);
    }


}