using JetBrains.Annotations;
using UI.CustomControls;
using UnityEngine;
using UnityEngine.UIElements;
using PopupWindow = UI.CustomControls.PopupWindow;

namespace DinoGame {
  public class GameUI : MonoBehaviour {
    private UIDocument _uiDocument;
    private VisualElement _root;
    private PauseButton _pauseButton;
    private CustomButton _resumeButton;
    private CustomButton _quitButton;
    private VisualElement _pauseOverlay;
    private VisualElement _controls;
    private Label _musicLabel;
    private Label _soundsLabel;
    [CanBeNull] private PopupWindow _popupWindow;
    private bool _onPause;

    private PlayerInput _input;

    private void Awake() {
      _input = new PlayerInput();
      _uiDocument = GetComponent<UIDocument>();
      _root = _uiDocument.rootVisualElement;

      _controls = _root.Q<VisualElement>("Controls");
      _pauseButton = _root.Q<PauseButton>("PauseButton");
      _resumeButton = _root.Q<CustomButton>("ResumeButton");
      _pauseOverlay = _root.Q<VisualElement>("PauseOverlay");
      _quitButton = _root.Q<CustomButton>("QuitButton");
      _musicLabel = _root.Q<Label>("MusicLabel");
      _soundsLabel = _root.Q<Label>("SoundsLabel");

      if (SettingsManager.Instance.IsBrowser()) {
        _quitButton.RemoveFromHierarchy();
        return;
      }

      _popupWindow = new PopupWindow(SettingsManager.Instance.GetLanguageDictionary());
      _pauseOverlay.Add(_popupWindow);
    }

    private void OnEnable() {
      _input.Player.Enable();
    }

    private void OnDisable() {
      _input.Player.Disable();
    }

    private void Start() {
      var dict = SettingsManager.Instance.GetLanguageDictionary();
      _resumeButton.text = dict.resume;
      _quitButton.text = dict.quit;
      _musicLabel.text = dict.music;
      _soundsLabel.text = dict.sounds;
      _pauseButton.clicked += () => ShowPauseOverlay(true);
      _resumeButton.clicked += () => ShowPauseOverlay(false);

      if (!SettingsManager.Instance.IsBrowser()) {
        _quitButton.clicked += () => ShowPopupWindow(true);
        _popupWindow.OkClicked += () => {
          ShowPopupWindow(false);
          SettingsManager.Instance.QuitGame();
        };
        _popupWindow.CancelClicked += () => ShowPopupWindow(false);
      }
      else {
        SettingsManager.Instance.OnGetLangFromYandex += langDict => {
          _resumeButton.text = langDict.resume;
          _musicLabel.text = langDict.music;
          _soundsLabel.text = langDict.sounds;
        };
      }

      _input.Player.Pause.performed += _ => {
        if (GameManager.Instance.IsGameOver) {
          GameManager.Instance.NewGame();
        }
        else {
          if (GameManager.Instance.IsPlayerLive) {
            if (_onPause) {
              ShowPauseOverlay(false);
            }
            else {
              ShowPauseOverlay(true);
            }
          }
        }
      };
    }

    private void Paused() {
      Time.timeScale = 0;
      AudioListener.pause = true;
    }

    private void Resumed() {
      Time.timeScale = 1;
      AudioListener.pause = false;
      GameManager.Instance.SetSoundTrackVolume();
      GameManager.Instance.SetSoundsVolume();
    }

    private void ShowPauseOverlay(bool show) {
      _onPause = show;
      if (show) {
        _pauseOverlay.style.display = DisplayStyle.Flex;
        _resumeButton.Focus();
        Paused();
      }
      else {
        _pauseOverlay.style.display = DisplayStyle.None;
        Resumed();
      }
    }

    private void ShowPopupWindow(bool show) {
      if (show) {
        _popupWindow?.RemoveFromClassList("translate-y");
        _popupWindow?.SetFocus();
        _controls.AddToClassList("translate-y");
      }
      else {
        _popupWindow?.AddToClassList("translate-y");
        _controls.RemoveFromClassList("translate-y");
        _resumeButton.Focus();
      }
    }
  }
}