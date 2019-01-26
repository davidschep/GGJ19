using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    SEARCHING_FOR_FOOD = 0,
    FADING_TO_NEW_DAY,
    GAME_OVER
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get { return GetInstance(); } }
    private static GameStateManager instance;
    private static GameStateManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<GameStateManager>();
        }
        return instance;
    }

    public GameState gameState;
}
