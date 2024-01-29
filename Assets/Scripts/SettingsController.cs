using UnityEngine;
using UnityEngine.UIElements;

namespace DinoGame {
  public class SettingsController : MonoBehaviour {
    private UIDocument _uiDocument;
    private VisualElement _root;

    private Slider _musicSlider;
    private Label _musicValue;
    private Slider _soundsSlider;
    private Label _soundsValue;

    private void Awake() {
      _uiDocument = GetComponent<UIDocument>();
      _root = _uiDocument.rootVisualElement;

      _musicSlider = _root.Q<Slider>("MusicSlider");
      _musicValue = _root.Q<Label>("MusicValue");
      _soundsSlider = _root.Q<Slider>("SoundsSlider");
      _soundsValue = _root.Q<Label>("SoundsValue");
    }

    private void Start() {
      var storedMusicVolume = SettingsManager.Instance.GetMusicVolume();
      _musicSlider.value = storedMusicVolume;
      _musicValue.text = Mathf.Ceil(storedMusicVolume * 10).ToString();

      var storedSoundsValue = SettingsManager.Instance.GetSoundsVolume();
      _soundsSlider.value = storedSoundsValue;
      _soundsValue.text = Mathf.Ceil(storedSoundsValue * 10).ToString();

      _musicSlider.RegisterValueChangedCallback(OnMusicSliderValueChange);
      _soundsSlider.RegisterValueChangedCallback(OnSoundsSliderValueChange);
    }

    private void OnMusicSliderValueChange(ChangeEvent<float> @event) {
      SettingsManager.Instance.SetMusicVolume(@event.newValue);
      _musicValue.text = Mathf.Ceil(@event.newValue * 10).ToString();
    }

    private void OnSoundsSliderValueChange(ChangeEvent<float> @event) {
      SettingsManager.Instance.SetSoundsVolume(@event.newValue);
      _soundsValue.text = Mathf.Ceil(@event.newValue * 10).ToString();
    }
  }
}