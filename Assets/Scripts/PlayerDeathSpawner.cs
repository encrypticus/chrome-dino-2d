using System;
using UnityEngine;

namespace DinoGame {
  public class PlayerDeathSpawner : MonoBehaviour {
    public GameObject playerDiePrefab;
    public GameObject playerDieEffectPrefab;
    public static PlayerDeathSpawner Instance;

    private void Start() {
      Instance = this;
    }

    public void PlayerDie(Vector3 position) {
      Instantiate(playerDiePrefab, position, Quaternion.identity);
    }
  }
}