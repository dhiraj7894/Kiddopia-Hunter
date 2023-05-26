using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField] private float _movementOffset;
    [SerializeField] private float _movementTime;

    public void MovePlayer(int direction)
    {
        float moveDistance = _movementOffset * direction;
        LeanTween.move(gameObject, new Vector3(moveDistance, -2.9f, 0), _movementTime).setEaseInCubic();
    }
}
