using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public enum Language {English, Japanese, Hebrew};
    public Language currentLanguage;

    private void Start()
    {
        currentLanguage = Language.English;
    }

    public void ChangeLanguage(Language language)
    {
        currentLanguage = language;
    }
}
