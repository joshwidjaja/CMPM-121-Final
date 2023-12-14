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

    const float buttonSquareX = 30f;
    const float buttonSquareY = 30f;
    const int EN = 0;
    const int JA = 1;
    const int HE = 2;

    string[] Plant = new [] {"P", "植える", "שתל"};
    string[] Harvest = new [] {"H", "収穫", "קצר"};
    string[] Undo = new [] {"U", "元に戻す", "בטל"};
    string[] Redo = new [] {"R", "やり直し", "בצע שוב"};

    Dictionary<GameObject, string[]> localizables;

    void Start()
    {
        canvasObj = new GameObject();
        canvasObj.name = "MyCanvas";
        canvasObj.AddComponent<Canvas>();
        myCanvas = canvasObj.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        localizables = new Dictionary<GameObject, string[]>();
        
        MakeButton("UP", -283, -105, buttonSquareX, buttonSquareY, "↑", playerController.MoveUp);
        MakeButton("LEFT", -315, -139, buttonSquareX, buttonSquareY, "←", playerController.MoveLeft);
        MakeButton("DOWN", -283, -170, buttonSquareX, buttonSquareY, "↓", playerController.MoveDown);
        MakeButton("RIGHT", -251, -139, buttonSquareX, buttonSquareY, "→", playerController.MoveRight);
        
        localizables.Add(MakeButton("Plant", -283, -05, buttonSquareX, buttonSquareY, "P", playerController.Plant), Plant);
        localizables.Add(MakeButton("Harvest", -283, 95, buttonSquareX, buttonSquareY, "H", playerController.Harvest), Harvest);
        localizables.Add(MakeButton("Undo", 253, -05, buttonSquareX, buttonSquareY, "U", playerController.Undo), Undo);
        localizables.Add(MakeButton("Redo", 253, 95, buttonSquareX, buttonSquareY, "R", playerController.Redo), Redo);

        MakeButton("English", -7, 168, buttonSquareX * 3f, buttonSquareY, "English", languageManager.SetToEnglish);
        MakeButton("Japanese", 102, 168, buttonSquareX * 2f, buttonSquareY, "日本語", languageManager.SetToJapanese);
        MakeButton("Hebrew", 202, 168, buttonSquareX * 2.5f, buttonSquareY, "עברית", languageManager.SetToHebrew);
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
        textTransform.sizeDelta = new Vector2(size1, size2);
        textComponent.fontSize = 24;
        textComponent.SetText(text);
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.color = Color.black;
        return uiObject;
    }

    public void UpdateLanguage(int language)
    {
        foreach(KeyValuePair<GameObject, string[]> entry in localizables)
        {
            SetButtonText(entry.Key, entry.Value[language]);

            float width;
            switch (language)
            {
                case EN:
                default:
                    width = buttonSquareX;
                    break;
                case JA:
                    width = buttonSquareX * 3f;
                    break;
                case HE:
                    width = buttonSquareX * 2.5f;
                    break;
            }

            SetButtonWidth(entry.Key, width);
        }
    }

    public void SetButtonWidth(GameObject uiObject, float width)
    {
        RectTransform uiTransform = uiObject.GetComponent<RectTransform>();
        RectTransform textTransform = uiObject.GetComponentInChildren<RectTransform>();

        uiTransform.sizeDelta = new Vector2(width, buttonSquareY);
        textTransform.sizeDelta = new Vector2(width, buttonSquareY);
    }

    public void SetButtonText(GameObject uiObject, string text)
    {
        TextMeshProUGUI textComponent = uiObject.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.SetText(text);
    }
}
