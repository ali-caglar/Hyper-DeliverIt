using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action OnGameStateChanged;

    public GameState CurrentGameState { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        ChangeGameState(GameState.Playing);
    }

    public void ChangeGameState(GameState state)
    {
        if (CurrentGameState == state)
        {
            return;
        }

        CurrentGameState = state;
        OnGameStateChanged?.Invoke();
    }
}