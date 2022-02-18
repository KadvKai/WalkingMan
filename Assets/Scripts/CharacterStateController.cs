using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStateController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Ready,
        Move,
        Jump,
        FreeFall
    }
    private State _currentState;
    private CharacterState _currentCharacterState;
    private Dictionary<State, CharacterState> _listState;

    private void Start()
    {
        var characterStateIdle = new CharacterStateIdle(gameObject);
        var characterStateReady = new CharacterStateReady(gameObject);
        var characterStateMove = new CharacterStateMove(gameObject);
        characterStateMove.CharacterStateEnd.AddListener(NextState);
        var characterStateJump = new CharacterStateJump(gameObject);
        characterStateJump.CharacterStateEnd.AddListener(NextState);
        var characterStateFreeFall = new CharacterStateFreeFall(gameObject);
        characterStateFreeFall.CharacterStateEnd.AddListener(NextState);
        _listState = new Dictionary<State, CharacterState>()
        {
            {State.Idle, characterStateIdle},
            {State.Ready, characterStateReady},
            {State.Move, characterStateMove},
            {State.Jump, characterStateJump},
            {State.FreeFall, characterStateFreeFall}
        };

        _currentState = State.Idle;
        _currentCharacterState = _listState[_currentState];
    }

    public void SetState(State state)
    {
        _currentCharacterState.StateEnd();
        Debug.Log("CharacterStateMachine состояние " + state);
        if (_currentState != state)
        {
            _currentState = state;
            _currentCharacterState = _listState[_currentState];
            _currentCharacterState.StateStart();
        }
    }

    private void NextState(CharacterState currentState, CharacterStateController.State nextState)
    {
        if (_currentCharacterState == currentState)
        {

            SetState(nextState);
        }
    }

    private void Update()
    {
        _currentCharacterState.StateUpdate();
        //Debug.Log("deltaTime=" + Time.deltaTime);
    }

}
