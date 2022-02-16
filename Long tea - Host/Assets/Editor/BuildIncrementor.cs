using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildIncrementor : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        PlayerSettings.bundleVersion = TryParseVersion(PlayerSettings.bundleVersion, 0.001f);
    }

    public string TryParseVersion(string currentVersion, float increment)
    {
        float.TryParse(currentVersion, out float currentVersionNumber);
        float newVersion = currentVersionNumber + increment;
        Debug.Log($"Building version {newVersion}");
        return newVersion.ToString();
    }
}