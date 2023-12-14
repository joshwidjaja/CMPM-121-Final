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
    void Start()
    {
        canvasObj = new GameObject();
        canvasObj.name = "MyCanvas";
        canvasObj.AddComponent<Canvas>();
        myCanvas = canvasObj.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        
        MakeButton("UP", -283, -105, 30, 30, "↑", GameObject.Find("Player").GetComponent<PlayerController>().MoveUp);
        MakeButton("LEFT", -315, -139, 30, 30, "←", GameObject.Find("Player").GetComponent<PlayerController>().MoveLeft);
        MakeButton("DOWN", -283, -170, 30, 30, "↓", GameObject.Find("Player").GetComponent<PlayerController>().MoveDown);
        MakeButton("RIGHT", -251, -139, 30, 30, "→", GameObject.Find("Player").GetComponent<PlayerController>().MoveRight);
        MakeButton("Plant", -283, -05, 30, 30, "P", GameObject.Find("Player").GetComponent<PlayerController>().Plant);
        MakeButton("Harvest", -283, 95, 30, 30, "H", GameObject.Find("Player").GetComponent<PlayerController>().Harvest);
        MakeButton("Undo", 483, -05, 30, 30, "U", GameObject.Find("Player").GetComponent<PlayerController>().Undo);
        MakeButton("Redo", 483, 95, 30, 30, "R", GameObject.Find("Player").GetComponent<PlayerController>().Redo);
    }
    GameObject MakeButton(string name, int pos1, int pos2, int size1, int size2, string text, UnityEngine.Events.UnityAction theFunction){
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
