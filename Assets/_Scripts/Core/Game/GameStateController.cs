using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController
{
    // Attributes
    private readonly Stack<GameState> gameStateStack = new();

    // Events
    public event GameStateChangeEvent OnGameStateChange;
    public event Action<bool> OnPausedEvent;

    // Constructor
    public GameStateController()
    {
        gameStateStack.Push(GameState.NULL);
        gameStateStack.Push(GameState.RUNNING);
        OnGameStateChange += LogGameStateEvent;
    }

    // Functions
    public void PushState(GameState gameState)
    {
        gameStateStack.Push(gameState);

        SetState(gameState);
        OnGameStateChange?.Invoke(
            new GameStateChangeArgs(
                StackChangeEventType.PUSH,
                gameStateStack.Count,
                gameState
            )
        );
    }

    public void PopState()
    {
        gameStateStack.Pop();
        GameState gameState = gameStateStack.Peek();

        SetState(gameState);
        OnGameStateChange?.Invoke(
            new GameStateChangeArgs(
                StackChangeEventType.POP,
                gameStateStack.Count,
                gameState
            )
        );
    }

    public void HandleEscape()
    {
        switch (gameStateStack.Peek())
        {
            case GameState.RUNNING:
                PushState(GameState.PAUSED);
                OnPausedEvent?.Invoke(true);
                return;

            case GameState.CHEAT:
                PopState();
                return;

            case GameState.PAUSED:
                OnPausedEvent?.Invoke(false);
                PopState();
                return;
            case GameState.CUTSCENE:
            case GameState.MENU:
                PopState();
                return;

            default:
                return;
        }
    }

    // Internal Functions
    private void LogGameStateEvent(GameStateChangeArgs e)
    {
        Debug.Log($"GameState {e.EventType}; Current gamestate is {e.NewGameState}");
    }

    private void SetState(GameState gameState)
    {
        //TODO: Review cutscenes and menu behaviour
        Time.timeScale = gameState switch
        {
            GameState.PAUSED => 0,
            GameState.CHEAT => 0,
            GameState.RUNNING => 1,
            GameState.CUTSCENE => 0,
            GameState.MENU => 0,
            _ => throw new Exception("Invalid gameState pushed to GameStateController, please refer to enum GameState for valid states"),
        };
        Cursor.lockState = gameState switch
        {
            GameState.PAUSED => CursorLockMode.None,
            GameState.CHEAT => CursorLockMode.None,
            GameState.RUNNING => CursorLockMode.Locked,
            GameState.CUTSCENE => CursorLockMode.Locked,
            GameState.MENU => CursorLockMode.None,
            _ => throw new Exception("Invalid gameState pushed to GameStateController, please refer to enum GameState for valid states"),
        };
    }

    public GameState GetState()
    {
        return gameStateStack.Peek();
    }
}