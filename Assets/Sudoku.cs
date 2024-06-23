using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct niveles
{
    public int[] valor;
}

public class Sudoku : MonoBehaviour
{
    public niveles[] niveles4; // lista de niveles 4x4
    public niveles[] niveles6; // lista de niveles 6x6
    public int size; // tama�o de la grilla
    public SudokuCell prefad;
    public Transform gridParent; // Parent object donde se colocar�n los botones
    public SudokuCell[,] celdas; // grilla
    private GridLayoutGroup grid;

    private void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
    }

    // creamos un nuevo sudoku
    public void newSudoku()
    {
        Debug.Log(size);
        grid.cellSize = new Vector2(Convert.ToSingle((Screen.width * 0.9)) / size, Convert.ToSingle((Screen.width * 0.8)) / size);
        // Eliminar los botones existentes (si los hay)
        ClearGrid();
        int contador = 0;
        celdas = new SudokuCell[size, size];
        // Generar la grilla de 4x4 o 6x6 seg�n el tama�o
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                GameObject button = Instantiate(prefad.gameObject, gridParent);
                button.name = "Button_" + row + "_" + col;
                SudokuCell cell = button.GetComponent<SudokuCell>();
                celdas[row, col] = cell;

                // Configurar la celda directamente sin depender de propiedades espec�ficas en SudokuCell
                if (size == 4)
                {
                    cell.setNum(niveles4[contador].valor[row * size + col]);
                }
                else
                {
                    cell.setNum(niveles6[contador].valor[row * size + col]);
                }
            }
        }
    }

    // eliminamos el sudoku actual
    private void ClearGrid()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
    }

    // Comprobamos si el sudoku es v�lido
    public bool validateSudoku()
    {
        for (int i = 0; i < size; i++)
        {
            if (!IsValidGroup(celdas, i, true) || !IsValidGroup(celdas, i, false))
            {
                GameManager.Instance.livesLevel--;
                return false;
            }
        }

        if (!GameManager.Instance.hard)
        {
            int subgridSize = Mathf.RoundToInt(Mathf.Sqrt(size));
            for (int row = 0; row < size; row += subgridSize)
            {
                for (int col = 0; col < size; col += subgridSize)
                {
                    if (!IsValidSquare(celdas, row, col, subgridSize))
                    {
                        GameManager.Instance.livesLevel--;
                        return false;
                    }
                }
            }
        }

        return true; // La grilla cumple con las reglas del Sudoku
    }

    // M�todo para verificar si una fila o columna es v�lida
    public bool IsValidGroup(SudokuCell[,] grid, int index, bool isRow)
    {
        bool[] isNumberUsed = new bool[size + 1]; // Usamos un arreglo de tama�o size + 1 para los n�meros del 1 al size

        for (int i = 0; i < size; i++)
        {
            int num = isRow ? grid[index, i].getNum() : grid[i, index].getNum();
            if (num != 0)
            {
                if (isNumberUsed[num])
                {
                    return false; // N�mero repetido en la fila o columna
                }
                isNumberUsed[num] = true;
            }
        }

        return true; // La fila o columna es v�lida
    }

    // M�todo para verificar si un cuadrante es v�lido
    public bool IsValidSquare(SudokuCell[,] grid, int startRow, int startCol, int subgridSize)
    {
        bool[] isNumberUsed = new bool[size + 1]; // Usamos un arreglo de tama�o size + 1 para los n�meros del 1 al size

        for (int row = startRow; row < startRow + subgridSize; row++)
        {
            for (int col = startCol; col < startCol + subgridSize; col++)
            {
                int num = grid[row, col].getNum();
                if (num != 0)
                {
                    if (isNumberUsed[num])
                    {
                        return false; // N�mero repetido en el cuadrante
                    }
                    isNumberUsed[num] = true;
                }
            }
        }

        return true; // El cuadrante es v�lido
    }

    // Verificamos si todas las celdas est�n completas
    public bool AllCellsFilled()
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (celdas[row, col].getNum() == 0)
                {
                    return false; // Se encontr� al menos una casilla vac�a
                }
            }
        }

        return true; // Todas las casillas est�n llenas
    }
}

