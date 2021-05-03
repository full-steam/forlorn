using Yarn;

/// <summary>
/// Custom YarnSpinner Variable Storage for integration with FlagManager.
/// </summary>
public class VariableStorage : Yarn.Unity.VariableStorageBehaviour
{
    /// <summary>
    /// Return a value, given a variable name.
    /// </summary>
    /// <param name="variableName">Variable name of the requested value.</param>
    /// <returns></returns>
    public override Value GetValue(string variableName)
    {
        return new Value(GameManager.Instance.Blackboard.FlagManager.GetFlag(variableName));
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
    /// </summary>
    /// <param name="variableName">Variable name of the given value.</param>
    /// <param name="value">Value to be stored.</param>
    public override void SetValue(string variableName, Value value)
    {
        GameManager.Instance.Blackboard.FlagManager.SetFlag(variableName, value.AsBool);
    }
}
