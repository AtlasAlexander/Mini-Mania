using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseOptionsMenu;

    [Header("First Selected Options")]
    [SerializeField] private GameObject pauseMenuFirst;
    [SerializeField] private GameObject pauseOptionsMenuFirst;

    PauseMenu _pauseMenu;

    private bool isPaused;

    private void Start()
    {
        pauseMenu.SetActive(false);
        pauseOptionsMenu.SetActive(false);
        _pauseMenu = GetComponent<PauseMenu>();
    }

    private void Update()
    {
        if(_pauseMenu.GamePaused)
        {
            isPaused = true;
        }

        else
        {
            isPaused = false;
        }

    }
}
