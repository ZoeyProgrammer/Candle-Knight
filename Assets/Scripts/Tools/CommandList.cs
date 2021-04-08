using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Name: Zoey Bastian
Functionality: Storing all Debug Commands
Requirements: none
Notes: If you add a new command, also add it into the commandList at the bottom!
*/

public class CommandList : MonoBehaviour
{
    private static DebugCommand help = new DebugCommand("help", "Lists all Commands", "help", () =>
    {
        Debug.Log("List of all Commands:");
        foreach (DebugCommandBase cmd in commandList)
        {
            Debug.Log(cmd.CommandFormat + " - " + cmd.CommandDescription);
        }
    });

    private static DebugCommand test = new DebugCommand("test", "Just a Test", "test", () =>
        {
            Debug.Log("Test Command successfully executed");
        });

    private static DebugCommand<int> testInt = new DebugCommand<int>("testInt", "Tests Int values", "testInt <int>", (value) =>
        {
            Debug.Log("Your test value is: " + value);
        });

    private static DebugCommand quit = new DebugCommand("quit", "Quit the game", "quit", () =>
    {
        Application.Quit();
    });

    private static DebugCommand reset = new DebugCommand("reset", "Resets the current Scene", "reset", () =>
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    });

    private static DebugCommand backToMenu = new DebugCommand("backToMenu", "Returns to the Main Menu.", "backToMenu", () =>
    {
        SceneManager.LoadSceneAsync("Title");
    });

    //Fill this up, when you make a new command
    public static List<DebugCommandBase> commandList = new List<DebugCommandBase>
        {
            help,
            test,
            testInt,
            quit,
            reset,
            backToMenu,
        };
}
