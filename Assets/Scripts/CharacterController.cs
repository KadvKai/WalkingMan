using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Ready,
        Move,
        Jump
    }
    private State _currentState;
    private CharacterState _currentCharacterState;
    private  Dictionary<State, CharacterState> _listState;
    private  Dictionary<State, State> _listNextState;
    //private readonly Controller _controller;
    //public UnityEvent<CharacterState> NewCharacterControllerState = new UnityEvent<CharacterState>();

    private void Start()
    {
        var characterStateIdle = new CharacterStateIdle(gameObject);
        //characterStateIdle.CharacterStateEnd.AddListener(NextState);
        var characterStateReady = new CharacterStateReady(gameObject);
        //characterStateReady.CharacterStateEnd.AddListener(NextState);
        var characterStateMove = new CharacterStateMove(gameObject);
        //characterStateMove.CharacterStateEnd.AddListener(NextState);
        var characterStateJump = new CharacterStateJump(gameObject);
        characterStateJump.CharacterStateEnd.AddListener(NextState);
        _listState = new Dictionary<State, CharacterState>()
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
        _currentCharacterState = _listState[_currentState];
        //SetCurrentState(State.Idle);
    }

    public void SetState(State state)
    {
        Debug.Log("CharacterStateMachine состояние " + state);
        if (_currentState != state)
        {
            //SetCurrentState(state);
            _currentState = state;
            _currentCharacterState = _listState[_currentState];
            _currentCharacterState.StartState();
            //NewCharacterControllerState.Invoke(_currentCharacterState);
        }
    }

    private void NextState(CharacterState characterState)
    {
        if (_currentCharacterState==characterState )
        {
            SetState(_listNextState[_currentState]);
        }
    }

    private void Update()
    {
        _currentCharacterState.UpdateState();
        //Debug.Log("deltaTime=" + Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _currentCharacterState.FixedUpdateState();
    }
}
