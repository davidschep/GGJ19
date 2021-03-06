﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get { return GetInstance(); } }
    private static DayManager instance;
    private static DayManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<DayManager>();
        }
        return instance;
    }

    [SerializeField] private float dayDuration = 120f; // in seconds
    [SerializeField]  private float fadeDuration = 0.5f; // in seconds
    [SerializeField] private float extraBlackTime = 0.4f; // in seconds

    private float endDayTimer = 0f;
    private float dayTimer = 0f; // in seconds

    private int currentDay = 0;

    void Start()
    {
        Debug.Assert(dayDuration > 0f);
        Debug.Assert(fadeDuration > 0f);

        StartDay();
        EnemyStateManager.Instance.GetComponent<NavMeshAgent>().enabled = true;
    }

    void Update()
    {
        InGameUIController.Instance.SetDayTime(currentDay, Mathf.RoundToInt(dayDuration - dayTimer));
        if (GameStateManager.Instance.gameState == GameState.FADING_TO_NEW_DAY)
        {
            endDayTimer += Time.deltaTime;
            InGameUIController.Instance.SetFadeInOutImageAlpha(endDayTimer / (fadeDuration + extraBlackTime));
            if (endDayTimer > fadeDuration + extraBlackTime)
            {
                StartDay();
            }
        }
        else
        {
            dayTimer += Time.deltaTime;

            if (dayTimer - extraBlackTime < fadeDuration) // fade in, day starting
            {
                InGameUIController.Instance.SetFadeInOutImageAlpha(1f - (dayTimer - extraBlackTime) / fadeDuration);
            }
            else if (dayDuration - dayTimer - extraBlackTime < fadeDuration) // fade out, day almost over
            {
                InGameUIController.Instance.SetFadeInOutImageAlpha(1f - (dayDuration - dayTimer - extraBlackTime) / fadeDuration);
            }
            if (dayTimer > dayDuration)
            {
                // end of day, game over
                GameOver();
            }
        }
    }

    public void StartDay()
    {
        dayTimer = 0f;
        endDayTimer = 0f;
        currentDay++;
        GameStateManager.Instance.gameState = GameState.SEARCHING_FOR_FOOD;
        GameStateManager.Instance.SpawnFood();

        // TODO: set mouse position in base
        // TODO: set random cat position
        EnemyStateManager cat = EnemyStateManager.Instance;
        if(cat != null)
        {
            List<Point> points = PointManager.Instance.GetPoints(PointType.EnemySpawnPoint);
            int posIndex = Random.Range(0, points.Count);
            cat.transform.position = points[posIndex].transform.position;
        }
    }

    public void DayCompleted()
    {
        GameStateManager.Instance.gameState = GameState.FADING_TO_NEW_DAY; // fade out will happen and new day will start
    }

    private void GameOver()
    {
        GameStateManager.Instance.GameOver();
    }
}
