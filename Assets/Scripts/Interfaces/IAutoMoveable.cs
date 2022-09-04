using System.Collections;
using UnityEngine;

public interface IAutoMoveable
{
    void setMovement();
    IEnumerator MoveCoroutine();
}
