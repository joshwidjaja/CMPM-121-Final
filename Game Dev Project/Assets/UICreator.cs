using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UICreator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject canvasObj;
    Canvas myCanvas;

    public PlayerController playerController;
    public LanguageManager languageManager;

    void Start()
    {
        float buttonSquareX = 30f;
        float buttonSquareY = 30f;

        canvasObj = new GameObject();
        canvasObj.name = "MyCanvas";
        canvasObj.AddComponent<Canvas>();
        myCanvas = canvasObj.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        
        MakeButton("UP", -283, -105, buttonSquareX, buttonSquareY, "↑", playerController.MoveUp);
        MakeButton("LEFT", -315, -139, buttonSquareX, buttonSquareY, "←", playerController.MoveLeft);
        MakeButton("DOWN", -283, -170, buttonSquareX, buttonSquareY, "↓", playerController.MoveDown);
        MakeButton("RIGHT", -251, -139, buttonSquareX, buttonSquareY, "→", playerController.MoveRight);
        MakeButton("Plant", -283, -05, buttonSquareX, buttonSquareY, "P", playerController.Plant);
        MakeButton("Harvest", -283, 95, buttonSquareX, buttonSquareY, "H", playerController.Harvest);
        MakeButton("Undo", 483, -05, buttonSquareX, buttonSquareY, "U", playerController.Undo);
        MakeButton("Redo", 483, 95, buttonSquareX, buttonSquareY, "R", playerController.Redo);

        MakeButton("English", -7, 168, buttonSquareX * 3f, buttonSquareY, "English", languageManager.SetToEnglish);
        MakeButton("Japanese", 102, 168, buttonSquareX * 2f, buttonSquareY, "日本語", languageManager.SetToJapanese);
        MakeButton("Hebrew", 202, 168, buttonSquareX * 2.5f, buttonSquareY, "עִבְֿרִית", languageManager.SetToHebrew);
    }
    GameObject MakeButton(string name, int pos1, int pos2, float size1, float size2, string text, UnityEngine.Events.UnityAction theFunction){
        GameObject uiObject = new GameObject();
        uiObject.name = name;
        uiObject.AddComponent<UnityEngine.UI.Button>();
        uiObject.AddComponent<RectTransform>();
        UnityEngine.UI.Button uiButton = uiObject.GetComponent<UnityEngine.UI.Button>();
        uiObject.AddComponent<UnityEngine.UI.Image>();
        RectTransform uiTransform = uiObject.GetComponent<RectTransform>();
        UnityEngine.UI.Button.ButtonClickedEvent clickedEvent = new UnityEngine.UI.Button.ButtonClickedEvent();
        clickedEvent.AddListener(theFunction);
        uiButton.onClick = clickedEvent;
        uiObject.transform.parent = myCanvas.transform;

        uiTransform.anchoredPosition = new Vector3(pos1, pos2);
        uiTransform.sizeDelta = new Vector2(size1, size2);
        GameObject uiText = new GameObject();
        uiText.transform.parent = uiObject.transform;
        uiText.AddComponent<TextMeshProUGUI>();
        TextMeshProUGUI textComponent = uiText.GetComponent<TextMeshProUGUI>();

        RectTransform textTransform = uiText.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector2(0, 0);
        textTransform.sizeDelta = new Vector2(30, 30);
        textComponent.fontSize = 24;
        textComponent.SetText(text);
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.color = Color.black;
        return uiObject;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
