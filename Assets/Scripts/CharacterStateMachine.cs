using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle,
        Ready,
        Move,
        Jump
    }
    private State _currentState;
    private CharacterControllerState _currentCharacterControllerState;
    private  Dictionary<State, CharacterControllerState> _listState;
    private  Dictionary<State, State> _listNextState;
    public UnityEvent<CharacterControllerState> NewCharacterControllerState = new UnityEvent<CharacterControllerState>();

    private void Awake()
    {
        var characterStateIdle = new CharacterControllerStateIdle(gameObject);
        characterStateIdle.CharacterStateEnd.AddListener(NextState);
        var characterStateReady = new CharacterControllerStateReady(gameObject);
        characterStateReady.CharacterStateEnd.AddListener(NextState);
        var characterStateMove = new CharacterControllerStateMove(gameObject);
        characterStateMove.CharacterStateEnd.AddListener(NextState);
        var characterStateJump = new CharacterControllerStateJump(gameObject);
        characterStateJump.CharacterStateEnd.AddListener(NextState);
        _listState = new Dictionary<State, CharacterControllerState>()
        {
            {State.Idle, characterStateIdle},
            {State.Ready, characterStateReady},
            {State.Move, characterStateMove},
            {State.Jump, characterStateJump}
        };

        _listNextState = new Dictionary<State, State>()
        {
         {State.Idle, State.Idle},
         {State.Ready, State.Ready},
         {State.Move, State.Move},
         {State.Jump, State.Move}
        };
        SetCurrentState(State.Idle);
        //_currentState = State.Idle;
        //_currentCharacterControllerState = GetCurrentCharacterState();


    }


    public void SetState(State state)
    {
        Debug.Log("CharacterStateMachine состояние " + state);
        if (_currentState != state)
        {
            SetCurrentState(state);
        }
    }
    private void NextState(CharacterControllerState characterState)
    {
        if (GetCurrentCharacterState() == characterState)
        {
            SetCurrentState(_listNextState[_currentState]);
        }
    }

    public CharacterControllerState GetCurrentCharacterState()
    {
        return _listState[_currentState];
    }

    private void SetCurrentState(State state)
    {
        _currentState = state;
        _currentCharacterControllerState = GetCurrentCharacterState();
        NewCharacterControllerState.Invoke(GetCurrentCharacterState());
    }
    private void Start()
    {
        _currentCharacterControllerState.StartState();
    }

    private void Update()
    {
        _currentCharacterControllerState.UpdateState();
    }
}
