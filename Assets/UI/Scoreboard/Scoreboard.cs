using DinoGame;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class Scoreboard : VisualElement {
    public new class UxmlFactory : UxmlFactory<Scoreboard, UxmlTraits> {}

    private Label _hiScoreLabel;
    private Label _currentScoreLabel;
    private Label hi;
    
    public Scoreboard() {}

    public Scoreboard(SettingsManager.LanguageDictionary dict) {
      styleSheets.Add(Resources.Load<StyleSheet>("Scoreboard"));
      styleSheets.Add(Resources.Load<StyleSheet>("Common"));
      AddToClassList("scoreboard");
      AddToClassList("typography");

      hi = new Label() { text = dict.hi };
      Add(hi);

      _hiScoreLabel = new Label() { text = "00000" };
      _hiScoreLabel.AddToClassList("score-label");
      Add(_hiScoreLabel);
      
      _currentScoreLabel = new Label() { text = "00000" };
      _currentScoreLabel.AddToClassList("score-label");
      Add(_currentScoreLabel);
    }

    public void UpdateCurrentScore(float score) {
      _currentScoreLabel.text = Mathf.FloorToInt(score).ToString("D5");
    }

    public void UpdateHiScore(float score) {
      _hiScoreLabel.text = Mathf.FloorToInt(score).ToString("D5");
    }

    public void ChangeLanguage(SettingsManager.LanguageDictionary dict) {
      hi.text = dict.hi;
    }
  }
}