using UnityEngine;
using UnityEngine.EventSystems;

namespace DinoGame {
  [RequireComponent(typeof(AttachToScreenEdge))]
  public class Player : MonoBehaviour {
    private CharacterController _character;
    [SerializeField] private Vector3 direction;
    private PlayerInput _input;
    private Animator _animator;

    public GameObject PlayerDieEffectPrefab;
    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;
    public AudioSource PlayerJumpSound;

    private bool _isAlive = true;

    public bool IsAlive {
      get => _isAlive;
      set {
        _isAlive = value;
        _animator.SetBool(AnimationStrings.isAlive, value);
      }
    }

    private void Awake() {
      _character = GetComponent<CharacterController>();
      _input = new PlayerInput();
      _animator = GetComponent<Animator>();
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

        if (pressedValue > 0 && !EventSystem.current.IsPointerOverGameObject() && IsAlive) {
          direction = Vector3.up * jumpForce;
          PlayerJumpSound.Play();
        }

        _animator.SetBool(AnimationStrings.isGrounded, true);
      }
      else {
        _animator.SetBool(AnimationStrings.isGrounded, false);
      }

      _character.Move(direction * Time.deltaTime);
      _animator.SetFloat(AnimationStrings.walkSpeed, GameManager.Instance.gameSpeed / 7f);
    }

    private void GameOver() {
      GameManager.Instance.GameOver();
    }

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Obstacle") && IsAlive) {
        IsAlive = false;
        Instantiate(PlayerDieEffectPrefab, other.gameObject.transform.position, Quaternion.identity);
        Invoke(nameof(GameOver), time: 0.7f);
      }
    }
  }
}