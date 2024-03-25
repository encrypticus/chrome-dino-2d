using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace DinoGame {
  public class MainMenuUI : MonoBehaviour {
    private UIDocument _uiDocument;
    private VisualElement _root;
    private UI.CustomControls.MainMenu _mainMenu;

    private void Awake() {
      InitControls();
    }

    private void Start() {
      _mainMenu.PlayClicked += () => StartGame();
      _mainMenu.SettingsClicked += () => ShowSettingsControls();
      _mainMenu.BackClicked += () => ShowMainControls();
      _mainMenu.QuitClicked += () => SettingsManager.Instance.QuitGame();
      _mainMenu.SetPlayButtonFocus();
    }

    private void InitControls() {
      _uiDocument = GetComponent<UIDocument>();
      _root = _uiDocument.rootVisualElement;
      var languageKey = SettingsManager.Instance.GetLanguageKeyFromPrefs();

      _mainMenu = new UI.CustomControls.MainMenu(
        dict: SettingsManager.Instance.GetLanguageDictionary(),
        languageKey
      );
      _root.Add(_mainMenu);
      _mainMenu.selectLanguage.customDropdown.RegisterValueChangedCallback(e => {
        SettingsManager.Instance.SetLanguageKeyToPrefs(e.newValue);
        var langObject = SettingsManager.Instance.languageObject;
        var langDict = e.newValue switch {
          "RU" => langObject.ru,
          "EN" => langObject.en,
          _ => langObject.en
        };
        _mainMenu.ChangeLanguage(langDict);
      });

      if (SettingsManager.Instance.IsBrowser()) {
        SettingsManager.Instance.OnGetLangFromYandex += langDict => {
          _mainMenu.ChangeLanguage(langDict);
          _mainMenu.selectLanguage.RemoveFromHierarchy();
        };
      }
    }

    private void ShowSettingsControls() {
      _mainMenu.ShowSettingsControls();
    }

    private void ShowMainControls() {
      _mainMenu.ShowMainControls();
    }

    private void StartGame() {
      StartCoroutine(LoadGameAsync());
    }

    IEnumerator LoadGameAsync() {
      AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
      _mainMenu.ShowLoadingProgress();

      while (!loadOperation.isDone) {
        float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
        _mainMenu.UpdateLoadingProgressValue(progressValue);
        yield return null;
      }
    }
  }
}