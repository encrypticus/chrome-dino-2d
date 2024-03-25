using System;
using UnityEngine;
using UnityEngine.UI;

namespace DinoGame {
  public class Ground2 : MonoBehaviour {
    private RawImage _image;

    private void Awake() {
      _image = GetComponent<RawImage>();
    }

    private void Update() {
      float speed = GameManager.Instance.gameSpeed / 27;
      _image.uvRect = new Rect(
        _image.uvRect.position + new Vector2(speed * Time.deltaTime, 0), _image.uvRect.size
      );
    }
  }
}