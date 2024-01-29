using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DinoGame {
  public class Player : MonoBehaviour {
    private CharacterController _character;
    [SerializeField] private Vector3 direction;
    private PlayerInput _input;

    public GameObject PlayerDieEffectPrefab;
    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;
    public AudioSource PlayerJumpSound;

    private void Awake() {
      _character = GetComponent<CharacterController>();
      _input = new PlayerInput();
    }

    private void OnEnable() {
      direction = Vector3.zero;
      _input.Player.Enable();
    }

    private void OnDisable() {
      _input.Player.Disable();
    }

    private void Update() {
      var pressedValue = _input.Player.Jump.ReadValue<float>();
      direction += Vector3.down * gravity * Time.deltaTime;
      
      if (_character.isGrounded) {
        direction = Vector3.down;
      
        if (pressedValue > 0 && !EventSystem.current.IsPointerOverGameObject()) {
          direction = Vector3.up * jumpForce;
          PlayerJumpSound.Play();
        }
      }
      
      _character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Obstacle")) {
        PlayerDeathSpawner.Instance.PlayerDie(transform.position);
        Instantiate(PlayerDieEffectPrefab, other.gameObject.transform.position, Quaternion.identity);

        gameObject.SetActive(false);
        GameManager.Instance.GameOver();
      }
    }
  }
}