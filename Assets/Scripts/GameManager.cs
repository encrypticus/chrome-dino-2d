using System.Runtime.InteropServices;
using UI.CustomControls;
using UnityEngine;
using UnityEngine.UIElements;

namespace DinoGame {
  public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float initialBackgroundScrollSpeed = 0.1f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }
    public float backgroundScrollSpeed { get; private set; }
    public AudioSource soundTrack;
    public AudioSource[] sounds;
    public bool IsGameOver { get; private set; }

    private Player _player;
    private Spawner _spawner;
    private float _score;
    private float _scoreAccum;
    private float _gameSpeedAccum;
    public bool IsPlayerLive => _player.IsAlive;

    private bool _isRewardReceived;

    private UIDocument _uiDocument;
    private VisualElement _root;
    private GameOverOverlay _gameOverOverlay;
    private Scoreboard _scoreboard;

    private void Awake() {
      if (Instance == null) {
        Instance = this;
      }
      else {
        DestroyImmediate(gameObject);
      }

      _uiDocument = GameObject.FindWithTag("GameUI").GetComponent<UIDocument>();
      _root = _uiDocument.rootVisualElement;
      _gameOverOverlay = new GameOverOverlay(SettingsManager.Instance.GetLanguageDictionary());
      _scoreboard = new Scoreboard(SettingsManager.Instance.GetLanguageDictionary());
      _root.Add(_scoreboard);

      if (SettingsManager.Instance.IsBrowser()) {
        _gameOverOverlay.AddRewardButton();
        _gameOverOverlay.rewardButton.button.clicked += () => ShowRewardAdv();
        SettingsManager.Instance.OnGetLangFromYandex += langDict => {
          _scoreboard.ChangeLanguage(langDict);
          _gameOverOverlay.rewardButton.ChangeLanguage(langDict);
          _gameOverOverlay.ChangeLanguage(langDict);
        };
      }
    }

    private void OnDestroy() {
      if (Instance == this) {
        Instance = null;
      }
    }

    private void Start() {
      _player = FindObjectOfType<Player>();
      _spawner = FindObjectOfType<Spawner>();
      _gameOverOverlay.OnRetryClicked += () => NewGame();

      NewGame();
    }

    private void Update() {
      gameSpeed += gameSpeedIncrease * Time.deltaTime;
      _gameSpeedAccum += gameSpeedIncrease * Time.deltaTime;
      _score += gameSpeed * Time.deltaTime;
      _scoreAccum += _gameSpeedAccum * Time.deltaTime;
      _scoreboard.UpdateCurrentScore(_score);
    }

    public void NewGame() {
      enabled = true;
      Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
      GameObject[] playerDie = GameObject.FindGameObjectsWithTag("PlayerDie");

      foreach (var obstacle in obstacles) {
        Destroy(obstacle.gameObject);
      }

      if (playerDie != null) {
        foreach (var obj in playerDie) {
          Destroy(obj.gameObject);
        }
      }
      
      backgroundScrollSpeed = initialBackgroundScrollSpeed;
      
      if (!_isRewardReceived) {
        _score = 0f;
        _scoreAccum = 0f;
        gameSpeed = initialGameSpeed;
        _gameSpeedAccum = initialGameSpeed;
      }
      else {
        _score = _scoreAccum;
        gameSpeed = _gameSpeedAccum;
        _isRewardReceived = false;
        _gameOverOverlay.AddRewardButton();
      }

      _player.IsAlive = true;
      _spawner.gameObject.SetActive(true);

      SetSoundTrackVolume();
      SetSoundsVolume();
      if (_gameOverOverlay != null) {
        _gameOverOverlay.RemoveFromHierarchy();
      }

      IsGameOver = false;
      AudioListener.pause = false;

      UpdateHiScore();
    }

    public void ReceiveReward() {
      _isRewardReceived = true;
      _gameOverOverlay.RemoveRewardButton();
    }

    public void SetSoundTrackVolume() {
      soundTrack.volume = SettingsManager.Instance.GetMusicVolume();
    }

    public void SetSoundsVolume() {
      foreach (var sound in sounds) {
        sound.volume = SettingsManager.Instance.GetSoundsVolume();
      }
    }

    [DllImport("__Internal")]
    private static extern void ShowFullScreenAdv();

    [DllImport("__Internal")]
    private static extern void ShowRewardAdv();

    [DllImport("__Internal")]
    private static extern void SaveScoreOnLeaderboard(int score);

    private void ShowAdvAndMute() {
      AudioListener.pause = true;
      ShowFullScreenAdv();
    }

    public void GameOver() {
      gameSpeed = 0f;
      backgroundScrollSpeed = 0f;
      
      _spawner.gameObject.SetActive(false);
      _root.Add(_gameOverOverlay);
      _gameOverOverlay.SetFocus();
      IsGameOver = true;
      UpdateHiScore();

      if (SettingsManager.Instance.IsBrowser()) {
        ShowAdvAndMute();
      }
      enabled = false;
    }

    private void UpdateHiScore() {
      float hiScore = PlayerPrefs.GetFloat("hiScore", 0);

      if (_score > hiScore) {
        hiScore = _score;
        PlayerPrefs.SetFloat("hiScore", _score);
        if (SettingsManager.Instance.IsBrowser()) {
          SaveScoreOnLeaderboard((int)_score);
        }
      }

      _scoreboard.UpdateHiScore(hiScore);
    }
  }
}