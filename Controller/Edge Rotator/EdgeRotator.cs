using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Runner.Managers;
using Runner.Interfaces;

public class EdgeRotator : MonoBehaviour, ITrigger
{
    [SerializeField]
    [Range(-90f, 90f)]
    private float _turnValuePlayer;

    [SerializeField]
    [Range(0.2f, 3f)]
    private float _turnCompleateSecondsPlayer;

    public void Triggered(GameObject playerHorizontal)
    {
        GameManager.instance.playerController.transform.DORotate(new Vector3(0f, _turnValuePlayer, 0f), _turnCompleateSecondsPlayer).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed);
        enabled = false;
    }
}
