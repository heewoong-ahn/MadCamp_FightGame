using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource backgroundMusic; // 배경음악 AudioSource
    public AudioSource winner; // 승리 음악 AudioSource
    public AudioSource startBell;
    public AudioSource win;
    public AudioSource ready;
    public AudioSource three;
    public AudioSource two;
    public AudioSource one;

    void Start()
    {
        // 배경음악 시작
        backgroundMusic.Play();
        Invoke("playStartBell", 4f);
        Invoke("playReady", 0.2f);
        Invoke("playThree", 1.1f);
        Invoke("playTwo", 2.1f);
        Invoke("playOne", 3.1f);
    }

    public void PlayWinnerMusic()
    {
        // 배경음악 정지
        backgroundMusic.Stop();

        // 승리 음악 재생
        win.Play();
        winner.Play();
       
    }

    void playStartBell()
    {
        startBell.Play();
    }

    void playReady()
    {
        ready.Play();
    }
    void playThree()
    {
        three.Play();
    }
    void playTwo()
    {
        two.Play();
    }
    void playOne()
    {
        one.Play();
    }
}