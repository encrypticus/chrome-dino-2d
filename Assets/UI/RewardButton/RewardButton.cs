using DinoGame;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class RewardButton : VisualElement {
    public new class UxmlFactory : UxmlFactory<RewardButton, UxmlTraits> {}

    public Button button { get; }
    private Label _buttonText;
    private Label _watchAdsText;
    
    
    public RewardButton() {}

    public RewardButton(SettingsManager.LanguageDictionary dict) {
      styleSheets.Add(Resources.Load<StyleSheet>("RewardButton"));
      styleSheets.Add(Resources.Load<StyleSheet>("Common"));
      AddToClassList("reward");
      
      button = new Button { name = "rewardButton" };
      button.RemoveFromClassList("unity-button");
      VisualElement wrapper = new VisualElement { name = "wrapper" };
      VisualElement icon = new VisualElement { name = "icon" };
      _buttonText = new Label { name = "buttonLabel", text = dict.watchLabel};
      _buttonText.RemoveFromClassList("unity-label");
      wrapper.Add(icon);
      wrapper.Add(_buttonText);
      button.Add(wrapper);
      Add(button);

      _watchAdsText = new Label() {
        name = "rewardLabel",
        text = dict.watchAds
      };
      
      Add(_watchAdsText);
    }

    public void ChangeLanguage(SettingsManager.LanguageDictionary dict) {
      _buttonText.text = dict.watchLabel;
      _watchAdsText.text = dict.watchAds;
    }
  }
}