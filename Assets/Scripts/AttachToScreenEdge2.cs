using UnityEngine;

public class AttachToScreenEdge2 : MonoBehaviour {
  public float horizontalOffsetPercentage = 0.5f; // Процентное смещение по горизонтали от левого края экрана
  public float verticalOffsetPercentage = 0.5f; // Процентное смещение по вертикали от нижнего края экрана

  private Vector3 initialPosition; // Начальная позиция игрока относительно левого нижнего угла экрана

  private void Start() {
    // Получаем координаты левого нижнего угла экрана в мировых координатах
    Vector3 bottomLeftCorner = Camera.main.ViewportToScreenPoint(Vector3.zero);

    // Вычисляем начальную позицию игрока относительно левого нижнего угла экрана
    initialPosition = new Vector3(
      bottomLeftCorner.x + horizontalOffsetPercentage * Camera.main.pixelWidth / Screen.width,
      bottomLeftCorner.y + verticalOffsetPercentage * Camera.main.pixelHeight / Screen.height,
      transform.position.z
    );

    // Устанавливаем начальную позицию игрока
    transform.position = initialPosition;
  }

  private void Update() {
    // Пересчитываем позицию игрока при изменении размера экрана
    if (Screen.width != Screen.safeArea.width || Screen.height != Screen.safeArea.height) {
      Vector3 bottomLeftCorner = Camera.main.ViewportToScreenPoint(Vector3.zero);
      transform.position = new Vector3(
        bottomLeftCorner.x + horizontalOffsetPercentage * Camera.main.pixelWidth / Screen.width,
        bottomLeftCorner.y + verticalOffsetPercentage * Camera.main.pixelHeight / Screen.height,
        transform.position.z
      );
    }
  }
}