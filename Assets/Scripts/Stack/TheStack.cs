using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public int Score { get => StackCount; }
    int ComboCount = 0;
    public int Combo { get => ComboCount; }
    public int MaxCombo = 0;

    public int _MaxCombo { get => MaxCombo; }

    public Color PrevColor;
    public Color NextColor;

    bool IsMovingX = true;

    int bestScore = 0;
    public int BestScore { get => bestScore; }

    int bestCombo = 0;
    public int BestCombo { get => bestCombo; }

    private const string BestScoreKey = "BestScore";
    private const string BestComboKey = "BestCombo";


    private bool IsGameOver = true;

    void Start()
    {
        if (OriginBlock == null)
        {
            Debug.Log("OriginBlock is Null");
            return;
        }

        bestScore = PlayerPrefs.GetInt(BestScoreKey);
        bestCombo = PlayerPrefs.GetInt(BestComboKey);

        PrevColor = GetRandomColor();
        NextColor = GetRandomColor();

        PrevBlockPosition = Vector3.down;
        SpawnBlock();
        SpawnBlock();

    }
    void Update()
    {
        if (IsGameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceBlock())
            {
                SpawnBlock();
            }
            else
            {
                Debug.Log("GameOver");
                UpdateScore();
                IsGameOver = true;
                GameOverEffect();
                UI_Manager.Instance.SetScoreUI();
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

        UI_Manager.Instance.UpdateScore();
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
                ComboCount = 0;

            }
            else
            {
                ComboCheck();
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
                ComboCount = 0;
            }
            else
            {
                ComboCheck();
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


    void ComboCheck()
    {
        ComboCount++;

        if (ComboCount > MaxCombo)
            MaxCombo = ComboCount;
        if ((ComboCount % 5) == 0)
        {
            Debug.Log("5 Combo Success");
            StackBounds += new Vector3(0.5f, 0.5f);
            StackBounds.x = (StackBounds.x > BoundSize) ? BoundSize : StackBounds.x;
            StackBounds.y = (StackBounds.y > BoundSize) ? BoundSize : StackBounds.y;
        }
    }


    void UpdateScore()
    {
        if (bestScore < StackCount)
        {
            bestScore = StackCount;
            bestCombo = MaxCombo;

            PlayerPrefs.SetInt(BestComboKey, bestScore);
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
        }
    }

    void GameOverEffect()
    {
        int ChildCount = this.transform.childCount;

        for(int i=1; i<20;i++)
        {
            if (ChildCount < i) break;

            GameObject gameObject = transform.GetChild(ChildCount - i).gameObject;

            if (gameObject.name.Equals("Rubble")) continue;

            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();

            rigidbody.AddForce((Vector3.up * Random.Range(0, 10f) + Vector3.right * (Random.Range(0, 10f) - 5f))*100f);

            }
    }

    public void Restart()
    {
        int ChildCount = transform.childCount;

        for (int i = 0; i < ChildCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        IsGameOver = false;

        LastBlock = null;
        DesiredPosition = Vector3.zero;
        StackBounds = new Vector3(BoundSize, BoundSize);

        StackCount = -1;
        IsMovingX = true;
        BlockTransition = 0f;
        SecondaryPosition = 0f;

        ComboCount = 0;
        MaxCombo = 0;

        PrevBlockPosition = Vector3.down;

        PrevColor = GetRandomColor();
        NextColor = GetRandomColor();

        SpawnBlock();
        SpawnBlock();
    }
}