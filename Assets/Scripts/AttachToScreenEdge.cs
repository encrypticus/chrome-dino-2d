using UnityEngine;

public class AttachToScreenEdge : MonoBehaviour {
  public enum ScreenEdge {
    Top,
    Bottom,
    Left,
    Right
  }

  public ScreenEdge edge;
  public float offset;

  private void Awake() {
    UpdatePosition();
  }

  private void Start() {
    UpdatePosition();
  }

  private void OnEnable() {
    UpdatePosition();
  }

  void Update() {
    UpdatePosition();
  }

  private void UpdatePosition() {
    if (!gameObject.activeSelf) return;

    Vector3 newPosition = transform.position;

    switch (edge) {
      case ScreenEdge.Top:
        newPosition.y = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - offset;
        break;
      case ScreenEdge.Bottom:
        newPosition.y = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + offset;
        break;
      case ScreenEdge.Left:
        newPosition.x = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + offset;
        break;
      case ScreenEdge.Right:
        newPosition.x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - offset;
        break;
    }

    transform.position = newPosition;
  }
}