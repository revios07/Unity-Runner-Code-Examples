using Runner.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("---Variables---")]
    [SerializeField]
    public float forwardSpeed;
    [SerializeField]
    [Range(0.1f, 10f)]
    private float _sensitivity;
    [Header("Limit X Local Axis")]
    [Range(1f, 5f)]
    private float _xLocalAxisLimit = 2.6f;

    private float _childAngleY = 0f;

    [Header("Input X Watch")]
    [SerializeField]
    private float _xInput;

    [Header("---Referances---")]
    [SerializeField]
    private Transform _childHorizontalTransform;
    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = GameObject.FindObjectOfType<InputManager>();
    }

    //Inputs
    private void Update()
    {
        if (GameManager.instance.IsInputsClosed())
        {
            _xInput = 0f;
            return;
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                _xInput += Input.GetTouch(0).deltaPosition.x * Time.deltaTime;
            else
                _xInput = Mathf.Lerp(_xInput, 0f, 10f * Time.deltaTime);
        }
        //Player Not Touching Screen
        else
        {
            _xInput = Mathf.Lerp(_xInput, 0f, 10f * Time.deltaTime);
        }

        _xInput = Mathf.Clamp(_xInput, -1f, 1f);
    }

    //Movement
    private void FixedUpdate()
    {
        if (!GameManager.instance.IsPlaying())
            return;

        //Forward Movement
        transform.Translate(Vector3.forward * (forwardSpeed * Time.fixedDeltaTime - (_xInput / 50f)));

        //Limit Horizontal Axis Here
        float horizontalMoverX = Mathf.Clamp(Mathf.Lerp(_childHorizontalTransform.localPosition.x, _xInput * _sensitivity + _childHorizontalTransform.localPosition.x, 5f * Time.fixedDeltaTime), -_xLocalAxisLimit, _xLocalAxisLimit);

        //Horizontal Movement
        _childHorizontalTransform.localPosition = new Vector3(horizontalMoverX, _childHorizontalTransform.localPosition.y, 0f);

        if (Input.touchCount > 0 && Mathf.Abs(_childHorizontalTransform.localPosition.x) < 3.6f && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _childAngleY += _xInput * 30f;
            _childAngleY = Mathf.Clamp(_childAngleY, -45f, 45f);
        }
        else
        {
            _childAngleY = Mathf.Lerp(_childAngleY, 0f, 15f * Time.fixedDeltaTime);
        }

        _childHorizontalTransform.localEulerAngles = new Vector3(0f, _childAngleY, 0f);
    }
}
