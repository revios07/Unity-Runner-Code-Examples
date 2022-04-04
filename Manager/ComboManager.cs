using Runner.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Managers
{
    public class ComboManager : MonoBehaviour
    {
        public static ComboManager instance;

        public delegate void ComboGain(int score);
        public static ComboGain comboGained;

        private int _comboMultiplier;

        private int score;

        private void Awake()
        {
            if (instance)
                Destroy(gameObject);
            else
                instance = this;
        }

        void Start()
        {
            score = 0;
            _comboMultiplier = 0;

            comboGained.Invoke(0);
        }

        public void ComboGainTry()
        {
            Invoke(nameof(ComboUpdate), 1f);
        }

        private void ComboUpdate()
        {
            if (GameManager.instance.IsPlayerDead())
                return;

            ++_comboMultiplier;

            int comboRand = Random.Range(1, 5);

            score += comboRand * _comboMultiplier;

            comboGained.Invoke(score);
        }

        public void LoseCombo()
        {
            _comboMultiplier = 0;
        }

        public int GetCurrentScoreOnEndGame()
        {
            return score;
        }
    }
}
