using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils
{
    public static Vector3 FindTeleportPlace(Transform transform, BorderType borderType)
    {

        
            switch (borderType)
            {
                case BorderType.Bottom:

                    transform.position = new Vector3(transform.position.x,
                        transform.position.y + FindDistanceBetweenTopAndBottom(), 0);
                    break;
                case BorderType.Left:
                    transform.position = new Vector3(transform.position.x + FindDistanceBetweenLeftAndRight(),
                        transform.position.y, 0);

                    break;
                case BorderType.Right:
                    transform.position = new Vector3(transform.position.x - 1f * +FindDistanceBetweenLeftAndRight(),
                        transform.position.y, 0);

                    break;
                case BorderType.Top:
                    transform.position = new Vector3(transform.position.x,
                        transform.position.y + (-1f * FindDistanceBetweenTopAndBottom()), 0);

                    break;
                default:
                    Debug.LogErrorFormat("No Border Found, Probably a bug.");
                    break;

            }

            return transform.position;

    }

    public static float FindDistanceBetweenTopAndBottom()
    {
        return Mathf.Abs(GameObject.Find("BottomBorder").transform.position.y - GameObject.Find("TopBorder").transform.position.y);

    }

    public static float FindDistanceBetweenLeftAndRight()
    {
        return Mathf.Abs(GameObject.Find("LeftBorder").transform.position.x - GameObject.Find("RightBorder").transform.position.x);

    }
    public static Vector3 GetRandomizeDirectionVector()
    {
        return new Vector3(Random.Range(0, 359), Random.Range(0, 359), 0);
    }
}
