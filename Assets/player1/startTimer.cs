using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class startTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeRemaining = 3.0f;
    private bool timerStarted = false; 

    void Start()
    {
        timerText.text = "READY";
        Invoke("StartTimer", 1f);
    }

    void StartTimer()
    {
        timerText.text = "3";
        timerStarted = true;
    }

    void Update()
    {
        if (timerStarted && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else if (timeRemaining <= 0)
        {
            timerText.text = "Fight!";
            Invoke("DestroyTimer", 1f);
        }
    }

    void DestroyTimer()
    {
        Destroy(gameObject);
    }

    void UpdateTimerDisplay()
    {
        if(timeRemaining>0)
        {
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }
    }
}