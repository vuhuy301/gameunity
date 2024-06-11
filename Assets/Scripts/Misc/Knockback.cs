using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool getKnockBack { get; private set; }

    [SerializeField] private float knockBackTime = 2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockBack(Transform damageSource, float knockBackThrust)
    {
        getKnockBack = true;
        Vector2 diff = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(diff, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        getKnockBack = false;
    }
}
