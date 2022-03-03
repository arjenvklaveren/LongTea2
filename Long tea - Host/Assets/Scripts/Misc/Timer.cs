using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class Timer : NetworkBehaviour
{
    [SerializeField] private float startingTimeInSeconds = 61;
    [SerializeField] private string prefix = "Time left: ";
    [SerializeField] private TMPro.TextMeshProUGUI timeLabel = null;
    [SerializeField] private UnityEvent OnFinishTimer;

    [SyncVar(hook = "UpdateTextLabel")] private float currentTimeLeft = 0;
    private bool timerFinished = false;

    private void Start()
    {
        if (!timeLabel) timeLabel = GetComponent<TMPro.TextMeshProUGUI>();
        currentTimeLeft = startingTimeInSeconds;
        UpdateTextLabel(currentTimeLeft, startingTimeInSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (currentTimeLeft > 0)
            {
                currentTimeLeft -= Time.deltaTime;
            }
            else if (currentTimeLeft <= 0 && !timerFinished)
            {
                timerFinished = true;
                OnFinishTimer.Invoke();
                currentTimeLeft = 0f;
                Debug.Log("Timer finished");
            }
            UpdateTextLabel(currentTimeLeft, currentTimeLeft);
        }
    }

    public void UpdateTextLabel(float oldTime, float newTime)
    {
        float seconds = Mathf.Floor(newTime % 60);
        float minutes = Mathf.Floor(newTime / 60);
        timeLabel.text = prefix + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
