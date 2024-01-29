using UnityEngine;

namespace DinoGame {
  public class SettingsManager : MonoBehaviour {
    public static SettingsManager Instance { get; private set; }
    
    private void Awake() {
      if (Instance == null) {
        Instance = this;
      }
      else {
        DestroyImmediate(gameObject);
      }
    }

    private void OnDestroy() {
      if (Instance == this) {
        Instance = null;
      }
    }
    
    public float GetMusicVolume() {
      return PlayerPrefs.GetFloat("musicVolume", 1f);
    }

    public void SetMusicVolume(float volume) {
      PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public float GetSoundsVolume() {
      return PlayerPrefs.GetFloat("soundsVolume", 1f);
    }

    public void SetSoundsVolume(float volume) {
      PlayerPrefs.SetFloat("soundsVolume", volume);
    }
  }
}