using Runner.Interfaces;
using Runner.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Triggers
{
    public class DeadBlockerTrigger : MonoBehaviour, ITrigger
    {
        float yScaleHalf;

        GameManager.LastGameState _thisDeadTriggersState;

        private void Awake()
        {
            _thisDeadTriggersState = GetComponentInParent<DeadBlockTriggerAssigner>().GetStateOfBlocker();
        }

        private void Start()
        {
            yScaleHalf = transform.localScale.y / 3f;
        }

        public void Triggered(GameObject playerHorizontal)
        {
            if (GameManager.instance.CanJumpOnThisDeadBlocker(_thisDeadTriggersState))
            {
                //Player Jumped Here

                Debug.Log("Jumped");

                Debug.Log("Jumped : " + GameManager.instance.GetLastGameState());
            }
            else
            {
                //Player Cant Jump Player Dead

                ParticleManager.GetSingleton().FastParticleRequestToDeadAtPos(playerHorizontal.transform.position);

                GameManager.instance.PlayerDead();

                GetComponent<DeadBlockerTrigger>().enabled = false;
            }
        }
    }
}
