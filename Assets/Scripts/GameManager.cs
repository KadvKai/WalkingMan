using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController _animatorController;   
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Animator _player;
    void Start()
    {
        _player=Instantiate(_playerAnimator);
        _player.runtimeAnimatorController = _animatorController;
    }

}
