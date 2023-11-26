using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CropManager : MonoBehaviour
{
    // Start is called before the first frame update
    CropCell[,] Board; //A 2d array where each row/column is the same size
    int BOARD_SIZE = 10;
    int counter = 0;
    void Start()
    { 
        Board = new CropCell[BOARD_SIZE, BOARD_SIZE]; //Each "cell"'s area/square is from index x to index x+1, and index y to index y+1
        int UNIT_TILE = BOARD_SIZE / 2;
        int FIRST_HALF_START = 0 - UNIT_TILE;
        int FIRST_HALF_END = 0;

        int SECOND_HALF_START = 1;
        int SECOND_HALF_END = UNIT_TILE + 1;

        for(int x = FIRST_HALF_START; x < FIRST_HALF_END; x++){ //Left half of board ex: -5 to 0(not inclusive)
            for(int y = FIRST_HALF_START; y < FIRST_HALF_END; y++){ 
                Board[x + (UNIT_TILE), y + (UNIT_TILE)] = new CropCell(x, y);
             }
             for(int y = SECOND_HALF_START ; y < SECOND_HALF_END; y++){ 
                Board[x + (UNIT_TILE), y + (UNIT_TILE) - 1] = new CropCell(x, y);
             }
        }
        for(int x = SECOND_HALF_START ; x < SECOND_HALF_END; x++){ //Right half
            for(int y = FIRST_HALF_START; y < FIRST_HALF_END; y++){ 
                Board[x + (UNIT_TILE) - 1, y + (UNIT_TILE)] = new CropCell(x, y);
             }
             for(int y = SECOND_HALF_START ; y < SECOND_HALF_END; y++){ 
                Board[x + (UNIT_TILE) - 1, y + (UNIT_TILE) - 1] = new CropCell(x, y);
             }
        }
        //RegenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RegenerateBoard(){
        for(int i = 0; i < BOARD_SIZE; i++){
            for(int j = 0; j < BOARD_SIZE; j++){
                Board[i, j].Plant(); //To do: Only generate crops with a 30% chance
            }
        }
    }
    public void PlayerPlant(float playerX, float playerY){
        CropCell cell;
        for(int x = 0; x < BOARD_SIZE; x++){
            for(int y = 0; y < BOARD_SIZE; y++){
                cell = Board[x, y];
                (float realX, float realY) = cell.getRealCoordinates();
                if((Math.Abs((realX - playerX)) < 1) && (Math.Abs((realY - playerY)) < 1)){
                    bool result = Board[x, y].Plant();
                    if(result) {
                        x = BOARD_SIZE;
                        y = BOARD_SIZE;
                    }
                }
            }
        }   
    }
    public void PlayerHarvest(float playerX, float playerY){
        CropCell cell;
        for(int x = 0; x < BOARD_SIZE; x++){
            for(int y = 0; y < BOARD_SIZE; y++){
                cell = Board[x, y];
                (float realX, float realY) = cell.getRealCoordinates();
                if((Math.Abs((realX - playerX)) < 1) && (Math.Abs((realY - playerY)) < 1)){
                    bool result = Board[x, y].Harvest();
                    if(result){
                        x = BOARD_SIZE;
                        y = BOARD_SIZE;
                    }
                }
            }
        }   
    }
    public class CropCell{ //Planting a seed instantly makes the crop level 1
        GameObject cropObject;
        int sunLevel;
        int waterLevel;
        public float xPos;
        public float yPos;
        public CropCell(int xPos, int yPos){
            cropObject = null; // Null means no seed or plant there
            sunLevel = 0;
            waterLevel = 0;
            this.xPos = xPos;
            this.yPos = yPos;
        }
        public void ResetCell(){
            sunLevel = 0;
            waterLevel = 0;
            if(cropObject != null){
                Destroy(cropObject);
            }
        }
        public bool Plant(){ // Was planting successful?
            if(cropObject == null){
                cropObject = new GameObject("crop");
                return true;
            }
            return false;
        }
        public bool Harvest(){
            if(cropObject != null){
                Destroy(cropObject);
                return true;
            }
            return false;
        }
        public (float xPos, float yPos) getRealCoordinates(){
            float realXPos = this.xPos;
            float realYPos = this.yPos;
            if(realXPos > 0){
                realXPos = (float)(realXPos - 0.5);
            }
            else{
                realXPos = (float)(realXPos + 0.5);
            }
            if(realYPos > 0){
                realYPos = (float)(realYPos - 0.5);
            }
            else{
                realYPos = (float)(realYPos + 0.5);
            }
            return (realXPos, realYPos);
        }

    }
}

