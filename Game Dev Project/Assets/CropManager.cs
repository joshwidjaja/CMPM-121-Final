using System;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    // Start is called before the first frame update
    CropCell[,] Board; //A 2d array where each row/column is the same size
    int BOARD_SIZE = 10;
    int totalpoints = 0;
    String[] speciesList = {"tomato", "corn", "melon"};
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
                Board[x + UNIT_TILE, y + UNIT_TILE] = new CropCell(x, y, this.speciesList[UnityEngine.Random.Range(0, 3)]);
             }
             for(int y = SECOND_HALF_START ; y < SECOND_HALF_END; y++){ 
                Board[x + UNIT_TILE, y + UNIT_TILE - 1] = new CropCell(x, y, this.speciesList[UnityEngine.Random.Range(0, 3)]);
             }
        }
        for(int x = SECOND_HALF_START ; x < SECOND_HALF_END; x++){ //Right half
            for(int y = FIRST_HALF_START; y < FIRST_HALF_END; y++){ 
                Board[x + UNIT_TILE - 1, y + UNIT_TILE] = new CropCell(x, y, this.speciesList[UnityEngine.Random.Range(0, 3)]);
             }
             for(int y = SECOND_HALF_START ; y < SECOND_HALF_END; y++){ 
                Board[x + UNIT_TILE - 1, y + UNIT_TILE - 1] = new CropCell(x, y, this.speciesList[UnityEngine.Random.Range(0, 3)]);
             }
        }
        RegenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RegenerateBoard(){
        System.Random plantRandom = new System.Random();
        for(int i = 0; i < BOARD_SIZE; i++){
            for(int j = 0; j < BOARD_SIZE; j++){
                double plantChance = plantRandom.NextDouble();
                if(plantChance < 0.3){
                    Board[i, j].Plant(); //To do: Only generate crops with a 30% chance
                }
            }
        }
    }
    public void PlayerPlant(float playerX, float playerY){
        for(int x = 0; x < BOARD_SIZE; x++){
            for(int y = 0; y < BOARD_SIZE; y++){
                CropCell cell = Board[x, y];
                (float realX, float realY) = cell.getRealCoordinates();
                if((Math.Abs(realX - playerX) < 1) && (Math.Abs(realY - playerY) < 1)){
                    bool result = Board[x, y].Plant();
                    if(result) return;
                }
            }
        }   
    }
    public void PlayerHarvest(float playerX, float playerY){
        for(int x = 0; x < BOARD_SIZE; x++){
            for(int y = 0; y < BOARD_SIZE; y++){
                CropCell cell = Board[x, y];
                (float realX, float realY) = cell.getRealCoordinates();
                if((Math.Abs(realX - playerX) < 1) && (Math.Abs(realY - playerY) < 1)){
                    int result = Board[x, y].Harvest();
                    if(result != (-1)){
                        totalpoints += result;
                        if(totalpoints >= 10){
                            Debug.Log("You win!");
                        }
                        return;
                    } 
                }
            }
        }   
    }

    public void triggerTurn(){ //Once player does an action, this is triggered, should randomly add water or sun to cells or whatever
        for (int x = 0; x < BOARD_SIZE; x++) {
            for (int y = 0; y < BOARD_SIZE; y++) {
                CropCell cell = Board[x, y];
                //cell.ResetSunLevel();
                cell.ResetSunLevel();
                cell.SpawnSun();
                cell.SpawnWater();
                cell.SizeCheck();
            }
        }
    }
    public class CropCell{ //Planting a seed instantly makes the crop level 1
        GameObject cropObject;
        public int sunLevel;
        public int waterLevel;
        public float xPos;
        public float yPos;
        public int growthLevel;
        String species;
        public CropCell(int xPos, int yPos, String species){
            cropObject = null; // Null means no seed or plant there
            sunLevel = 0;
            waterLevel = 0;
            this.xPos = xPos;
            this.yPos = yPos;
            growthLevel = 0;
            this.species = species;
        }
        public void ResetCell(){
            sunLevel = 0;
            waterLevel = 0;
            if(cropObject != null){
                Destroy(cropObject);
            }
        }
        public void ResetSunLevel() {
            sunLevel--;
            if (sunLevel < 0) sunLevel = 0;
        }
        public void SpawnSun() {
            int sunSpawnRate = 3;
            if (sunSpawnRate > UnityEngine.Random.Range(0, 10)) {
                //sunLevel++;
                sunLevel = UnityEngine.Random.Range(1,6);
            }
            
        }
        public void SpawnWater() {
            int waterSpawnRate = 3;
            if (waterSpawnRate > UnityEngine.Random.Range(0, 10)) {
                waterLevel++;
            }
        }
        public bool Plant(){ // Was planting successful?
            if(cropObject == null){
                cropObject = GameObject.CreatePrimitive(PrimitiveType.Cube);//new GameObject("crop");
                cropObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                //cropObject.transform.localScale = new Vector3(sizeList[this.growthLevel], sizeList[this.growthLevel], sizeList[this.growthLevel]);
                (float newx, float newy) = this.getRealCoordinates();                                                               
                cropObject.transform.position = new Vector3(newx, 0.0f, newy);
                Material myMaterial = new Material(Shader.Find("Standard"));
                cropObject.GetComponent<Renderer>().material = myMaterial;
                switch(species){
                    case "tomato":
                        myMaterial.color = Color.red;
                        break;
                    case "corn":
                        myMaterial.color = Color.yellow;
                        break;
                    case "melon":
                        myMaterial.color = Color.green;
                        break;

                }
                return true;
            }
            return false;
        }
        public int Harvest(){
            if(cropObject != null){
                Destroy(cropObject);
                int tempGrowth = growthLevel;
                this.growthLevel = 0;
                this.sunLevel = 0;
                this.waterLevel = 0;
                return tempGrowth;
            }
            return -1;
        }
        public void SizeCheck(){
            if(cropObject == null){
                return;
            }
            float[] sizeList = {0.2f, 0.3f, 0.4f, 0.5f};
            if((this.sunLevel >= 1) && (this.waterLevel >= 1) && (this.growthLevel < 1)){
                this.growthLevel = 1;
            }
            if((this.sunLevel >= 3) && (this.waterLevel >= 3) && (this.growthLevel < 2)){
                this.growthLevel = 2;
            }
            if((this.sunLevel >= 5) && (this.waterLevel >= 5) && (this.growthLevel < 3)){
                this.growthLevel = 3;
            }
            cropObject.transform.localScale = new Vector3(sizeList[this.growthLevel], sizeList[this.growthLevel], sizeList[this.growthLevel]);
            
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

