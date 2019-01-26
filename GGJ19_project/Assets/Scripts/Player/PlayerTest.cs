using UnityEngine;
using System.Collections;

public class PlayerTest : MonoBehaviour
{
    public static PlayerTest Instance { get { return GetInstance(); } }

    private static PlayerTest instance;

    private static PlayerTest GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<PlayerTest>();
        }
        return instance;
    }

}
