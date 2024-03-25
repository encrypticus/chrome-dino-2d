using UnityEngine;
using UnityEngine.UI;

namespace DinoGame {
  public class BackgroundScroller2 : MonoBehaviour {
    private RawImage _image;
    private float _offset;

    private void Awake() {
      _image = GetComponent<RawImage>();
    }

    private void Update() {
      _offset = GameManager.Instance.backgroundScrollSpeed * Time.deltaTime;
      _image.uvRect = new Rect(
        _image.uvRect.position + new Vector2(_offset, 0), _image.uvRect.size
      );
    }
  }
}