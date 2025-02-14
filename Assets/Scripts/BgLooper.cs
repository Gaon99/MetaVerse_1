using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int ObstacleCount = 0;
    public Vector3 ObstacleLastPosition = Vector3.zero;

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        ObstacleLastPosition = obstacles[0].transform.position;
        ObstacleCount = obstacles.Length;


        for(int i =0; i<ObstacleCount;i++)
        {
            ObstacleLastPosition = obstacles[i].SetRandomPlace(ObstacleLastPosition, ObstacleCount);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.GetComponent<Obstacle>();

        if(obstacle)
        {
            ObstacleLastPosition = obstacle.SetRandomPlace(ObstacleLastPosition, ObstacleCount);
        }

    }
}
