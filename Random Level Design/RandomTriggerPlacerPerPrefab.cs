using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Triggers;

public class RandomTriggerPlacerPerPrefab : MonoBehaviour
{
    [Tooltip("This Should Upper Of Parent")]

    private Transform[] _triggersTransforms;

    private void Awake()
    {
        TriggersSelectable[] triggersSelectables = GetComponentsInChildren<TriggersSelectable>();

        _triggersTransforms = new Transform[triggersSelectables.Length];

        //Take Transforms Of Selectable GameObjects
        for (int i = 0; i < _triggersTransforms.Length; ++i)
        {
            _triggersTransforms[i] = triggersSelectables[i].transform;
        }
    }

    private void Start()
    {
        //Every Time Swap Random Positions Of Triggers
        RandomSwapPositions();
    }

    //<summary>
    //Transform Ref Type
    //Vector3 Value Type
    //</summary>
    private void RandomSwapPositions()
    {
        int randomNum1 = Random.Range(0, 3);
        int randomNum2 = Random.Range(1, 3);

        //Random Placement 1
        Vector3 tempPos1 = _triggersTransforms[0].localPosition;
        _triggersTransforms[0].localPosition = _triggersTransforms[randomNum1].localPosition;
        _triggersTransforms[randomNum1].localPosition = tempPos1;

        //Random Placement 2
        Vector3 tempPos2 = _triggersTransforms[1].localPosition;
        _triggersTransforms[1].localPosition = _triggersTransforms[randomNum2].localPosition;
        _triggersTransforms[randomNum2].localPosition = tempPos2; 
    }
}
