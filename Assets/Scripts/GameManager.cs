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

    private Player _player;
    private Spawner _spawner;
    private float _score;

    private UIDocument _uiDocument;
    private VisualElement _root;
    private Label _currentScoreLabel;
    private Label _hiScoreLabel;
    private VisualElement _gameOverOverlay;
    private Button _retryButton;

    private void Awake() {
      if (Instance == null) {
        Instance = this;
      }
      else {
        DestroyImmediate(gameObject);
      }

      _uiDocument = GameObject.FindWithTag("GameUI").GetComponent<UIDocument>();
      _root = _uiDocument.rootVisualElement;
      _currentScoreLabel = _root.Q<Label>("CurrentScoreLabel");
      _hiScoreLabel = _root.Q<Label>("HiScoreLabel");
      _gameOverOverlay = _root.Q<VisualElement>("GameOverOverlay");
      _retryButton = _root.Q<Button>("RetryButton");
    }

    private void OnDestroy() {
      if (Instance == this) {
        Instance = null;
      }
    }

    private void Start() {
      _player = FindObjectOfType<Player>();
      _spawner = FindObjectOfType<Spawner>();
      _retryButton.clicked += () => NewGame();
      NewGame();
    }

    private void Update() {
      gameSpeed += gameSpeedIncrease * Time.deltaTime;
      _score += gameSpeed * Time.deltaTime;
      _currentScoreLabel.text = Mathf.FloorToInt(_score).ToString("D5");
    }

    public void NewGame() {
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

      gameSpeed = initialGameSpeed;
      backgroundScrollSpeed = initialBackgroundScrollSpeed;
      enabled = true;
      _score = 0f;

      _player.gameObject.SetActive(true);
      _spawner.gameObject.SetActive(true);
      _gameOverOverlay.style.display = DisplayStyle.None;
      SetSoundTrackVolume();
      SetSoundsVolume();

      UpdateHiScore();
    }

    public void SetSoundTrackVolume() {
      soundTrack.volume = SettingsManager.Instance.GetMusicVolume();
    }

    public void SetSoundsVolume() {
      foreach (var sound in sounds) {
        sound.volume = SettingsManager.Instance.GetSoundsVolume();
      }
    }

    public void GameOver() {
      gameSpeed = 0f;
      backgroundScrollSpeed = 0f;
      enabled = false;

      _player.gameObject.SetActive(false);
      _spawner.gameObject.SetActive(false);
      _gameOverOverlay.style.display = DisplayStyle.Flex;
      _retryButton.Focus();

      UpdateHiScore();
    }

    private void UpdateHiScore() {
      float hiScore = PlayerPrefs.GetFloat("hiScore", 0);

      if (_score > hiScore) {
        hiScore = _score;
        PlayerPrefs.SetFloat("hiScore", _score);
      }

      _hiScoreLabel.text = Mathf.FloorToInt(hiScore).ToString("D5");
    }
  }
}