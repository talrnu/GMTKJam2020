using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text[] _gamePointsTexts;

    [SerializeField]
    private GameObject _gameOverPanel;
    private GameManager _gameManager;
    private Meters _meters;
    private Button[] _buttons;

    private bool _allObjectsActive = false;
    private List<KeyValuePair<string, int>> _ballNamesAndPoints;

    // Start is called before the first frame update
    void Start()
    {
        _meters = GameObject.Find("Meters").GetComponent<Meters>();
        if (_meters == null)
        {
            Debug.LogError("Meters component in GameOverScreen is null");
        }

        _gameManager = GameObject.Find("game_manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager component in GameOverScreen is null");
        }

        _buttons = GetComponents<Button>();
    }

    void OnGUI()
    {
        GuiUpdates();
    }

    private void GuiUpdates()
    {
        if (_gameManager.IsGameOver())
        {
            if (!_allObjectsActive)
            {
                _gameOverPanel.SetActive(true);

                int index = 0;
                _ballNamesAndPoints = _gameManager.GetBallNamesAndPoints();
                foreach (var gamePointTexts in _gamePointsTexts)
                {
                    //gamePointTexts.gameObject.SetActive(true);
                    gamePointTexts.text = _ballNamesAndPoints[index].Key + ": " + _ballNamesAndPoints[index].Value;
                    ++index;
                }

                //foreach (var button in _buttons)
                //{
                //    button.gameObject.SetActive(true);
                //}

                _allObjectsActive = true;
            }

            //_autoRestartText.text = "Restarting in: " + (int)_countdownTime;
            //_gamePointsText.text = "Total Points: " + _meters.Points;
        }
    }

    public void OnReplayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
