using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Runner.Managers;

public class GameStarter : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject[] _closedGameObjectsWhenStart;

    [SerializeField]
    private GameObject[] _openedGameObjectsWhenStart;

    //Down At Spesific SlideToMove
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.StartGame();

        for(int i = 0; i < _closedGameObjectsWhenStart.Length; ++i)
        {
            _closedGameObjectsWhenStart[i].SetActive(false);
        }

        for(int i = 0; i < _openedGameObjectsWhenStart.Length; ++i)
        {
            _openedGameObjectsWhenStart[i].SetActive(true); 
        }

        gameObject.SetActive(false);
    }
}
