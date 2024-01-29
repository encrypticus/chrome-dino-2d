using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DinoGame {
  public class MainMenu : MonoBehaviour {
    public GameObject progress;
    public Slider loadingSlider;
    public Slider musicSlider;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public TextMeshProUGUI musicSliderValueText;
    public void PlayGame() {
      StartCoroutine(LoadGameAsync());
    }

    public void ShowMainMenu() {
      settingsMenu.SetActive(false);
      mainMenu.SetActive(true);
    }

    public void ShowSettingsMenu() {
      mainMenu.SetActive(false);
      settingsMenu.SetActive(true);
    }

    public void OnChangeMusicVolume(float value) {
      musicSlider.value = value;
      PlayerPrefs.SetFloat("musicVolume", value);
      musicSliderValueText.text = Mathf.Ceil((value * 10)).ToString();
    }

    private void Awake() {
      float storedValue = PlayerPrefs.GetFloat("musicVolume", 0.5f);
      progress.SetActive(false);
      settingsMenu.SetActive(false);
      musicSlider.value = storedValue;
      musicSliderValueText.text = Mathf.Ceil((storedValue * 10)).ToString();
    }

    IEnumerator LoadGameAsync() {
      AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
      progress.SetActive(true);

      while (!loadOperation.isDone) {
        float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
        loadingSlider.value = progressValue;
        yield return null;
      }
    }
  }
}