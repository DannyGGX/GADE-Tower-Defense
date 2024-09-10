using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DannyG
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button restartButton;

        private void Start()
        {
            resumeButton.onClick.AddListener(ChangePauseState);
            exitButton.onClick.AddListener(ExitGame);
            restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDestroy()
        {
            resumeButton.onClick.RemoveListener(ChangePauseState);
            exitButton.onClick.RemoveListener(ExitGame);
            restartButton.onClick.RemoveListener(RestartGame);
        }

        public bool IsPaused { get; private set; } = false;
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ChangePauseState();
            }
        }

        private void ChangePauseState()
        {
            IsPaused = !IsPaused;
            pauseMenu.SetActive(IsPaused);

            Time.timeScale = IsPaused ? 0f : 1f;
        }
        
        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}