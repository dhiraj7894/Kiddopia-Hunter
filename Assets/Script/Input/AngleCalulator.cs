using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalulator : MonoBehaviour
{
    public int DirectionVal;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private void OnEnable()
    {
        TouchManager.onTouchStart += OnStartTouch;
        TouchManager.onTouchEnd += OnEndTouch;
    }

    private void OnEndTouch(Vector3 position)
    {
        _endPosition = position;
        CalculateDirection();
    }

    private void OnStartTouch(Vector3 position)
    {
        _startPosition = position;
    }

    public void CalculateDirection()
    {
        float distance = Vector3.Distance(_endPosition.normalized, _startPosition.normalized);
        if (distance > 0)
        {
            float angle = Mathf.Atan2(_endPosition.y - _startPosition.y, _endPosition.x - _startPosition.x) * 180 / Mathf.PI;
            if (!GameManager.Instance.isDead) PlayerMovement.Instance.MovePlayer(Direction(angle));
        }

    }

    private void OnDisable()
    {
        TouchManager.onTouchStart -= OnStartTouch;
        TouchManager.onTouchEnd -= OnEndTouch;
    }

    public int Direction(float angle)
    {
        if (angle > -45 && angle <= 45)
        {
            if (DirectionVal < 1)
                return DirectionVal += 1;
        }

        if (angle > 125 || angle <= -125)
        {
            if (DirectionVal > -1)
                return DirectionVal -= 1;
        }
        return DirectionVal;
    }
}
