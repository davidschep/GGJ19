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

    [SerializeField] private GameObject foodPrefab;
    const int maxFoodSpawns = 3;
    List<GameObject> spawnedFoodPickups = new List<GameObject>();

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

    public void SpawnFood()
    {
        RemoveOldFood();

        List<Point> points = PointManager.Instance.GetPoints(PointType.Pickup);
        Debug.Assert(points.Count > 0);
        for (int i = 0; i < maxFoodSpawns; i++)
        {
            // TODO: check if point hasn't been picked yet
            // TODO: collision doesn't work yet.
            Transform pointTransform = points[(int)Random.Range(0, points.Count)].transform;
            GameObject food = Instantiate(foodPrefab, pointTransform.position, pointTransform.rotation);
            spawnedFoodPickups.Add(food);
        }
    }

    private void RemoveOldFood()
    {
        for (int i = 0; i < spawnedFoodPickups.Count; i++)
        {
            Destroy(spawnedFoodPickups[i]); // could pool this, but performance increase would be minimal and it would make the system less robust, object set themselves inactive when picked up
        }
    }
}
