using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerAvatars;
    [SerializeField] private int _quantityCharacters;
    [SerializeField] private int _timeToReady;
    [SerializeField] private int _timeToMove;
    CharacterStateMachine[] _characters;
    private void Awake()
    {
        _characters = new CharacterStateMachine[_quantityCharacters];
        for (int i = 0; i < _quantityCharacters; i++)
        {
            _characters[i]=SetCharacter();

        }
    }

    private void Start()
    {
        StartCoroutine(ChangeState());
    }

    private CharacterStateMachine SetCharacter()
    {
        var character = Instantiate(_playerAvatars[Random.Range(0, _playerAvatars.Length)]);
        var playerController=character.AddComponent<PlayerController>();
        var characterStateMachine = character.GetComponent<CharacterStateMachine>(); ;
        playerController.SetCharacterStateMachine(characterStateMachine);
        return characterStateMachine;
    }

    private IEnumerator ChangeState()
    {
        foreach (var character in _characters)
        {
            character.SetState(CharacterStateMachine.State.Idle);
        }
        yield return new WaitForSeconds(_timeToReady);
        foreach (var character in _characters)
        {
            character.SetState(CharacterStateMachine.State.Ready);
        }
        yield return new WaitForSeconds(_timeToMove);
        foreach (var character in _characters)
        {
            character.SetState(CharacterStateMachine.State.Move);
        }
    }

}
