using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/*
Name: Zoey Bastian
Functionality: Opening and Closing the Debug Console, as well as Recieving and Executing the Commands.
Requirements: none
Notes: none
*/

public class DebugController : MonoBehaviour
{
    private bool showConsole = false;
    private string input = "";
    private List<string> log = new List<string>();

    InputMaster inputMaster;

    static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
        }
        else if (instance != this.gameObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        inputMaster = new InputMaster();

        inputMaster.Debug.ToggleDebug.started += OnToggleDebug;
        inputMaster.Debug.Tabulate.started += OnTabPress;
        inputMaster.UI.Submit.started += OnEnterPress;
        inputMaster.UI.Navigate.started += OnNavigate;
    }

    void OnEnable()
    {
        inputMaster.Enable();
        Application.logMessageReceived += HandleLog;
    }


    void HandleLog(string logString, string stackTrace, LogType type)
    {
        log.Add(String.Format("[{0}][{1}] {2}", System.DateTime.Now.ToLongTimeString(), type.ToString(), logString));
    }

    public void OnToggleDebug(InputAction.CallbackContext context)
    {
        showConsole = !showConsole;

        if (showConsole && FindObjectOfType<CharacterMovement>() != null)
        {
            FindObjectOfType<CharacterMovement>().isAllowedToMove = false;
            Time.timeScale = 0;
        }
        else if (!showConsole && FindObjectOfType<CharacterMovement>() != null)
        {
            Time.timeScale = 1;
            FindObjectOfType<CharacterMovement>().isAllowedToMove = true;
        }
    }

    public void OnEnterPress(InputAction.CallbackContext context)
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }

    private void HandleInput()
    {
        string[] inputSplit = input.Split(' ');
        string commandWord = inputSplit[0];
        string[] parameters = inputSplit.Skip(1).ToArray();

        for (int i = 0; i < CommandList.commandList.Count; i++)
        {
            DebugCommandBase commandBase = CommandList.commandList[i] as DebugCommandBase;

            if (commandWord == commandBase.CommandID)
            {
                if (CommandList.commandList[i] is DebugCommand command)
                {
                    command.Invoke();
                    return;
                }
                else if (CommandList.commandList[i] is DebugCommand<int> commandInt)
                {
                    int parameter = 0;

                    if (parameters.Length != 1)
                        Debug.LogWarning(string.Format("Could not find Parameter <int>: '{0}'\n", input));
                    else if (!int.TryParse(parameters[0], out parameter))
                        Debug.LogWarning(string.Format("Parameter not of Type <int>: '{0}'\n", parameters[0]));
                    else
                        commandInt.Invoke(parameter);
                    return;
                }
            }
        }
        Debug.LogWarning(string.Format("Could not find Command: '{0}'\n", input));
    }

    private Vector2 scrollPosition = Vector2.zero;
    private bool jumpToEnd = false;
    private int selectedAutocomplete = 0;

    public void OnTabPress(InputAction.CallbackContext context)
    {
        List<DebugCommandBase> commandSuggestions = CommandList.commandList.FindAll(x => x.CommandFormat.StartsWith(input));
        if (commandSuggestions.Count != 0)
        {
            input = commandSuggestions[selectedAutocomplete].CommandID;
            jumpToEnd = true;
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            List<DebugCommandBase> commandSuggestions = CommandList.commandList.FindAll(x => x.CommandFormat.StartsWith(input));
            Vector2 navigate = context.ReadValue<Vector2>();

            if (navigate.y == 1 && selectedAutocomplete > 0)
                selectedAutocomplete -= 1;
            else if (navigate.y == -1 && selectedAutocomplete < commandSuggestions.Count -1)
                selectedAutocomplete += 1;
        }
    }

    private void OnGUI()
    {
        if (!showConsole) { return; }

        float outputHeight = 300;
        float paddingHorizontal = 5;
        float paddingVertical = 5;
        float labelSize = 20;

        //Input Field
        GUI.backgroundColor = Color.black;
        GUI.Box(new Rect(0, outputHeight, Screen.width, 30), "");
        GUI.backgroundColor = Color.clear;
        GUI.skin.textField.wordWrap = false;
        input = GUI.TextField(new Rect(paddingHorizontal, outputHeight + paddingVertical, Screen.width - paddingHorizontal * 2, 20), input);

        TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
        if (jumpToEnd)
        {
            jumpToEnd = false;
            editor.MoveTextEnd();
        }

        //Auto-Completion DropDown Menu
        List<DebugCommandBase> commandSuggestions = CommandList.commandList.FindAll(x => x.CommandFormat.StartsWith(input));
        if (selectedAutocomplete > commandSuggestions.Count - 1)
            selectedAutocomplete = 0;


        if (!string.IsNullOrEmpty(input) && commandSuggestions.Count != 0)
        {
            GUI.contentColor = Color.gray;
            GUI.backgroundColor = Color.black;
            GUI.Box(new Rect(0, outputHeight + 30, 300, 20 * commandSuggestions.Count), "");
            for (int i = 0; i < commandSuggestions.Count; i++)
            {
                if (selectedAutocomplete <= commandSuggestions.Count -1 && selectedAutocomplete == i)
                    GUI.contentColor = Color.green;
                else
                    GUI.contentColor = Color.grey;

                GUI.skin.button.alignment = TextAnchor.MiddleLeft;
                if (GUI.Button(new Rect(paddingHorizontal, outputHeight + 30 + 20 * i, 300, 20), commandSuggestions[i].CommandFormat))
                {
                    input = commandSuggestions[i].CommandID;
                    jumpToEnd = true;
                }
            }
        }

        //Output Field
        GUI.backgroundColor = Color.black;
        GUI.Box(new Rect(0, 0, Screen.width, outputHeight), "");
        Rect viewport = new Rect(0, 0, Screen.width - paddingHorizontal * 2 - 20, labelSize * log.Count);

        GUI.backgroundColor = Color.white;
        scrollPosition = GUI.BeginScrollView(new Rect(paddingHorizontal, 0 + paddingVertical, Screen.width - paddingHorizontal * 2, outputHeight - paddingVertical * 2), scrollPosition, viewport, false, true);

        GUI.skin.label.wordWrap = false;
        for (int i = 0; i < log.Count; i++)
        {

            if (log[i].Contains("Log"))
                GUI.contentColor = Color.white;
            if (log[i].Contains("success")
                || log[i].Contains("Success"))
                GUI.contentColor = Color.green;
            if (log[i].Contains("Warning"))
                GUI.contentColor = Color.yellow;
            if (log[i].Contains("Assert")
                || log[i].Contains("Error")
                || log[i].Contains("Exception"))
                GUI.contentColor = Color.red;

            Rect labelRect = new Rect(0, labelSize * i, viewport.width, labelSize);
            GUI.Label(labelRect, log[i]);
        }

        //Hook to pull the view along
        if (scrollPosition.y + labelSize * (outputHeight / labelSize) > log.Count * labelSize)
            scrollPosition.y = log.Count * labelSize;

        GUI.EndScrollView();
    }
}
