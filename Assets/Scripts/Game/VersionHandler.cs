using TMPro;
using UnityEngine;

public class VersionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "version " + Application.version;
    }
}
