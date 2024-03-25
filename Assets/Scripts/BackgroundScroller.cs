using UnityEngine;

namespace DinoGame {
  public class BackgroundScroller : MonoBehaviour {
    public MeshRenderer MeshRenderer;

    private void Awake() {
      MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update() {
      MeshRenderer.material.mainTextureOffset +=
        Vector2.right * GameManager.Instance.backgroundScrollSpeed * Time.deltaTime;
    }
  }
}