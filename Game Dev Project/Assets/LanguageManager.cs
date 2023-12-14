using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum Language {English, Japanese, Hebrew};
public class LanguageManager : MonoBehaviour
{
    const int EN = 0;
    const int JA = 1;
    const int HE = 2;
    public UICreator uiCreator;
    public int currentLanguage;

    private void Start()
    {
        currentLanguage = EN;
    }

    public void SetToEnglish()
    {
        currentLanguage = EN;
        uiCreator.UpdateLanguage(EN);
    }

    public void SetToJapanese()
    {
        currentLanguage = JA;
        uiCreator.UpdateLanguage(JA);
    }

    public void SetToHebrew()
    {
        currentLanguage = HE;
        uiCreator.UpdateLanguage(HE);
    }
}
