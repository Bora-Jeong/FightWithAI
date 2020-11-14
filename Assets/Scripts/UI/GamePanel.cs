using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseGroup;
    [SerializeField]
    private GameEndPanel _gameEndPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameEndPanel.isActiveAndEnabled)
        {
            if (_pauseGroup.activeSelf)
            {
                Time.timeScale = 1f;
                _pauseGroup.SetActive(false);
            }
            else 
            {
                Time.timeScale = 0f;
                _pauseGroup.SetActive(true);
            }
        }
    }

    public void OnLobbyButton()
    {
        Time.timeScale = 1f;
        AudioManager.instance.ClickSound();
        GameManager.instance.StopGame();
        _pauseGroup.SetActive(false);
    }

    public void OnQuitGameButton()
    {
        Time.timeScale = 1f;
        AudioManager.instance.ClickSound();
        Application.Quit();
    }
}
