using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerAvatars;
    [SerializeField] private int _quantityCharacters;
    [SerializeField] private int _timeToReady;
    [SerializeField] private int _timeToMove;
    private List<CharacterStateController> _characters;
    private void Awake()
    {
        _characters = new List<CharacterStateController>();
        var gameManagers = GameObject.FindObjectsOfType<GameManager>();
        
        for (int i = 0; i < gameManagers.Length-1; i++)
        {
            Destroy(gameManagers[i].gameObject);
        }
    }

    public void AdCharacter(CharacterStateController character)
    {
        _characters.Add(character);
    }

    private void Start()
    {
        for (int i = 0; i < _quantityCharacters; i++)
        {
            AdCharacter(SetCharacter());

        }
        StartCoroutine(ChangeState());
    }

    private CharacterStateController SetCharacter()
    {
        var character = Instantiate(_playerAvatars[Random.Range(0, _playerAvatars.Length)], Vector3.zero, Quaternion.identity);
        character.AddComponent<PlayerController>().enabled=false;
        var characterController = character.GetComponent<CharacterStateController>();
        return characterController;
    }

    private IEnumerator ChangeState()
    {
        yield return StartCoroutine(OnGround());
        foreach (var character in _characters)
        {
            character.SetState(CharacterStateController.State.Idle);
        }
        yield return new WaitForSeconds(_timeToReady);
        foreach (var character in _characters)
        {
            character.SetState(CharacterStateController.State.Ready);
        }
        yield return new WaitForSeconds(_timeToMove);
        foreach (var character in _characters)
        {
            character.SetState(CharacterStateController.State.Move);
            character.GetComponent<Controller>().enabled = true;
        }
        //Time.timeScale = 0.1f;
    }

    private IEnumerator OnGround()
    {
        var characterControllers = new CharacterController[_characters.Count];
        for (int i = 0; i < _characters.Count; i++)
        {
            characterControllers[i] = _characters[i].GetComponent<CharacterController>();
            StartCoroutine(GroundInstallation(characterControllers[i]));
        }

        foreach (var character in characterControllers)
        {
            yield return new WaitUntil(() => (character.isGrounded));
        }
    }
    private IEnumerator GroundInstallation(CharacterController characterController)
    {
        while (!characterController.isGrounded)
        {
            characterController.SimpleMove(Vector3.zero);
            yield return null;
        }
    }
}
