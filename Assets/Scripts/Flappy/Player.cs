using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float FlapForce = 6f;
    public float ForwardSpeed = 3f;
    public bool IsDead = false;
    float deathCooldown = 0f;

    bool IsFlap = false;

    public bool godMode = false;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.Log("Not Founded Animator");

        if (_rigidbody == null)
            Debug.Log("Not Founded RigidBody");



    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
                {

                    gameManager.Restart();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
            
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)))
        {
            IsFlap = true;
        }
        
    }

    public void FixedUpdate()
    {
        if (IsDead)
            return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = ForwardSpeed;

        if (IsFlap)
        {
            velocity.y += FlapForce;
            IsFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp(_rigidbody.velocity.y * 10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode)
            return;
        if (IsDead)
            return;

        animator.SetInteger("IsDie", 1);
        IsDead = true;
        deathCooldown = 1f;

        gameManager.GameOver();

    }

}
