using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text _gamePointsText;

    [SerializeField]
    private Text _autoRestartText;

    [SerializeField]
    private GameObject _gameOverPanel;
    private GameManager _gameManager;
    private Meters _meters;

    private float _countdownTime;

    // Start is called before the first frame update
    void Start()
    {
        _meters = GameObject.Find("Meters").GetComponent<Meters>();
        if (_meters == null)
        {
            Debug.LogError("Meters component in GameOverScreen is null");
        }
        else
        {
            _gamePointsText.text = "Total Points: " + _meters.Points;
        }

        _gameManager = GameObject.Find("game_manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager component in GameOverScreen is null");
        }
        else
        {
            _countdownTime = _gameManager.WaitTimeForRestart;
            _autoRestartText.text = "Restarting in: " + (int)_countdownTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountdownToRestart();
    }

    void OnGUI()
    {
        GuiUpdates();
    }

    private void GuiUpdates()
    {
        if (_gameManager.IsGameOver())
        {
            _gameOverPanel.SetActive(true);
            _gamePointsText.gameObject.SetActive(true);
            _autoRestartText.gameObject.SetActive(true);

            _autoRestartText.text = "Restarting in: " + (int)_countdownTime;
            _gamePointsText.text = "Total Points: " + _meters.Points;
        }
    }

    private void CountdownToRestart()
    {
        if (_gameManager.IsGameOver())
        {
            _countdownTime -= Time.deltaTime;
        }
    }
}
