using System;
using DinoGame;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class MainMenu : VisualElement {
    public new class UxmlFactory : UxmlFactory<MainMenu, UxmlTraits> {
    }

    private SettingsManager.LanguageDictionary _dict;
    private string _languageKey;

    // Class names
    private const string StyleSheetMainMenu = "MainMenu";
    private const string StyleSheetCommon = "Common";
    private const string ClassNameRoot = "root";
    private const string ClassNameMainMenu = "main-menu";
    private const string ClassNameControls = "controls";
    private const string ClassNameHidden = "hidden";
    private const string ClassNameSliderInfo = "slider-info";
    private const string ClassNameSliderWrapper = "slider-wrapper";

    // Wrappers
    private VisualElement _mainControls;
    private VisualElement _settingsControls;
    private VisualElement _mainMenu;
    private VisualElement _musicSliderWrapper;
    private VisualElement _musicSliderInfo;
    private VisualElement _soundSliderWrapper;
    private VisualElement _soundSliderInfo;

    // Buttons
    private CustomButton _playButton;
    private CustomButton _settingsButton;
    private CustomButton _quitButton;
    private CustomButton _backButton;

    // Sliders
    private Label _musicVolumeValueLabel;
    private Label _soundsVolumeValueLabel;
    private Label _musicLabel;
    private Label _soundLabel;
    public CustomSlider musicSlider { get; private set; }
    public CustomSlider soundsSlider { get; private set; }
    private LoadingProgress _loadingProgress;
    
    public SelectLanguage selectLanguage { get; private set; }

    public MainMenu() {
    }

    public MainMenu(SettingsManager.LanguageDictionary dict, string languageKey) {
      _dict = dict;
      _languageKey = languageKey;
      styleSheets.Add(Resources.Load<StyleSheet>(StyleSheetMainMenu));
      styleSheets.Add(Resources.Load<StyleSheet>(StyleSheetCommon));
      AddToClassList(ClassNameRoot);
      
      InitSelectLanguage();
      InitButtons();
      InitMainMenu();
      InitMainControls();
      InitSettingsControls();
      InitMusicSlider();
      InitSoundsSlider();

      _loadingProgress = new LoadingProgress();
      Add(_loadingProgress);
      _playButton.Focus();

      _playButton.clicked += OnPlay;
      _settingsButton.clicked += OnSettings;
      _backButton.clicked += OnBack;
      _quitButton.clicked += OnQuit;
    }

    private void InitSelectLanguage() {
      selectLanguage = new SelectLanguage(_dict) {
        customDropdown = { value = _languageKey }
      };
      Add(selectLanguage);
    }

    private void InitButtons() {
      _playButton = new CustomButton() { text = _dict.play, name = "playButton" };
      _settingsButton = new CustomButton() { text = _dict.settings, name = "settingsButton" };
      _quitButton = new CustomButton() { text = _dict.quit, name = "quitButton" };
      _backButton = new CustomButton { text = _dict.back, name = "backButton" };
    }

    private void InitMainMenu() {
      _mainMenu = new VisualElement();
      _mainMenu.AddToClassList(ClassNameMainMenu);
      _mainMenu.name = "mainMenu";
      Add(_mainMenu);
    }

    private void InitMainControls() {
      _mainControls = new VisualElement() { name = "mainControls" };
      _mainControls.AddToClassList(ClassNameControls);
      _mainMenu.Add(_mainControls);
      _mainControls.Add(_playButton);
      _mainControls.Add(_settingsButton);
      if (!IsBrowser()) {
        _mainControls.Add(_quitButton);
      }
    }

    private void InitSettingsControls() {
      _settingsControls = new VisualElement() { name = "settingsControls" };
      _settingsControls.AddToClassList(ClassNameControls);
      _settingsControls.AddToClassList(ClassNameHidden);
      _mainMenu.Add(_settingsControls);
      _settingsControls.Add(_backButton);
    }

    private void InitMusicSlider() {
      _musicSliderWrapper = new VisualElement() { name = "musicSliderWrapper" };
      _musicSliderWrapper.AddToClassList(ClassNameSliderWrapper);
      _settingsControls.Add(_musicSliderWrapper);
      _musicSliderInfo = new VisualElement() { name = "musicSliderInfo" };
      _musicSliderInfo.AddToClassList(ClassNameSliderInfo);

      var storedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
      musicSlider = new CustomSlider() {
        name = "musicSlider",
        lowValue = 0f,
        highValue = 1f,
        value = storedMusicVolume
      };

      _musicSliderWrapper.Add(_musicSliderInfo);
      _musicSliderWrapper.Add(musicSlider);
      _musicLabel = new Label {
        text = _dict.music,
        name = "musicLabel"
      };
      _musicLabel.AddToClassList("soundControlsLabel");
      _musicVolumeValueLabel = new Label() {
        text = Mathf.Ceil(storedMusicVolume * 10).ToString(),
        name = "musicVolumeValueLabel"
      };
      _musicVolumeValueLabel.AddToClassList("soundControlsLabel");
      _musicSliderInfo.Add(_musicLabel);
      _musicSliderInfo.Add(_musicVolumeValueLabel);
    }

    private void InitSoundsSlider() {
      _soundSliderWrapper = new VisualElement() { name = "soundSliderWrapper" };
      _soundSliderWrapper.AddToClassList(ClassNameSliderWrapper);
      _settingsControls.Add(_soundSliderWrapper);
      _soundSliderInfo = new VisualElement() { name = "soundSliderInfo" };
      _soundSliderInfo.AddToClassList(ClassNameSliderInfo);

      var storedSoundsVolume = PlayerPrefs.GetFloat("soundsVolume", 1f);
      soundsSlider = new CustomSlider() {
        name = "soundsSlider",
        lowValue = 0f,
        highValue = 1f,
        value = storedSoundsVolume
      };

      _soundSliderWrapper.Add(_soundSliderInfo);
      _soundSliderWrapper.Add(soundsSlider);
      _soundLabel = new Label {
        text = _dict.sounds,
        name = "soundLabel"
      };
      _soundLabel.AddToClassList("soundControlsLabel");
      _soundsVolumeValueLabel = new Label() {
        text = Mathf.Ceil(storedSoundsVolume * 10).ToString(),
        name = "soundsVolumeValueLabel"
      };
      _soundsVolumeValueLabel.AddToClassList("soundControlsLabel");
      _soundSliderInfo.Add(_soundLabel);
      _soundSliderInfo.Add(_soundsVolumeValueLabel);
    }

    public void ShowMainControls() {
      _mainControls.RemoveFromClassList(ClassNameHidden);
      _settingsControls.AddToClassList(ClassNameHidden);
      _playButton.Focus();
    }

    public void ShowSettingsControls() {
      _settingsControls.RemoveFromClassList(ClassNameHidden);
      _mainControls.AddToClassList(ClassNameHidden);
      _backButton.Focus();
    }

    public void ChangeLanguage(SettingsManager.LanguageDictionary dict) {
      _playButton.text = dict.play;
      _settingsButton.text = dict.settings;
      _backButton.text = dict.back;
      _musicLabel.text = dict.music;
      _soundLabel.text = dict.sounds;
      _quitButton.text = dict.quit;
    }

    public event Action PlayClicked;
    public event Action SettingsClicked;
    public event Action BackClicked;
    public event Action QuitClicked;

    public void ChangeMusicVolume(float value) {
      _musicVolumeValueLabel.text = Mathf.Ceil(value * 10).ToString();
    }

    public void ChangeSoundsVolume(float value) {
      _soundsVolumeValueLabel.text = Mathf.Ceil(value * 10).ToString();
    }

    public void ShowLoadingProgress() {
      _loadingProgress.Show();
    }

    public void UpdateLoadingProgressValue(float progressValue) {
      _loadingProgress.UpdateProgressValue(progressValue);
    }

    public void SetPlayButtonFocus() {
      _playButton.Focus();
    }

    private void OnPlay() {
      PlayClicked?.Invoke();
    }

    private void OnSettings() {
      SettingsClicked?.Invoke();
    }

    private void OnBack() {
      BackClicked?.Invoke();
    }

    private void OnQuit() {
      QuitClicked?.Invoke();
    }

    private bool IsBrowser() {
#if !UNITY_EDITOR && UNITY_WEBGL
        return true;
#endif
      return false;
    }
  }
}