using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    Rigidbody2D RB { get; set; }

    bool IsFacingRight { get; set; }

    void MoveCharacter(Vector2 velocity);

    void CheckForLeftOrRightFacing();
}
