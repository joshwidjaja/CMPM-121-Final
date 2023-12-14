using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language {English, Japanese, Hebrew};
public class LanguageManager : MonoBehaviour
{
    public Language currentLanguage;

    private void Start()
    {
        currentLanguage = Language.English;
    }

    public void ChangeLanguage(Language language)
    {
        currentLanguage = language;
    }

    public void SetToEnglish()
    {
        currentLanguage = Language.English;
    }

    public void SetToJapanese()
    {
        currentLanguage = Language.Japanese;
    }

    public void SetToHebrew()
    {
        currentLanguage = Language.Hebrew;
    }
}
