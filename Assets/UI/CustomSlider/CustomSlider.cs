using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class CustomSlider : Slider {
    public new class UxmlFactory : UxmlFactory<CustomSlider, UxmlTraits> {}

    public CustomSlider() {
      styleSheets.Add(Resources.Load<StyleSheet>("CustomSlider"));
      AddToClassList("custom-slider");
    }
  }
}