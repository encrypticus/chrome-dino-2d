using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DinoGame {
  public class GameUI : MonoBehaviour {
    private UIDocument _uiDocument;
    private VisualElement _root;
    private Button _pauseButton;
    private Button _resumeButton;
    private VisualElement _pauseOverlay;
    
    private PlayerInput _input;

    private void Awake() {
      _input = new PlayerInput();
      _uiDocument = GetComponent<UIDocument>();
      _root = _uiDocument.rootVisualElement;

      _pauseButton = _root.Q<Button>("PauseButton");
      _resumeButton = _root.Q<Button>("ResumeButton");
      _pauseOverlay = _root.Q<VisualElement>("PauseOverlay");
    }

    private void OnEnable() {
      _input.Player.Enable();
    }

    private void OnDisable() {
      _input.Player.Disable();
    }

    private void Start() {
      
      _pauseButton.clicked += () => ShowPauseOverlay(true);
      _resumeButton.clicked += () => ShowPauseOverlay(false);
      _input.Player.Pause.performed += _ => {
        ShowPauseOverlay(true);
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
  }
}