using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour
{
    private const float BoundSize = 3.5f;
    private const float MovingBoundsSize = 3f;
    private const float StackMovingSpeed = 5f;
    private const float BlockMovingSpeed = 3.5f;
    private const float ErrorMargin = 0.1f;

    public GameObject OriginBlock = null;

    private Vector3 PrevBlockPosition;
    private Vector3 DesiredPosition;
    private Vector3 StackBounds = new Vector2(BoundSize, BoundSize);

    Transform LastBlock = null;
    float BlockTransition = 0f;
    float SecondaryPosition = 0f;

    int StackCount = -1;
    int ComboCount = 0;

    public Color PrevColor;
    public Color NextColor;

    bool IsMovingX = true;

    void Start()
    {
        if (OriginBlock == null)
        {
            Debug.Log("OriginBlock is Null");
            return;
        }
        PrevColor = GetRandomColor();
        NextColor = GetRandomColor();

        PrevBlockPosition = Vector3.down;
        SpawnBlock();
        SpawnBlock();

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceBlock())
            {
                SpawnBlock();
            }
            else
            {
                // 게임 오버
                Debug.Log("GameOver");
            }
        }

        MoveBlock();
        transform.position = Vector3.Lerp(transform.position, DesiredPosition, StackMovingSpeed * Time.deltaTime);
    }

    bool SpawnBlock()
    {
        if (LastBlock != null)
            PrevBlockPosition = LastBlock.localPosition;

        GameObject NewBlock = null;
        Transform NewTrans = null;

        NewBlock = Instantiate(OriginBlock);

        if (NewBlock == null)
        {
            Debug.Log("New Block Instantiate Failed");
            return false;
        }

        ColorChange(NewBlock);

        NewTrans = NewBlock.transform;
        NewTrans.parent = this.transform;
        NewTrans.localPosition = PrevBlockPosition + Vector3.up;
        NewTrans.localRotation = Quaternion.identity;
        NewTrans.localScale = new Vector3(StackBounds.x, 1, StackBounds.y);

        StackCount++;

        DesiredPosition = Vector3.down * StackCount;
        BlockTransition = 0f;

        LastBlock = NewTrans;

        IsMovingX = !IsMovingX;

        return true;
    }

    Color GetRandomColor()
    {
        float R = Random.Range(100f, 250f) / 255f;
        float G = Random.Range(100f, 250f) / 255f;
        float B = Random.Range(100f, 250f) / 255f;
        return new Color(R, G, B);
    }

    void ColorChange(GameObject gameObject)
    {
        Color ApplyColor = Color.Lerp(PrevColor, NextColor, (StackCount % 11) / 10f);

        Renderer renderer = gameObject.GetComponent<Renderer>();

        if (renderer == null)
        {
            Debug.Log("Renderer is Null");
            return;
        }

        renderer.material.color = ApplyColor;
        Camera.main.backgroundColor = ApplyColor - new Color(0.3f, 0.3f, 0.3f);

        if (ApplyColor.Equals(NextColor))
        {
            PrevColor = NextColor;
            NextColor = GetRandomColor();
        }
    }

    void MoveBlock()
    {
        BlockTransition += Time.deltaTime * BlockMovingSpeed;

        float MovePosition = Mathf.PingPong(BlockTransition, BoundSize) - BoundSize / 2;

        if (IsMovingX)
        {
            LastBlock.localPosition = new Vector3(MovePosition * MovingBoundsSize, StackCount, SecondaryPosition);
        }
        else
        {
            LastBlock.localPosition = new Vector3(SecondaryPosition, StackCount, -MovePosition * MovingBoundsSize);
        }
    }


    bool PlaceBlock()
    {
        Vector3 LastPosition = LastBlock.localPosition;

        if (IsMovingX)
        {
            float DeltaX = PrevBlockPosition.x - LastPosition.x;
            bool IsNegativeNum = (DeltaX < 0) ? true : false;

            DeltaX = Mathf.Abs(DeltaX);

            if (DeltaX > ErrorMargin)
            {
                StackBounds.x -= DeltaX;

                if (StackBounds.x <= 0)
                {
                    return false;
                }
                float middle = (PrevBlockPosition.x + LastPosition.x) / 2;
                LastBlock.localScale = new Vector3(StackBounds.x, 1, StackBounds.y);

                Vector3 TempPos = LastBlock.localPosition;
                TempPos.x = middle;

                LastBlock.localPosition = LastPosition = TempPos;

                float RubbleHalfScale = DeltaX / 2f;
                CreateRubble(
                    new Vector3(
                        IsNegativeNum
                        ? LastPosition.x + StackBounds.x / 2 + RubbleHalfScale
                        : LastPosition.x - StackBounds.x / 2 - RubbleHalfScale
                        , LastPosition.y, LastPosition.z
                        ),
                    new Vector3(DeltaX, 1, StackBounds.y)
                );


            }
            else
            {
                LastBlock.localPosition = PrevBlockPosition + Vector3.up;
            }
        }
        else
        {
            float DeltaZ = PrevBlockPosition.z - LastPosition.z;
            bool IsNegativeNum = (DeltaZ < 0) ? true : false;

            DeltaZ = Mathf.Abs(DeltaZ);
            if (DeltaZ > ErrorMargin)
            {
                StackBounds.y -= DeltaZ;

                if (StackBounds.y <= 0)
                {
                    return false;
                }

                float middle = (PrevBlockPosition.z + LastPosition.z) / 2;
                LastBlock.localScale = new Vector3(StackBounds.x, 1, StackBounds.y);

                Vector3 TempPos = LastBlock.localPosition;
                TempPos.z = middle;

                LastBlock.localPosition = LastPosition = TempPos;

                float RubbleHalfScale = DeltaZ / 2f;

                CreateRubble(new Vector3(LastPosition.x, LastPosition.y,
                    IsNegativeNum ? LastPosition.z + StackBounds.z / 2 + RubbleHalfScale : LastPosition.z - StackBounds.z / 2 - RubbleHalfScale),
                    new Vector3(StackBounds.x, 1, DeltaZ));
            }
            else
            {
                LastBlock.localPosition = PrevBlockPosition + Vector3.up;
            }
        }

        SecondaryPosition = (IsMovingX) ? LastBlock.localPosition.x : LastBlock.localPosition.z;

        return true;
    }

    void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject gameObject = Instantiate(LastBlock.gameObject);
        gameObject.transform.parent = this.transform;

        gameObject.transform.localPosition = pos;
        gameObject.transform.localScale = scale;
        gameObject.transform.localRotation = Quaternion.identity;

        gameObject.AddComponent<Rigidbody>();

        gameObject.name = "Rubble";
    }
}