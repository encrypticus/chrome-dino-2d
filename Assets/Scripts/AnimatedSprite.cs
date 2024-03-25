using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DinoGame {
  public class AnimatedSprite : MonoBehaviour {
    public Sprite[] spritesForRun;

    protected SpriteRenderer SpriteRenderer;
    protected int FrameRun;

    private void Awake() {
      SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
      Invoke(nameof(Animate), 0f);
    }

    private void OnDisable() {
      CancelInvoke();
    }

    protected virtual void Animate() {
      FrameRun++;
      if (FrameRun >= spritesForRun.Length) {
        FrameRun = 0;
      }
      
      if (FrameRun >= 0 && FrameRun < spritesForRun.Length) {
        SpriteRenderer.sprite = spritesForRun[FrameRun];
      }

      Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
  }
}