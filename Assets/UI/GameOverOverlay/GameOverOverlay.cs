using System;
using DinoGame;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class GameOverOverlay : VisualElement {
    public new class UxmlFactory : UxmlFactory<GameOverOverlay, UxmlTraits> {
    }

    private RetryButton _retryButton;
    private Label _gameOverText;
    public RewardButton rewardButton { get; }

    public GameOverOverlay() {
    }

    public GameOverOverlay(SettingsManager.LanguageDictionary dict) {
      styleSheets.Add(Resources.Load<StyleSheet>("Common"));
      styleSheets.Add(Resources.Load<StyleSheet>("GameOverOverlay"));
      AddToClassList("game-over-overlay");

      _gameOverText = new Label() { text = dict.gameOver };
      _gameOverText.AddToClassList("game-over-label");
      _gameOverText.AddToClassList("typography");
      Add(_gameOverText);

      _retryButton = new RetryButton();
      _retryButton.Focus();
      _retryButton.clicked += OnRetry;
      Add(_retryButton);

      rewardButton = new RewardButton(dict);
      rewardButton.AddToClassList("reward-button");
    }

    public event Action OnRetryClicked;

    public void SetFocus() {
      _retryButton.Focus();
    }

    public void AddRewardButton() {
      Add(rewardButton);
    }

    public void RemoveRewardButton() {
      Remove(rewardButton);
    }

    public void ChangeLanguage(SettingsManager.LanguageDictionary dict) => _gameOverText.text = dict.gameOver;

    private void OnRetry() {
      OnRetryClicked?.Invoke();
    }
  }
}