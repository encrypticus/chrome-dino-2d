using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class LoadingProgress : ProgressBar {
    public new class UxmlFactory : UxmlFactory<LoadingProgress, UxmlTraits> {}

    private Label _progressLabel;

    public LoadingProgress() {
      styleSheets.Add(Resources.Load<StyleSheet>("LoadingProgress"));
      styleSheets.Add(Resources.Load<StyleSheet>("Common"));
      AddToClassList("loading-progress");
      AddToClassList("typography");
      AddToClassList("hidden");

      lowValue = 0f;
      highValue = 1f;
      value = 0f;

      _progressLabel = this.Q<Label>();
      _progressLabel.text = "0%";
    }

    public void UpdateProgressValue(float progressValue) {
      value = progressValue;
      _progressLabel.text = $"{progressValue * 100}%";
    }

    public void Show() {
      RemoveFromClassList("hidden");
    }
  }
}