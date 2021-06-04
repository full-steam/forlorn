using System.Collections.Generic;
using UnityEngine;
using Yarn;

/// <summary>
/// Custom YarnSpinner Variable Storage for integration with FlagManager.
/// </summary>
public class VariableStorage : Yarn.Unity.VariableStorageBehaviour
{
    private Dictionary<string, Value> variables = new Dictionary<string, Value>();

    /// <summary>
    /// Return a value, given a variable name.
    /// </summary>
    /// <param name="variableName">Variable name of the requested value.</param>
    /// <returns>
    /// If the variable name starts with "$", return a locally stored variable of type Value (Yarn's Value).
    /// Else, returns the value of the flag named variableName.
    /// </returns>
    public override Value GetValue(string variableName)
    {
        if (variableName.StartsWith("$VAR")) return variables[variableName];
        return new Value(GameManager.Instance.Blackboard.FlagManager.GetFlag(variableName.Substring(1)));
    }

    /// <summary>
    /// Return to the original state.
    /// </summary>
    public override void ResetToDefaults()
    {
        GameManager.Instance.Blackboard.FlagManager.ResetFlags();
    }

    /// <summary>
    /// Store a value into a variable.
    /// If it's a locally-stored variable and the variable by the name variableName does not exist, it will be created.
    /// </summary>
    /// <param name="variableName">Variable name of the given value.</param>
    /// <param name="value">Value to be stored.</param>
    public override void SetValue(string variableName, Value value)
    {
        if (variableName.StartsWith("$VAR")) SetVariable(variableName, value);
        GameManager.Instance.Blackboard.FlagManager.SetFlag(variableName.Substring(1), value.AsBool);
    }

    /// <summary>
    /// Sets the value of a locally stored variable.
    /// If the variable by the name variableName does not exist, it will be created.
    /// </summary>
    /// <param name="variableName">Variable name of the given value. Must start with "$".</param>
    /// <param name="value">Value to be stored.</param>
    public void SetVariable(string variableName, Value value)
    {
        if (!variableName.StartsWith("$VAR")) Debug.LogError("Locally-stored variables must start with a '$VAR'!");

        if (variables.ContainsKey(variableName)) variables[variableName] = new Value(value);
        else variables.Add(variableName, new Value(value));
    }
}
