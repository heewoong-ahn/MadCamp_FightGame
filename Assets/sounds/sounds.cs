using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    public AudioSource punched;
    public AudioSource kicked;
    public AudioSource guard;
    public AudioSource uppered;
   

    public void playPunched()
    {
        punched.Play();
    }
    public void playKicked()
    {
        kicked.Play();
    }

    public void guarded()
    {
        guard.Play();
    }

    public void playUppered()
    {
        uppered.Play();
    }
}
