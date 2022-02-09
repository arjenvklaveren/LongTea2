using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class ShowCurrentBuildNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buildLabel;

    private void Awake()
    {
            buildLabel.SetText($"Build: {Application.version}");
    }
}
