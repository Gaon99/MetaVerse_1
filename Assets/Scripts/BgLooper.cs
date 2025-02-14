using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int ObstacleCount = 0;
    public Vector3 ObstacleLastPosition = Vector3.zero;
    public int NumBgCount = 5; // Unity���� �۾��ÿ�, �� ���Ǵ� 5���� Object�� ������ ����

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        ObstacleLastPosition = obstacles[0].transform.position;
        ObstacleCount = obstacles.Length;


        for (int i = 0; i < ObstacleCount; i++)
        {
            ObstacleLastPosition = obstacles[i].SetRandomPlace(ObstacleLastPosition, ObstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            float WidthofBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += WidthofBgObject * NumBgCount;
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();

        if(obstacle)
        {
            ObstacleLastPosition = obstacle.SetRandomPlace(ObstacleLastPosition, ObstacleCount);
        }

    }
}
