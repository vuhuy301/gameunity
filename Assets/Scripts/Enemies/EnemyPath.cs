using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float chaseDistance = 5f;

    private Rigidbody2D rb;

    private Vector2 moveDir;

    private Knockback knockback;

    private Transform player;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();  
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player has the 'Player' tag.");
        }
    }

    private void FixedUpdate()
    {
        if(knockback.getKnockBack) { return; }
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            moveDir = direction;
        }
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPos)
    {
        moveDir = targetPos;
    }
}
