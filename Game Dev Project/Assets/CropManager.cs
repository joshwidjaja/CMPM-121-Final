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
    int boardSize = 10;
    int counter = 0;
    void Start()
    { 
        Board = new CropCell[boardSize, boardSize]; //Each "cell"'s area/square is from index x to index x+1, and index y to index y+1
        for(int x = (0-(boardSize/2)); x < 0; x++){ //Left half of board ex: -5 to 0(not inclsuive)
            for(int y = (0-(boardSize/2)); y < 0; y++){ 
                Board[x + (boardSize/2), y + (boardSize/2)] = new CropCell(x, y);
             }
             for(int y = 1 ; y < ((boardSize/2) + 1); y++){ 
                Board[x + (boardSize/2), y + (boardSize/2) - 1] = new CropCell(x, y);
             }
        }
        for(int x = 1 ; x < ((boardSize/2) + 1); x++){ //Right half
            for(int y = (0-(boardSize/2)); y < 0; y++){ 
                Board[x + (boardSize/2) - 1, y + (boardSize/2)] = new CropCell(x, y);
             }
             for(int y = 1 ; y < ((boardSize/2) + 1); y++){ 
                Board[x + (boardSize/2) - 1, y + (boardSize/2) - 1] = new CropCell(x, y);
             }
        }
        //RegenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RegenerateBoard(){
        for(int i = 0; i < boardSize; i++){
            for(int j = 0; j < boardSize; j++){
                Board[i, j].Plant(); //To do: Only generate crops with a 30% chance
            }
        }
    }
    public void PlayerPlant(float playerx, float playery){
        CropCell theCell;
        for(int x = 0; x < boardSize; x++){
            for(int y = 0; y < boardSize; y++){
                theCell = Board[x, y];
                (float realx, float realy) = theCell.getRealCoordinates();
                if((Math.Abs((realx - playerx)) < 1) && (Math.Abs((realy - playery)) < 1)){
                    bool result = Board[x, y].Plant();
                    if(result == true){
                        x = boardSize;
                        y = boardSize;
                    }
                }
            }
        }   
    }
    public void PlayerHarvest(float playerx, float playery){
        CropCell theCell;
        for(int x = 0; x < boardSize; x++){
            for(int y = 0; y < boardSize; y++){
                theCell = Board[x, y];
                (float realx, float realy) = theCell.getRealCoordinates();
                if((Math.Abs((realx - playerx)) < 1) && (Math.Abs((realy - playery)) < 1)){
                    bool result = Board[x, y].Harvest();
                    if(result == true){
                        x = boardSize;
                        y = boardSize;
                    }
                }
            }
        }   
    }
    public class CropCell{ //Planting a seed instantly makes the crop level 1
        GameObject cropObject;
        int sunLevel;
        int waterLevel;
        public float xpos;
        public float ypos;
        public CropCell(int xpos, int ypos){
            cropObject = null; // Null means no seed or plant there
            sunLevel = 0;
            waterLevel = 0;
            this.xpos = xpos;
            this.ypos = ypos;
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
            else{
                return false;
            }
        }
        public bool Harvest(){
            if(cropObject != null){
                Destroy(cropObject);
                return true;
            }
            else{
                return false;
            }
        }
        public (float xpos, float ypos) getRealCoordinates(){
            float realxpos = this.xpos;
            float realypos = this.ypos;
            if(realxpos > 0){
                realxpos = (float)(realxpos - 0.5);
            }
            else{
                realxpos = (float)(realxpos + 0.5);
            }
            if(realypos > 0){
                realypos = (float)(realypos - 0.5);
            }
            else{
                realypos = (float)(realypos + 0.5);
            }
            return (realxpos, realypos);
        }

    }
}

