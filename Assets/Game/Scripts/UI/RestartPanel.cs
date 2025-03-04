using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private GameObject _restartPanel;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartGame);
        EventBus.Instance.playerDied += ShowPanel;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        EventBus.Instance.playerDied -= ShowPanel;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void ShowPanel()
    {
        _restartPanel.SetActive(true);
    }
}
