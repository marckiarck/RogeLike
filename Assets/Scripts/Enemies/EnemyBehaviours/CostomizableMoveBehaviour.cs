using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostomizableMoveBehaviour : MoveBehaviour
{
    public void SetMoveDirection(Vector2 newMoveDireciton)
    {
        moveDirection = newMoveDireciton;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
