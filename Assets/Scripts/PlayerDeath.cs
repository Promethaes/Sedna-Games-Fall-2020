using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    [SerializeField] private PlayerManager _playerManager = null;
    private void OnCollisionEnter(Collision other) {
        if(!other.gameObject.TryGetComponent(out ObjectTags tags)) return;
        if(!tags.hasTag("Enemy")) return;
        
        _playerManager.players.Remove(gameObject);
        Destroy(gameObject);
    }
}
