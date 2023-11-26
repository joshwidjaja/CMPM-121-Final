using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[,] Board; //A 2d array where each row/column is the same size
    int boardSize = 10;
    void Start()
    {
        Board = new GameObject[boardSize, boardSize]; //Each "cell"'s area/square is from index x to index x+1, and index y to index y+1
        RegenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RegenerateBoard(){
        for(int i = 0; i < boardSize; i++){
            for(int j = 0; j < boardSize; j++){
                Board[i, j] = new GameObject("crop: " + ((10*i) + (j+1)).ToString()); //To do: Only generate crops with a 30% chance
            }
        }
    }
}
