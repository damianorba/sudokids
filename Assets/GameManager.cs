using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;//singleton para manejar variable static
    public int numSelect = 2;//Numero del sudoku actual seleccionado
    public Sudoku sud;//Referencia al controlador de la logica del sudoku
    public int level = 0;//nivel actual en juego
    public int livesLevel = 3;//Vidas dentro del juego
    public bool hard = false;//dificultad
    public EndLevel end; //referencia al controlador de final de partida (UI)
    public GameObject UIEndLevel;
    public GameObject HardUI;

    //Variables para manejo de puntos y niveles habilitados en cada dificultad
    public int[] levelsScore;
    public int[] levelsScoreHard;
    public bool[] win;
    public bool[] winHard;

    public LevelEnter[] icons;//lista de botones 
    public AudioManager audioManager;//Referencia al controlador de sonido SFX

    //escenas en canvas para pasar de game a mapa
    public GameObject CanvasGame;
    public GameObject CanvasMap;

    //Variables para manejar el sonido
    public bool musicOn = true;
    public Image soundUI;
    public Sprite soundOn;
    public Sprite SoundOff;
    public AudioSource musicSound;
    internal static object instance;

    //instanciamos el singleton
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
        LoadData();
        win[0] = true;
        winHard[0] = true;
    }
    //Determinamos como se ven los canvas de inicio
    private void Start()
    {
        UIEndLevel.SetActive(false);
        CanvasMap.SetActive(true);
        CanvasGame.SetActive(false);
    }
    //cambiamos el numero que vamos a poner en el sudoku
    public void setNumSelect(int num)
    {
        numSelect = num;
    }
    //Lo ejecutamos al terminar el nivel, reseteamos las variables y guardamos la informacion
    public void endLevel()
    {
        UIEndLevel.SetActive(true);
        UIEndLevel.SetActive(true);
        if (livesLevel < 0)
            livesLevel = 1;
        if (hard)
        {
            if (levelsScoreHard[level] < livesLevel)
                levelsScoreHard[level] = livesLevel;
            winHard[level + 1] = true;
        }
        else
        {
            if (levelsScore[level] < livesLevel)
                levelsScore[level] = livesLevel;
            win[level + 1] = true;
        }
        end.setEnd();
        
        SaveData();
    }
    //Comprobamos si se completo el nivel en facil
    public void checkEndGame()
    {
        if (!hard)
        {
            if (sud.AllCellsFilled())
                endLevel();
        }
        else
        {
            if (sud.AllCellsFilled())
                endLevel();
        }

    }
    //Generamos un nuevo nivel, determinamos que UI usar y cuanto por cuanto es el tablero
    public void newLevel()
    {
        CanvasMap.SetActive(false);
        CanvasGame.SetActive(true);
        if (hard)
        {
            sud.size = 6;
            HardUI.SetActive(true);
        }
        else
        {
            sud.size = 4;
            HardUI.SetActive(false);
        }
        sud.newSudoku();
    }
    //Recargamos las visuales de los botones del mapa para que correspondan con los cambios visuales
    public void UpdateUI()
    {
        for(int a = 0; a < icons.Length; a++)
        {
            icons[a].UpdateIcons();
        }
    }
    //Activamos el mapa y updateamos la data (UI)
    public void goMenuMap()
    {
        UIEndLevel.SetActive(false);
        CanvasMap.SetActive(true);
        CanvasGame.SetActive(false);
        UpdateUI();
    }
    //Guardamos las variables de los scores y los niveles habilitados
    public void SaveData()
    {
        // Guardar niveles de puntuación
        for (int i = 0; i < levelsScore.Length; i++)
        {
            PlayerPrefs.SetInt("LevelScore" + i, levelsScore[i]);
        }

        for (int i = 0; i < levelsScoreHard.Length; i++)
        {
            PlayerPrefs.SetInt("LevelScoreHard" + i, levelsScoreHard[i]);
        }

        // Guardar estados de victoria
        for (int i = 0; i < win.Length; i++)
        {
            PlayerPrefs.SetInt("Win" + i, win[i] ? 1 : 0);
        }

        // Guardar estados de victoria en modo difícil
        for (int i = 0; i < winHard.Length; i++)
        {
            PlayerPrefs.SetInt("WinHard" + i, winHard[i] ? 1 : 0);
        }

        // Guardar los cambios
        PlayerPrefs.Save();
    }
    //Cargamos la informacion de los niveles habilitados y las puntuaciones guardadas
    public void LoadData()
    {
        // Cargar niveles de puntuación
        for (int i = 0; i < levelsScore.Length; i++)
        {
            levelsScore[i] = PlayerPrefs.GetInt("LevelScore" + i);
        }

        for (int i = 0; i < levelsScoreHard.Length; i++)
        {
            levelsScoreHard[i] = PlayerPrefs.GetInt("LevelScoreHard" + i);
        }

        // Cargar estados de victoria
        for (int i = 0; i < win.Length; i++)
        {
            win[i] = PlayerPrefs.GetInt("Win" + i) == 1;
        }

        // Cargar estados de victoria en modo difícil
        for (int i = 0; i < winHard.Length; i++)
        {
            winHard[i] = PlayerPrefs.GetInt("WinHard" + i) == 1;
        }
    }
    //cambiamos la dificultad
    public void setDificultad(bool _hard)
    {
        hard = _hard;
    }
    //habilitamos o desabilitamos el sonido
    public void setSound()
    {
        musicOn = !musicOn;
        if (musicOn)
        {
            soundUI.sprite = soundOn;
            musicSound.Play();
        }
        else
        {
            soundUI.sprite = SoundOff;
            musicSound.Pause();
        }
    }

    public void Exit() { 
    
        Application.Quit();

    }
}
