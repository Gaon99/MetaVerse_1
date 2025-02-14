using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float HighPosY = 1f;
    public float LowPosY = -1f;

    public float HoleSizeMin = 1f;
    public float HoleSizeMax = 3f;

    public Transform TopObject;
    public Transform BottomObject;

    public float widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float Holesize = Random.Range(HoleSizeMin, HoleSizeMax);
        float BalancedHoleSize = Holesize / 2.5f;

        TopObject.localPosition = new Vector3(0, BalancedHoleSize);
        BottomObject.localPosition = new Vector3(0, -BalancedHoleSize);

        Vector3 PlacePosition = lastPosition + new Vector3(widthPadding, 0);

        PlacePosition.y = Random.Range(LowPosY, HighPosY);

        transform.position = PlacePosition;

        return PlacePosition;
    }

}
