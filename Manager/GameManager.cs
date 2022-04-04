using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.GamePlayCore;

namespace Runner.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        #region PlayerReferances
        [Header("Player Referances")]
        public Transform playerForwardTransform;
        public Transform playerHorizontalChildTransform;
        public PlayerAnimationManager playerAnimationManager;
        public PlayerController playerController;

        [Header("Game Referances")]
        public GamePlay gamePlayRef;
        public UI_Manager uiManager;
        #endregion

        #region Events
        //[("Events")]
        public delegate void RagdollOpener();
        public static RagdollOpener ragdollOpenPlayer;

        public delegate void LevelStarted();
        public static LevelStarted onLevelStarted;

        public delegate void ReachedEndGame();
        public static ReachedEndGame onReachedEndGame;
        #endregion

        #region Variables
        private GameStates _gameState = GameStates.stop;
        private LastGameState _lastTouchedState = LastGameState.NormalState;

        private bool _isGameEnded;
        private bool _isPlayerDead;
        #endregion

        #region Enums
        public enum LastGameState
        {
            NormalState,
            Lower,
            LowerJump,
            Medium,
            Higher
        }

        public enum GameStates
        {
            play,
            controllerClosed,
            stop
        }
        #endregion

        private void Awake()
        {
            if (instance)
                Destroy(gameObject);
            else
                instance = this;
        }

        private void Start()
        {
            _gameState = GameStates.stop;

            Application.targetFrameRate = 60;
        }

        public bool IsPlaying()
        {
            return _gameState != (GameStates.stop);
        }

        public bool IsInputsClosed()
        {
            return _gameState != GameStates.play;
        }

        public bool IsPlayerDead()
        {
            return _isPlayerDead;
        }

        public void SetState(GameStates state)
        {
            _gameState = state;
        }

        #region GameStarter
        public void StartGame()
        {
            if (onLevelStarted != null)
                onLevelStarted.Invoke();

            playerAnimationManager.StartGame();
        }

        #endregion

        #region Conditions
        public void PlayerDead()
        {
            if (_isGameEnded)
                return;

            _isPlayerDead = true;
            _isGameEnded = true;

            _gameState = GameStates.stop;

            uiManager.RestartLevelButtonOpener();

            ragdollOpenPlayer.Invoke();
        }

        public void ReachedToEndGame()
        {
            _isGameEnded = true;

            playerController.forwardSpeed /= 2f;

            uiManager.NextLevelButtonOpener();

            playerAnimationManager.EndGameReached();

            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("HighScore", 0) + ComboManager.instance.GetCurrentScoreOnEndGame());

            //reachedEndGame.Invoke();
        }
        #endregion

        #region GamePlay

        public LastGameState GetLastGameState()
        {
            return _lastTouchedState;
        }

        public bool CanJumpOnThisDeadBlocker(LastGameState State)
        {
            if (State == LastGameState.LowerJump)
            {
                if (_lastTouchedState != LastGameState.Lower)
                    return true;

                return false;

            }
            else if (State == LastGameState.Medium)
            {
                if (_lastTouchedState == LastGameState.Medium || _lastTouchedState == LastGameState.Higher)
                    return true;

                return false;
            }
            else
            {
                return State == _lastTouchedState;
            }
        }

        //Triggered Player Last
        //Player Can Dodge Spawned 
        //
        public bool IsPlayerHaveTriggered()
        {
            return _lastTouchedState != LastGameState.NormalState;
        }

        public void SetLastState(LastGameState touchedState)
        {
            _lastTouchedState = touchedState;

            Invoke(nameof(SetDefaultLastTouchedState), 2f);
        }

        private void SetDefaultLastTouchedState()
        {
            _lastTouchedState = LastGameState.NormalState;
        }

        #endregion
    }
}