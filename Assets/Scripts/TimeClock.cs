using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TimeClock : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeRemaining = 90; // 타이머 시작 시간 (초)
    private bool timerStarted = false;
    player1Controller player1;
    enemyPlayer player2;
    private bool gameEnd;


    void Start()
    {
        gameEnd = false;
        player1 = GameObject.FindObjectOfType<player1Controller>();
        player2 = GameObject.FindObjectOfType<enemyPlayer>();
        // 3초 후에 타이머 시작
        Invoke("StartTimer", 4.5f);
    }

    void StartTimer()
    {
        timerStarted = true;
    }

    void Update()   
    {
        if (timerStarted && timeRemaining > 0 && gameEnd == false)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        if(timeRemaining <= 0 && gameEnd == false)
        {
            EndOfTime();
        }
    }
    void EndOfTime()
    {
        Debug.Log(gameEnd);
        if(player1.curHealth >= player2.curHealth)
        {
            player1.Win0();
            player2.Die0();
        }
        else
        {
            player1.Die0();
            player2.Win0();
        }  
    }

    void UpdateTimerDisplay()
    {
        // 타이머 값을 정수로 변환하여 표시
        timerText.text = Mathf.RoundToInt(timeRemaining).ToString();
    }

    public void EndOfGame()
    {
        gameEnd = true;
    }
}