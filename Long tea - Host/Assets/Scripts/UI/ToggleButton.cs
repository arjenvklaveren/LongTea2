using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleButton : MonoBehaviour
{
     private bool isToggled = false;
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private float fadeInDuration = 0.3f;
    [SerializeField] private float fadeOutDuration = 0.3f;

    public void DoToggleAction()
    {
        isToggled = !isToggled;
        if (isToggled)
        {
            canvasGroup.DOFade(1f, fadeInDuration);
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.DOFade(0f, fadeOutDuration);
            canvasGroup.interactable = false;
        }
    }

}
