using UnityEngine;

public class FlagChecker : MonoBehaviour
{
    public enum ExecType
    {
        DESTROY,
        DISABLE
    }

    public string flag;
    public ExecType execType;

    private void Awake()
    {
        if (GameManager.Instance.Blackboard.FlagManager.GetFlag(flag)) Execute();
    }

    private void Execute()
    {
        switch (execType)
        {
            case ExecType.DESTROY:
                Destroy(gameObject);
                break;
            case ExecType.DISABLE:
                gameObject.SetActive(false);
                break;
        }
    }
}
