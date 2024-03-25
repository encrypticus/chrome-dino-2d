using UnityEngine;

namespace DinoGame {
  public class PlayerDie : MonoBehaviour {
    public float gravity = 9.81f * 2f;
    private Transform _transform;

    private void Awake() {
      _transform = GetComponent<Transform>();
    }

    private void Update() {
      Vector3 newPosition = transform.position;
      newPosition.x = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 1.5f;
      if (_transform.position.y <= 0.6f) {
        _transform.position = new Vector3(newPosition.x, 0.6f, _transform.position.z);
      }
      else {
        _transform.position += new Vector3(1, -1, 0) * gravity * Time.deltaTime;
      }
    }
  }
}