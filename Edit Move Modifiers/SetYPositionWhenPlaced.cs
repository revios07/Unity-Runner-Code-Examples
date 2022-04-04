using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetYPositionWhenPlaced : MonoBehaviour
{
    [SerializeField]
    private float _wantedY = 1f;

    private IEnumerator _setPosCoroutine;

    private void Start()
    {

    }

    [ExecuteInEditMode]
    private void OnValidate()
    {
        if (!gameObject.activeInHierarchy)
            return;

        Debug.Log("Position Y Sets");

        transform.position = new Vector3(transform.position.x, _wantedY, transform.position.z);

        if (_setPosCoroutine != null)
            return;

        _setPosCoroutine = SetPosition();
        StartCoroutine(_setPosCoroutine);
    }

    private void OnDestroy()
    {
        StopCoroutine(_setPosCoroutine);
        _setPosCoroutine = null;
    }

    private IEnumerator SetPosition()
    {
        while (true)
        {
            Debug.Log("Placed");

            transform.position = new Vector3(transform.position.x, _wantedY, transform.position.z);

            yield return new WaitForEndOfFrame();
        }
    }
}
