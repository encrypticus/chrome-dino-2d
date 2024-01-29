using System;
using UnityEngine;

namespace DinoGame {
  public class PauseMenu : MonoBehaviour {
    public GameObject pausePanel;

    public void Pause() {
      Time.timeScale = 0;
      pausePanel.SetActive(true);
      AudioListener.pause = true;
    }

    public void Continue() {
      pausePanel.SetActive(false);
      Time.timeScale = 1;
      AudioListener.pause = false;
    }
  }
}