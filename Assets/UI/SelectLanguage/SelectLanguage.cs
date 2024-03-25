using System.Collections.Generic;
using DinoGame;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class SelectLanguage : VisualElement {
    public new class UxmlFactory : UxmlFactory<SelectLanguage, UxmlTraits> {}
    
    public CustomDropdown customDropdown { get; private set; }

    public SelectLanguage() {}

    public SelectLanguage(SettingsManager.LanguageDictionary dict) {
      styleSheets.Add(Resources.Load<StyleSheet>("SelectLanguage"));
      styleSheets.Add(Resources.Load<StyleSheet>("Common"));
      AddToClassList("select-language");

      Label label = new() { text = dict.lang, name = "label" };
      label.AddToClassList("soundControlsLabel");
      customDropdown = new() { choices = new List<string>() { "RU", "EN" }};
      
      Add(label);
      Add(customDropdown);
    }
  }
}