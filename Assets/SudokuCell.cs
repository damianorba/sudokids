using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SudokuCell : MonoBehaviour
{
    private int correctNumber = 0; // Número correcto para esta celda
    public Sprite[] img;
    public Image imgElement;

    public void Start()
    {
        imgElement = GetComponent<Image>();
    }
    //se ejecuta cuando cargamos un nuevo numero en el sudoku 
    public void CheckAnswer()
    {
        bool temp;
        correctNumber = GameManager.Instance.numSelect;
        if (!GameManager.Instance.hard)
        {
            temp = GameManager.Instance.sud.validateSudoku();
            if (temp)
            {
                imgElement.sprite = img[correctNumber];
                GameManager.Instance.checkEndGame();
                GameManager.Instance.audioManager.soundStart(temp);
            }
            else
            {
                correctNumber = 0;
                imgElement.sprite = img[correctNumber];
                StartCoroutine(error());
                GameManager.Instance.audioManager.soundStart(temp);
            }
        }
        else
        {
            imgElement.sprite = img[correctNumber];
            temp = GameManager.Instance.sud.validateSudoku();
            GameManager.Instance.audioManager.soundStart(temp);
            GameManager.Instance.checkEndGame();
        }
        //Debug.Log(GameManager.Instance.sud.ValidateSudoku());
    }
    public int getNum()
    {
        return correctNumber;
    }
    //cambiamos el valor de la celda
    public void setNum(int num)
    {
        correctNumber = num;
        StartCoroutine(updateVisual());
    }
    //Updateamos la imagen de la casilla 
    private IEnumerator updateVisual()
    {
        yield return new WaitForSeconds(0.1f);
        imgElement.sprite = img[correctNumber];
    }
    //en caso de no poder cargarse la imagen por estar mal generamos un efecto rojo de error
    private IEnumerator error()
    {
        imgElement.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        imgElement.color = Color.white;
    }
}
