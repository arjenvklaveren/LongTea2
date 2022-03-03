using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class GraphicsSettings : MonoBehaviour
{
    [SerializeField] private Volume ppVolume;
    [SerializeField] private Toggle ppfxToggle;

    private void Start()
    {
        if (ppVolume == null) ppVolume = FindObjectOfType<Volume>();

        bool postProcessingToggled = PlayerPrefs.GetInt("ppfxEnabled", 1) == 1;

        TogglePostProcessing(postProcessingToggled);
    }

    public void UpdateQualitySlider()
    {
        if (ppfxToggle) TogglePostProcessing(ppfxToggle.isOn);
    }

    public void TogglePostProcessing(bool ppfxEnabled)
    {
        if (ppVolume) ppVolume.enabled = ppfxEnabled;
        QualitySettings.SetQualityLevel(ppfxEnabled ? 2 : 0, true);
        PlayerPrefs.SetInt("ppfxEnabled", ppfxEnabled ? 1 : 0);
        if (ppfxToggle != null) ppfxToggle.isOn = ppfxEnabled;
    }
}
