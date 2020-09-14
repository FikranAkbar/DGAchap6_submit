using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Bird
{
    [SerializeField]
    private float fieldOfImpact = 4f;
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private LayerMask layerToHit;

    public bool _isExploded = false;

    private void Update()
    {
        if (State == BirdState.HitSomething && !_isExploded)
        {
            Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);
            foreach (Collider2D other in others)
            {
                Vector2 dir = other.transform.position - transform.position;
                other.GetComponent<Rigidbody2D>().AddForce(dir * force);
            }
            _isExploded = true;
        }

        if (_isExploded)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
