using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace DinoGame {
  public class SettingsManager : MonoBehaviour {
    public static SettingsManager Instance { get; private set; }
    public UnityAction<LanguageDictionary> OnGetLangFromYandex;

    public LanguageObject languageObject { get; private set; }

    private LanguageDictionary LangDict { get; set; }

    private void Awake() {
      languageObject = GetLanguageObject();
      LangDict = languageObject.en;
      Instance = this;

      DontDestroyOnLoad(this);
    }

    private void OnDestroy() {
      if (Instance == this) {
        Instance = null;
      }
    }

    public float GetMusicVolume() {
      return PlayerPrefs.GetFloat("musicVolume", 1f);
    }

    public void SetMusicVolume(float volume) {
      PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public float GetSoundsVolume() {
      return PlayerPrefs.GetFloat("soundsVolume", 1f);
    }

    public void SetSoundsVolume(float volume) {
      PlayerPrefs.SetFloat("soundsVolume", volume);
    }

    public string GetLanguageKeyFromPrefs() {
      return PlayerPrefs.GetString("lang", "EN");
    }

    public void SetLanguageKeyToPrefs(string lang) {
      PlayerPrefs.SetString("lang", lang);
    }

    public void GetYandexLangAsync(string languageKey) {
      LangDict = languageKey switch {
        "ru" => languageObject.ru,
        "en" => languageObject.en,
        _ => LangDict
      };
      OnGetLangFromYandex?.Invoke(LangDict);
    }

    public void SetScoreFromLeaderboard(float? hiScore) {
      if (hiScore is > 0) {
        PlayerPrefs.SetFloat("hiScore", (float)hiScore);
      }
    }

    public LanguageDictionary GetLanguageDictionary() {
      if (IsBrowser()) return LangDict;
      switch (GetLanguageKeyFromPrefs()) {
        case "EN":
        case "en":
          return languageObject.en;
        case "RU":
        case "ru":
          return languageObject.ru;
        default:
          return languageObject.en;
      }
    }

    public void QuitGame() {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#endif
      Application.Quit();
    }

    public bool IsBrowser() {
#if !UNITY_EDITOR && UNITY_WEBGL
        return true;
#endif
      return false;
    }

    [Serializable]
    public class LanguageObject {
      public LanguageDictionary ru;
      public LanguageDictionary en;
    }

    [Serializable]
    public class LanguageDictionary {
      public string play;
      public string settings;
      public string quit;
      public string back;
      public string music;
      public string sounds;
      public string yes;
      public string no;
      public string leaveGame;
      public string resume;
      public string lang;
      public string hi;
      public string gameOver;
      public string watchAds;
      public string watchLabel;
    }

    public LanguageObject GetLanguageObject() {
      var locale = Resources.Load<TextAsset>("localization");
      var languageObject = JsonUtility.FromJson<LanguageObject>(locale.text);
      return languageObject;
    }
  }
}