using UnityEngine;

namespace DinoGame {
  public class Obstacle : MonoBehaviour {
    private float leftEdge;

    private void Start() {
      leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 3f;
    }

    private void Update() {
      var position = Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;
      transform.position += position;

      if (transform.position.x < leftEdge) Destroy(gameObject);
    }
  }
}