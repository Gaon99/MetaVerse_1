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

    void Start()
    {
        if (OriginBlock == null)
        {
            Debug.Log("OriginBlock is Null");
            return;
        }

        PrevBlockPosition = Vector3.down;

        Spawn_Block();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& Input.GetKeyDown(KeyCode.Space))
        {
            Spawn_Block();
        }
        transform.position = Vector3.Lerp(transform.position,DesiredPosition,StackMovingSpeed*Time.deltaTime);
    }
    bool Spawn_Block()
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

        NewTrans = NewBlock.transform;
        NewTrans.parent = this.transform;
        NewTrans.localPosition = PrevBlockPosition + Vector3.up;
        NewTrans.localRotation = Quaternion.identity;
        NewTrans.localScale = new Vector3(StackBounds.x, 1, StackBounds.y);

        StackCount++;

        DesiredPosition = Vector3.down * StackCount;
        BlockTransition = 0f;

        LastBlock = NewTrans;

        return true;
    }
}

