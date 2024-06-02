using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnter : MonoBehaviour
{
    public int levelId; //Id unico del boton para identificarlo
    public Image[] stars;
    public Image level;
    public Sprite on;
    public Sprite off;
    public Sprite sudOn;
    public Sprite sudOff;
    void Start()
    {
        UpdateIcons();
    }

    public void UpdateIcons() //Updateamos visualmente el boton
    {
        if (!GameManager.Instance.hard)//Segun si esta en 4x4 o 6x6 ponemos una imagen u otra segun si podemos jugar el nivel
        {
            if (GameManager.Instance.win[levelId])
                level.sprite = sudOn;
            else
                level.sprite = sudOff;
        }
        else
        {
            if (GameManager.Instance.winHard[levelId])
                level.sprite = sudOn;
            else
                level.sprite = sudOff;
        }
        for(int a = 0; a < stars.Length; a++)
        {
            stars[a].sprite = off;
        }
        if (GameManager.Instance.hard)
        {
            for (int a = 0; a < GameManager.Instance.levelsScoreHard[levelId]; a++)//Cargamos las imagenes de estrellas prendidas segun los puntos del nivel
            {
                stars[a].sprite = on;
            }
        }
        else
        {
            for (int a = 0; a < GameManager.Instance.levelsScore[levelId]; a++)
            {
                stars[a].sprite = on;
            }
        }
    }
    public void StartLevel()//Iniciamos el nivel siempre y cuando este habilitado
    {
        if (GameManager.Instance.hard)
        {
            if (GameManager.Instance.winHard[levelId])
            {
                GameManager.Instance.level = levelId;
                GameManager.Instance.newLevel();
            }
        }
        else
        {
            if (GameManager.Instance.win[levelId])
            {
                GameManager.Instance.level = levelId;
                GameManager.Instance.newLevel();
            }
        }
    }
}
