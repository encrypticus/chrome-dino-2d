using UnityEngine;

namespace DinoGame {
  public class AnimatedPlayerSprite : AnimatedSprite {
    public Sprite spriteForJump;
    public CharacterController characterController;

    protected override void Animate() {
      FrameRun++;

      if (FrameRun >= spritesForRun.Length) {
        FrameRun = 0;
      }

      if (!characterController.isGrounded) {
        SpriteRenderer.sprite = spriteForJump;
      } else {
        if (FrameRun >= 0 && FrameRun < spritesForRun.Length) {
          SpriteRenderer.sprite = spritesForRun[FrameRun];
        }
      }

      Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
  }
}