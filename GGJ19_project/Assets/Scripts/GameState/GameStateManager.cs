using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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

    const string GAMEOVER_SCENE_NAME = "gameover_menu";

    public GameState gameState;

    private void Start() 
    {
        PlayerController.Instance.playerDeathEvent.AddListener(GameOver);    
    }

    public void GameOver()
    {
        Debug.Log("GameOver!");
        gameState = GameState.GAME_OVER;
        SceneManager.LoadScene(GAMEOVER_SCENE_NAME);
    }
}
