using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Palmmedia.ReportGenerator.Core.Common;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private CropCell[,] Board; //A 2d array where each row/column is the same size
    private static int BOARD_SIZE = 10;
    private int totalPoints = 0;
    private readonly string[] speciesList = { "tomato", "corn", "melon" };

    private GameObject[] cropObjects; // unsure if it will work
    private byte[] sunLevels;
    private byte[] waterLevels;
    private byte[] growthLevels;
    private string[] cropSpecies;
    private Stack<BoardState> undoStack;
    private Stack<BoardState> redoStack;
    GameObject player;
    int totalCells = BOARD_SIZE * BOARD_SIZE;
    FileStream dataFile;
    StreamWriter sw;

    public PlantDefinitionLanguage plantDefinitionLanguage;
    private void Start()
    {
        // Calculate total number of cells in the crop field

        // Arrays to store sun, water, growth levels, and crop species for each cell
        string filePath = Directory.GetCurrentDirectory() + @"\data.txt";

        player = GameObject.Find("Player");
        undoStack = new Stack<BoardState>();
        redoStack = new Stack<BoardState>();
        cropObjects = new GameObject[totalCells];
        sunLevels = new byte[totalCells];
        waterLevels = new byte[totalCells];
        growthLevels = new byte[totalCells];
        cropSpecies = new string[totalCells];

        // new implementation
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                int index = x * BOARD_SIZE + y;

                cropObjects[index] = null;
                cropSpecies[index] = speciesList[UnityEngine.Random.Range(0, 3)];
                sunLevels[index] = 0;
                waterLevels[index] = 0;
                growthLevels[index] = 0;
            }
        }
        if (File.Exists(filePath))
        {
            //dataFile = File.Open(filePath, FileMode.Open);
            //dataFile.Close();
            StreamReader sr = new StreamReader(filePath);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] parsed = line.Split(' ');
                if (parsed.Contains("cell:"))
                {
                    int tempIndex = Array.IndexOf(parsed, "cell:");
                    int lineX = Int32.Parse(parsed[tempIndex + 1]);
                    int lineY = Int32.Parse(parsed[tempIndex + 2]);
                    int arrIndex = (lineX * BOARD_SIZE) + lineY;
                    if (parsed.Contains("sun:"))
                    {
                        sunLevels[arrIndex] = (byte)Int32.Parse(parsed[Array.IndexOf(parsed, "sun:") + 1]);
                    }
                    if (parsed.Contains("water:"))
                    {
                        waterLevels[arrIndex] = (byte)Int32.Parse(parsed[Array.IndexOf(parsed, "water:") + 1]);
                    }
                    if (parsed.Contains("growth:"))
                    {
                        growthLevels[arrIndex] = (byte)Int32.Parse(parsed[Array.IndexOf(parsed, "growth:") + 1]);
                    }
                    if (parsed.Contains("species:"))
                    {
                        cropSpecies[arrIndex] = parsed[Array.IndexOf(parsed, "species:") + 1];
                    }
                }
                /* else if(){//If parsed.Contains("waterrate:") then set the water rate accordingly 


                 }
                 else if(){//If parsed.Contains("winpoints:") etc, there should only be if/else if, no ELSE because the else is just going to the next line at the end of (line != null) loop

                 }*/
                line = sr.ReadLine();
            }
            sr.Close();
        }
        RegenerateBoard();
        SaveBoardState();
    }

    private void RegenerateBoard()
    {
        System.Random plantRandom = new();
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                int index = i * BOARD_SIZE + j;
                double plantChance = plantRandom.NextDouble();
                if (plantChance < 0.3)
                {
                    //Board[i, j].Plant(); 
                    Plant(i, j);
                }
            }
        }
    }

    public void PlayerPlant(float playerX, float playerY)
    {
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                //CropCell cell = Board[x, y];
                (float realX, float realY) = GetRealCoordinates(x, y);
                if ((Math.Abs(realX - playerX) < 1) && (Math.Abs(realY - playerY) < 1))
                {
                    bool result = Plant(x, y);
                    if (result) return;
                }
            }
        }
    }
    public void PlayerHarvest(float playerX, float playerY)
    {
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                //CropCell cell = Board[x, y];
                (float realX, float realY) = GetRealCoordinates(x, y);
                if ((Math.Abs(realX - playerX) < 1) && (Math.Abs(realY - playerY) < 1))
                {
                    int result = Harvest(x, y);
                    if (result != (-1))
                    {
                        totalPoints += result;
                        if (totalPoints >= 10)
                        {
                            Debug.Log("You win!");
                        }
                        return;
                    }
                }
            }
        }
    }

    public int GetSunLevel(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        return sunLevels[index];
    }

    public int GetWaterLevel(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        return waterLevels[index];
    }

    public int GetGrowthLevel(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        return growthLevels[index];
    }

    public string GetSpecies(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        return cropSpecies[index];
    }

    public bool Plant(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        if (cropObjects[index] == null)
        {
            cropObjects[index] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cropObjects[index].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            // NEEDS TO BE REWRITTEN
            (float newX, float newY) = GetRealCoordinates(x, y);
            cropObjects[index].transform.position = new Vector3(newX, 0.0f, newY);
            //
            Material myMaterial = new(Shader.Find("Standard"));
            cropObjects[index].GetComponent<Renderer>().material = myMaterial;
            switch (cropSpecies[index])
            {
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

    public int Harvest(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        if (cropObjects[index] != null)
        {
            Destroy(cropObjects[index]);
            int cropValue = growthLevels[index];
            sunLevels[index] = 0;
            waterLevels[index] = 0;
            growthLevels[index] = 0;
            return cropValue;
        }
        return -1;
    }

    public void SizeCheck(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        if (cropObjects[index] == null)
        {
            return;
        }

        float[] sizeList = { 0.2f, 0.3f, 0.4f, 0.5f };
        if ((sunLevels[index] >= 1) && (waterLevels[index] >= 1) && (growthLevels[index] < 1))
        {
            growthLevels[index] = 1;
        }
        if ((sunLevels[index] >= 3) && (waterLevels[index] >= 3) && (growthLevels[index] < 2))
        {
            growthLevels[index] = 2;
        }
        if ((sunLevels[index] >= 5) && (waterLevels[index] >= 5) && (growthLevels[index] < 3))
        {
            growthLevels[index] = 3;
        }

        float selectedSize = sizeList[growthLevels[index]];
        cropObjects[index].transform.localScale = new Vector3(selectedSize, selectedSize, selectedSize);
    }

    public void ResetCell(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        sunLevels[index] = 0;
        waterLevels[index] = 0;
        if (cropObjects[index] != null)
        {
            Destroy(cropObjects[index]);
        }
    }

    public void ResetSunLevel(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        sunLevels[index]--;
        if (sunLevels[index] < 0) sunLevels[index] = 0;
    }

    public void SpawnSun(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        int sunSpawnRate = 3;
        if (sunSpawnRate > UnityEngine.Random.Range(0, 10))
        {
            sunLevels[index] = (byte)UnityEngine.Random.Range(1, 6);
        }
    }

    public void SpawnWater(int x, int y)
    {
        int index = x * BOARD_SIZE + y;
        int waterSpawnRate = 3;
        if (waterSpawnRate > UnityEngine.Random.Range(0, 10))
        {
            waterLevels[index]++;
        }
    }

    public void TriggerTurn()
    { //Once player does an action, this is triggered, should randomly add water or sun to cells or whatever
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                ResetSunLevel(x, y);
                SpawnSun(x, y);
                SpawnWater(x, y);
                SizeCheck(x, y);

            }
        }
        SaveBoardState();
    }

    public void SaveBoardState()
    {
        BoardState boardState = new BoardState(cropObjects, sunLevels, waterLevels, growthLevels, cropSpecies, player.transform.position, totalPoints);
        undoStack.Push(boardState);
    }

    public void LoadBoardState(BoardState boardState)
    {
        for (int i = 0; i < totalCells; i++)
        {
            cropObjects[i] = boardState.cropObjects[i];
            sunLevels[i] = boardState.sunLevels[i];
            waterLevels[i] = boardState.waterLevels[i];
            growthLevels[i] = boardState.growthLevels[i];
            cropSpecies[i] = boardState.cropSpecies[i];
        }
        player.transform.position = new Vector3(boardState.playerPosition.x, boardState.playerPosition.y, boardState.playerPosition.z);
        totalPoints = boardState.totalPoints;
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                SizeCheck(x, y);

            }
        }
    }

    public void Undo()
    {
        if (undoStack.Count <= 1)
        {
            Debug.Log("Nothing to undo");
            return;
        }
        redoStack.Push(undoStack.Pop());
        LoadBoardState(undoStack.Peek());
    }

    public void Redo()
    {
        if (redoStack.Count <= 0)
        {
            Debug.Log("Nothing to redo");
            return;
        }

        undoStack.Push(redoStack.Pop());
        //LoadBoardState(redoStack.Peek());
        LoadBoardState(undoStack.Peek());
    }

    public (float xPos, float yPos) GetRealCoordinates(float xPos, float yPos)
    {
        float realXPos = xPos - (BOARD_SIZE / 2);
        float realYPos = yPos - (BOARD_SIZE / 2);
        if (realXPos > (-1))
        {
            realXPos++;
        }
        if (realYPos > (-1))
        {
            realYPos++;
        }
        if (realXPos > 0)
        {
            realXPos = (float)(realXPos - 0.5f);
        }
        else
        {
            realXPos = (float)(realXPos + 0.5f);
        }
        if (realYPos > 0)
        {
            realYPos = (float)(realYPos - 0.5f);
        }
        else
        {
            realYPos = (float)(realYPos + 0.5f);
        }
        return (realXPos, realYPos);
    }

    public class BoardState
    {
        public GameObject[] cropObjects; // unsure if it will work
        public byte[] sunLevels;
        public byte[] waterLevels;
        public byte[] growthLevels;
        public string[] cropSpecies;
        public Vector3 playerPosition;
        public int totalPoints;
        public BoardState(GameObject[] cropObject, byte[] sunLevel, byte[] waterLevel, byte[] growthLevel, string[] cropSpeciesList, Vector3 playerPosition, int totalPoints)
        {
            this.cropObjects = new GameObject[sunLevel.Length];
            this.sunLevels = new byte[sunLevel.Length];
            this.waterLevels = new byte[sunLevel.Length];
            this.growthLevels = new byte[sunLevel.Length];
            this.cropSpecies = new string[sunLevel.Length];
            this.playerPosition = new Vector3();
            for (int i = 0; i < sunLevel.Length; i++)
            {
                this.cropObjects[i] = cropObject[i];
                this.sunLevels[i] = sunLevel[i];
                this.waterLevels[i] = waterLevel[i];
                this.growthLevels[i] = growthLevel[i];
                this.cropSpecies[i] = cropSpeciesList[i];
            }
            this.playerPosition.x = playerPosition.x;
            this.playerPosition.y = playerPosition.y;
            this.playerPosition.z = playerPosition.z;
            this.totalPoints = totalPoints;


        }
    }
}

