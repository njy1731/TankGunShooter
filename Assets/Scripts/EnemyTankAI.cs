using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankAI : MonoBehaviour
{
    public float detectionRange = 5.0f;
    public float attackRange = 1.5f;
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 5.0f;
    public float attackCooldown = 2.0f;
    public int attackDamage = 10;

    private Transform playerTransform;
    private bool isAttacking = false;
    private bool isCooldown = false;
    private float cooldownTimer = 0.0f;

    enum State
    {
        Idle,
        Chase,
        Attack,
    }

    private State currentState = State.Idle;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        switch (currentState)
        {
            case State.Idle:
                if (distanceToPlayer < detectionRange)
                {
                    currentState = State.Chase;
                }
                break;

            case State.Chase:
                if (distanceToPlayer < attackRange)
                {
                    currentState = State.Attack;
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = State.Idle;
                }
                else
                {
                    Vector3 direction = (playerTransform.position - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                    Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    transform.position += direction * moveSpeed * Time.deltaTime;
                }
                break;

            case State.Attack:
                if (!isCooldown)
                {
                    isAttacking = true;
                    isCooldown = true;
                    cooldownTimer = attackCooldown;
                }
                else
                {
                    cooldownTimer -= Time.deltaTime;
                    if (cooldownTimer <= 0.0f)
                    {
                        isAttacking = false;
                        isCooldown = false;
                    }
                }

                if (distanceToPlayer > attackRange)
                {
                    currentState = State.Chase;
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 플레이어에게 데미지를 입히는 코드를 여기에 작성합니다.
            Debug.Log("Enemy deals " + attackDamage + " damage to Player!");
        }
    }
}
