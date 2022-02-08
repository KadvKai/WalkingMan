using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerAvatars;
    [SerializeField] private int _quantityCharacters;
    [SerializeField] private int _timeToReady;
    [SerializeField] private int _timeToMove;
    CharacterController[] _characters;
    private void Awake()
    {
        _characters = new CharacterController[_quantityCharacters];
        for (int i = 0; i < _quantityCharacters; i++)
        {
            _characters[i]=SetCharacter();

        }
    }

    private void Start()
    {
        StartCoroutine(ChangeState());
    }

    private CharacterController SetCharacter()
    {
        var character = Instantiate(_playerAvatars[Random.Range(0, _playerAvatars.Length)]);
        character.AddComponent<PlayerController>();
        var characterController = character.GetComponent<CharacterController>();
        return characterController;
    }

    private IEnumerator ChangeState()
    {
        foreach (var character in _characters)
        {
            character.SetState(CharacterController.State.Idle);
        }
        yield return new WaitForSeconds(_timeToReady);
        foreach (var character in _characters)
        {
            character.SetState(CharacterController.State.Ready);
        }
        yield return new WaitForSeconds(_timeToMove);
        foreach (var character in _characters)
        {
            character.SetState(CharacterController.State.Move);
        }
    }

}
