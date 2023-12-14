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
    //public LanguageManager languageManager;
    const float buttonSquareX = 30f;
    const float buttonSquareY = 30f;

    
    const int EN = 0;
    const int JA = 1;
    const int HE = 2;

    string[] Plant = new [] {"Plant", "植える", "שתל"};
    string[] Harvest = new [] {"Harvest", "収穫", "קצר"};
    string[] Undo = new [] {"Undo", "元に戻す", "בטל"};
    string[] Redo = new [] {"Redo", "やり直し", "בצע שוב"};
    
    Dictionary<GameObject, string[]> localizables;
    TMP_FontAsset myTMPFont;
    void Start()
    {
    
        for(int i = 0; i < Font.GetOSInstalledFontNames().Length; i++){
            Debug.Log(Font.GetOSInstalledFontNames()[i]);
        }
        Font theFont = new Font(Font.GetPathsToOSFonts()[Array.IndexOf(Font.GetOSInstalledFontNames(), "Microsoft Sans Serif")]);
        myTMPFont = TMP_FontAsset.CreateFontAsset(theFont);
        Debug.Log(myTMPFont);
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
        
        localizables.Add(MakeButton("Plant", -275, -05, 90, buttonSquareY, "Plant", playerController.Plant), Plant);
        localizables.Add(MakeButton("Harvest", -275, 95, 90, buttonSquareY, "Harvest", playerController.Harvest), Harvest);
        localizables.Add(MakeButton("Undo", 275, -05, 90, buttonSquareY, "Undo", playerController.Undo), Undo);
        localizables.Add(MakeButton("Redo", 275, 95, 90, buttonSquareY, "Redo", playerController.Redo), Redo);

        MakeButton("English",  275, 168, buttonSquareX * 3f, buttonSquareY, "English", SetToEnglish);
        MakeButton("Japanese", 375, 168, buttonSquareX * 3f, buttonSquareY, "日本語", SetToJapanese);
        MakeButton("Hebrew", 475, 168, buttonSquareX * 3f, buttonSquareY, "עברית", SetToHebrew);
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
        textComponent.font = myTMPFont;
        return uiObject;
    }
    public void SetToEnglish()
    {
        UpdateLanguage(EN);
    }
    public void SetToJapanese()
    {
        UpdateLanguage(JA);
    }
    public void SetToHebrew()
    {
        UpdateLanguage(HE);
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
                    width = buttonSquareX * 3f;
                    break;
                default:
                    width = buttonSquareX;
                    break;
                case JA:
                    width = buttonSquareX * 3f;
                    break;
                case HE:
                    width = buttonSquareX * 3f;
                    break;
            }

            SetButtonWidth(entry.Key, width);
        }
    }

    public void SetButtonWidth(GameObject uiObject, float width)
    {
        RectTransform uiTransform = uiObject.GetComponent<RectTransform>();
        RectTransform textTransform = uiObject.transform.GetChild(0).GetComponent<RectTransform>();
        uiTransform.sizeDelta = new Vector2(width, buttonSquareY);
        textTransform.sizeDelta = new Vector2(width, buttonSquareY);
    }

    public void SetButtonText(GameObject uiObject, string text)
    {
        TextMeshProUGUI textComponent = uiObject.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.SetText(text);
    }
}
