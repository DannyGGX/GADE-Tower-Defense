using System;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace DannyG
{
	
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private Button startGameButton;
		[SerializeField] private Button exitGameButton;
		[SerializeField] private SceneReference gameScene;
		
		private void Start()
		{
			startGameButton.onClick.AddListener(StartGame);
			exitGameButton.onClick.AddListener(ExitGame);
		}

		private void OnDestroy()
		{
			startGameButton.onClick.RemoveListener(StartGame);
			exitGameButton.onClick.RemoveListener(ExitGame);
		}

		private void StartGame()
		{
			SceneManager.LoadScene(gameScene.BuildIndex);
		}
		
		private void ExitGame()
		{
			Application.Quit();
		}
		
		
	}
}
