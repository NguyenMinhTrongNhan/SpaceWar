using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGo;
    public GameObject scoreUIText;
    public GameObject TextTimeCount;
    public GameObject GameTitle;
    public enum GameManagerStatus
    {
        Opening,
        GamePlay,
        GameOver,
    }
    GameManagerStatus GMStatus;
    private void Start()
    {
        GMStatus = GameManagerStatus.Opening;
    }

    void UpdateGameManagerStatus()
    {
        switch (GMStatus)
        {
            case GameManagerStatus.Opening:
                GameOverGo.SetActive(false);

                playButton.SetActive(true);

                GameTitle.SetActive(true);
                break;
            case GameManagerStatus.GamePlay:
                //
                scoreUIText.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                //
                GameTitle.SetActive(false);
                //
                playerShip.GetComponent<PlayerShipController>().Init();
                //
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                //
                TextTimeCount.GetComponent<TimeCount>().StartTimeCounter();
                break;
            case GameManagerStatus.GameOver:
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                //
                GameOverGo.SetActive(true);
                Invoke("ChangeToOpeningStatus", 8f);
                break;
        }
    }
    public void SetGameManagerStatus(GameManagerStatus status)
    {
        GMStatus = status;
        UpdateGameManagerStatus();
    }
    public void StartGamePlay()
    {
        GMStatus = GameManagerStatus.GamePlay;
        UpdateGameManagerStatus();
    }
    public void ChangeToOpeningStatus()
    {
        SetGameManagerStatus(GameManagerStatus.Opening);
    }
}
