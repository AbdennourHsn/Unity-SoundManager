using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CleanArchitect
{
    /// <summary>
    /// GlobalGameManager is a singleton class that manages the core lifecycle and state of a Unity game.
    /// It provides centralized control over game states, data persistence, audio management, and more.
    /// 
    /// Features:
    /// - Singleton Pattern: Ensures a single instance of GlobalGameManager exists.
    /// - Persistent Data Management: Saves and loads game data such as points and coins.
    /// - Event Management: Custom events for game lifecycle states (OnGameStart, OnGameComplete, OnGameOver, OnGamePause, OnGameResume).
    /// - Scene Management: Methods to reload the current scene and load specific levels.
    /// - Game Control Methods: Methods to start, pause, resume, and end the game.
    /// - Extend the class by adding custom attributes and methods as per your game requirements.
    ///</summary>
    
    [System.Serializable]
    public class CustomEvents : UnityEvent
    { }

    public class GlobalGameManager : MonoBehaviour
    {
        public static GlobalGameManager instance;
        public string gameIdentifier;
        
        [Space(20)]
        [Header("Levels Setup")]
        private int points;
        private int coins;
        
        [Header("Game LifeCycle")]
        [Space(10)]
        public CustomEvents OnGameStart;
        
        [Space(10)]
        public CustomEvents OnGameComplete;
        
        [Space(10)]
        public CustomEvents OnGameOver;
        
        [Space(10)]
        public CustomEvents OnGamePause;
        
        [Space(10)]
        public CustomEvents OnGameResume;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }
        
        private void Start()
        {
            
        }

        public void StartGame()
        {
            this.OnGameStart.Invoke();
        }

        public void GameOver()
        {
            this.OnGameOver.Invoke();
        }

        public void GameComplate()
        {
            this.OnGameComplete.Invoke();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            this.OnGamePause.Invoke();
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            this.OnGameResume.Invoke();
        }

        public void ReloadScene()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

        
        public void SetPoints(int value)
        {
            this.points = value;
        }

        public void SetCoin(int value)
        {
            this.coins = value;
        }
        public void AddCoins(int coins)
        {
            this.coins += coins;
        }

        public void LoadLevel(int level)
        {
            PlayerPrefs.SetInt(gameIdentifier+"level", level);
            ReloadScene();
        }
    }
}