using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStateMachine
{
    public enum State
    {
        Idle,
        Ready,
        Move,
        Jump
    }
    private State _currentState;
    private readonly Dictionary<State, CharacterControllerState> _listState;
    private readonly Dictionary<State, State> _listNextState;
    public UnityEvent<CharacterControllerState> NewCharacterControllerState = new UnityEvent<CharacterControllerState>();
    public CharacterStateMachine(GameObject character)
    {
        var characterStateIdle = new CharacterControllerStateIdle(character);
        characterStateIdle.CharacterStateEnd.AddListener(NextState);
        var characterStateReady = new CharacterControllerStateReady(character);
        characterStateReady.CharacterStateEnd.AddListener(NextState);
        var characterStateMove = new CharacterControllerStateMove(character);
        characterStateMove.CharacterStateEnd.AddListener(NextState);
        var characterStateJump = new CharacterControllerStateJump(character);
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
        _currentState = State.Idle;
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
        NewCharacterControllerState.Invoke(GetCurrentCharacterState());
    }
}
