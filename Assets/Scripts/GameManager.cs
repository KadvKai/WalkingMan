using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerAvatars;
    [SerializeField] private int _quantityCharacters;
    [SerializeField] private int _timeToReady;
    [SerializeField] private int _timeToMove;
    private CharacterStateController.State _currentState;
    public static GameManager GameManagerStatic { get; private set; }
    private List<CharacterStateController> _characters;
    private void Awake()
    {
        GameManagerStatic = this;
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
        character.SetState(_currentState);
    }

    private void Start()
    {
        for (int i = 0; i < _quantityCharacters; i++)
        {
            //AdCharacter(SetCharacter());
            SetCharacter();
        }
        StartCoroutine(ChangeState());
    }

    private void SetCharacter()
    {
        Instantiate(_playerAvatars[Random.Range(0, _playerAvatars.Length)], Vector3.zero, Quaternion.identity);
        //var character = Instantiate(_playerAvatars[Random.Range(0, _playerAvatars.Length)], Vector3.zero, Quaternion.identity);
        //character.GetComponent<PlayerController>().enabled = false;
    }

    private IEnumerator ChangeState()
    {
        yield return StartCoroutine(OnGround());
            _currentState = CharacterStateController.State.Idle;
            Debug.Log("State.Idle");
        foreach (var character in _characters)
        {
            character.SetState(_currentState);
        }
        yield return new WaitForSeconds(_timeToReady);
            _currentState = CharacterStateController.State.Ready;
            Debug.Log("State.Ready");
        foreach (var character in _characters)
        {
            character.SetState(_currentState);
        }
        yield return new WaitForSeconds(_timeToMove);
            _currentState = CharacterStateController.State.Move;
            Debug.Log("State.Move");
        foreach (var character in _characters)
        {
            character.SetState(_currentState);
            //character.GetComponent<Controller>().enabled = true;
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
