using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private AudioClip gameoverClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool playGameOverSound = false;

    private void Start()
    {
        if(playGameOverSound)
        {
            audioSource.PlayOneShot(gameoverClip);
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}