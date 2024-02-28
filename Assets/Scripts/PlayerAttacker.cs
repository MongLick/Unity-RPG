using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float range;
    [SerializeField] float angle;
    [SerializeField] int damage;

    private float cosRange;

    private void Awke()
    {
        cosRange = Mathf.Cos(angle * Mathf.Deg2Rad);
    }

    private void Attack()
    {
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            animator.SetTrigger("Attack1");
        }
        else
        {
            animator.SetTrigger("Attack2");
        }
    }

    Collider[] colliders = new Collider[20];
    private void AtttackTiming()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders);
        for(int i = 0; i < size; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) > angle)
            {
                continue;
            }

            if(Vector3.Dot(transform.forward, dirToTarget) < cosRange)
            {
                continue;
            }

            IDamagable damagable = colliders[i].GetComponent<IDamagable>();
            damagable?.TakeDamage(damage);
        }
    }

    private void OnAttack(InputValue value)
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
