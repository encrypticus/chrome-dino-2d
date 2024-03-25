using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class PauseButton : Button {
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<PauseButton, UxmlTraits> {}

    public PauseButton() {
      RemoveFromClassList("unity-button");
      styleSheets.Add(Resources.Load<StyleSheet>("PauseButton"));
      AddToClassList("pause-button");
    }
  }
}