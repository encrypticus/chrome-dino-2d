using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class CustomDropdown : DropdownField {
    public new class UxmlFactory : UxmlFactory<CustomDropdown, UxmlTraits> {
    }

    public CustomDropdown() {
      styleSheets.Add(Resources.Load<StyleSheet>("CustomDropdown"));
      styleSheets.Add(Resources.Load<StyleSheet>("Common"));
      AddToClassList("custom-dropdown");
      AddToClassList("typography");
    }
  }
}