using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //referencia al controlador de audio y sonidos SFX
    private AudioSource aud;
    public AudioClip correct;
    public AudioClip incorrect;

    public void Start()
    {
        aud = GetComponent<AudioSource>();//Asignamos referencia 
    }
    public void soundStart(bool _correct)//Ejecutamos SFX de error o correcto segun el bool
    {
        aud.Stop();
        if (_correct)
            aud.clip = correct;
        else
            aud.clip = incorrect;
        if(GameManager.Instance.musicOn)
            aud.Play();
    }
}
