using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Zoey Bastian
Functionality: Defining the different Syntax for Debug Commands
Requirements: none
Notes: none
*/

public class DebugCommandBase
{
    private string commandID = "";
    public string CommandID { get => this.commandID; }

    private string commandDescription = "";
    public string CommandDescription { get => this.commandDescription; }

    private string commandFormat = "";
    public string CommandFormat { get => this.commandFormat; }

    public DebugCommandBase(string id, string description, string format)
    {
        commandID = id;
        commandDescription = description;
        commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action command = null;

    public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}

public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> command = null;

    public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T1 parameter)
    {
        command.Invoke(parameter);
    }
}