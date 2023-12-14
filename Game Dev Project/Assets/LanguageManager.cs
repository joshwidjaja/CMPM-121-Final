using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    enum Language {English, Japanese, Hebrew};
    Language currentLanguage;

    private void Start()
    {
        currentLanguage = Language.English;
    }

    public void ChangeLanguage(Language language)
    {
        currentLanguage = language;
    }
}
