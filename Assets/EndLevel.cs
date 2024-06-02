using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    public Image[] estrellas;
    public Sprite on;
    public Sprite off;

    public void setEnd()//Carga las estrellas correspondiente segun la puntuacion
    {
        for(int b = 0; b < estrellas.Length; b++)
        {
            estrellas[b].sprite = off;
        }
        for(int a = 0; a < GameManager.Instance.livesLevel; a++)
        {
            estrellas[a].sprite = on;
        }
    }
    
}
