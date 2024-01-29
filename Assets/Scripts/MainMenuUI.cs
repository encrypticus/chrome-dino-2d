using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace DinoGame {
  public class MainMenuUI : MonoBehaviour {
    private UIDocument _uiDocument;

    private VisualElement _root;
    private VisualElement _mainControls;
    private VisualElement _settingsControls;

    private Button _playButton;
    private Button _settingsButton;
    private Button _backButton;
    private ProgressBar _startGameProgressBar;
    private Label _startGameProgressBarLabel;

    private void Awake() {
      _uiDocument = GetComponent<UIDocument>();
      _root = _uiDocument.rootVisualElement;

      _mainControls = _root.Q<VisualElement>("MainControls");
      _settingsControls = _root.Q<VisualElement>("SettingsControls");

      _playButton = _root.Q<Button>("ButtonPlay");
      _settingsButton = _root.Q<Button>("ButtonSettings");
      _backButton = _root.Q<Button>("ButtonBack");

      _startGameProgressBar = _root.Q<ProgressBar>("LoadingProgress");
      _startGameProgressBarLabel = _startGameProgressBar.Q<Label>();
    }

    private void Start() {
      _startGameProgressBar.lowValue = 0f;
      _startGameProgressBar.highValue = 1f;
      _startGameProgressBar.value = 0f;
      _startGameProgressBar.style.display = DisplayStyle.None;

      _playButton.Focus();
      _playButton.clicked += () => StartGame();
      _settingsButton.clicked += () => ShowSettingsControls();
      _backButton.clicked += () => ShowMainControls();
    }

    private void ShowSettingsControls() {
      _mainControls.style.display = DisplayStyle.None;
      _settingsControls.style.display = DisplayStyle.Flex;
      _backButton.Focus();
    }

    private void ShowMainControls() {
      _mainControls.style.display = DisplayStyle.Flex;
      _settingsControls.style.display = DisplayStyle.None;
      _playButton.Focus();
    }

    private void StartGame() {
      StartCoroutine(LoadGameAsync());
    }

    IEnumerator LoadGameAsync() {
      AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
      _startGameProgressBar.style.display = DisplayStyle.Flex;

      while (!loadOperation.isDone) {
        float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
        _startGameProgressBar.value = progressValue;
        _startGameProgressBarLabel.text = $"{progressValue * 100}%";
        yield return null;
      }
    }
  }
}