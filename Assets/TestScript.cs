using UnityEngine;

using StankUtilities.Runtime.Data;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        TestSettings testSettings = new TestSettings(Application.dataPath + "/test_settings.json");

        Debug.Log(testSettings.Mods[0].ModName);
        Debug.Log(testSettings.AudioLevel);
        Debug.Log(testSettings.CanWalk);
        Debug.Log(testSettings.MoveSpeed);
    }
}
