using System;
using DinoGame;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.CustomControls {
  public class PopupWindow : VisualElement {
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<PopupWindow> {
    }
    
    public CustomButton buttonCancel { get; }

    public PopupWindow() {
    }

    public PopupWindow(SettingsManager.LanguageDictionary dict) {
      styleSheets.Add(Resources.Load<StyleSheet>("Common"));
      styleSheets.Add(Resources.Load<StyleSheet>("PopupWindow"));
      
      AddToClassList("popup-window");
      AddToClassList("translate-y");
      
      Label title = new Label() { text = dict.leaveGame };
      title.AddToClassList("typography");
      title.AddToClassList("title");
      hierarchy.Add(title);

      VisualElement buttonsWrapper = new VisualElement();
      buttonsWrapper.AddToClassList("buttons-wrapper");
      hierarchy.Add(buttonsWrapper);

      CustomButton buttonOk = new CustomButton() { text = dict.yes };
      buttonCancel = new CustomButton() { text = dict.no };
      buttonOk.AddToClassList("button-small");
      buttonCancel.AddToClassList("button-small");
      
      buttonOk.AddToClassList("button-cancel");
      
      buttonOk.clicked += OnConfirm;
      buttonCancel.clicked += OnCancel;
      
      buttonsWrapper.Add(buttonOk);
      buttonsWrapper.Add(buttonCancel);
    }

    public event Action OkClicked;
    public event Action CancelClicked;

    public void SetFocus() {
      buttonCancel.Focus();
    }

    private void OnConfirm() {
      OkClicked?.Invoke();
    }

    private void OnCancel() {
      CancelClicked?.Invoke();
    }
  }
}