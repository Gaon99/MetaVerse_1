using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Obstacle : MonoBehaviour
{
    public float HighPosY = 1f;
    public float LowPosY = -1f;

    public float HoleSizeMin = 1f;
    public float HoleSizeMax = 3f;

    public Transform TopObject;
    public Transform BottomObject;

    public float widthPadding = 4f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            gameManager.AddScore(1);
        }
    }
}
