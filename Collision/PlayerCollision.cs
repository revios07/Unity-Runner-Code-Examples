using Runner.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ITrigger>().Triggered(gameObject);
    }
}
