using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _animatorController;   
    [SerializeField] private Rigidbody [] _playerAvatars;
    private Rigidbody _player;
    private void Awake()
    {
        _player=Instantiate(_playerAvatars[Random.Range(0, _playerAvatars.Length)]);
        _player.GetComponent<Animator>().runtimeAnimatorController = _animatorController;
        _player.gameObject.AddComponent<PlayerController>();
    }

}
