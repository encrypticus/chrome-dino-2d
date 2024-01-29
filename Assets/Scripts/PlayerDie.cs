using System;
using UnityEngine;

namespace DinoGame {
  public class PlayerDie : MonoBehaviour {
    public float gravity = 9.81f * 2f;
    [SerializeField] private Vector3 direction;
    private Transform _transform;

    private void Awake() {
      _transform = GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision other) {
      print(other.gameObject.name);
    }

    private void Update() {
      if (_transform.position.y <= 0.6f) {
        _transform.position = new Vector3(_transform.position.x, 0.6f, _transform.position.z);
      } else {
        _transform.position += new Vector3(1, -1, 0) * gravity * Time.deltaTime;
      }
    }
  }
}