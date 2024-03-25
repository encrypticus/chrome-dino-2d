using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class RetryButton : Button {
    public new class UxmlFactory : UxmlFactory<RetryButton, UxmlTraits> {}

    public RetryButton() {
      RemoveFromClassList("unity-button");
      styleSheets.Add(Resources.Load<StyleSheet>("RetryButton"));
      AddToClassList("retry-button");
    }
  }
}