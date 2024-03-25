using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class CustomButton : Button {
    public new class UxmlFactory : UxmlFactory<CustomButton, UxmlTraits> {}

    public CustomButton() {
      RemoveFromClassList("unity-button");
      styleSheets.Add(Resources.Load<StyleSheet>("CustomButton"));
      AddToClassList("button");
    }
  }
}