using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] bool debug;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float range;
    [SerializeField] float rangeAngle;
    [SerializeField] int damage;
    //[SerializeField] Weapon weapon;

    private float cosRange;

    private void Awake()
    {
        // 시작할때 먼저 계산 해놓는다.
        cosRange = Mathf.Cos(rangeAngle * Mathf.Deg2Rad);
    }
    [ContextMenu("Attack")]
    private void Attack()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            animator.SetTrigger("Attack1");
        }
        else { animator.SetTrigger("Attack2"); }
    }

    Collider[] colliders = new Collider[20];
    private void AttackTiming()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, layerMask);
        for (int i = 0; i < size; i++)
        {
            Vector3 dirToTarger = (colliders[i].transform.position - transform.position).normalized;
            // Angle은 연산이 느려지니 다른 방식을 사용해야된다.
            // if (Vector3.Angle(transform.forward, dirToTarger) > rangeAngle) continue;
            // 위에거 보다 훨씬 빠르다.
            if (Vector3.Dot(transform.forward, dirToTarger) < cosRange) continue;

            IDamagable damagable = colliders[i].GetComponent<IDamagable>();
            damagable?.TakeDamage(damage);

        }
    }

    /* public void EnableWeapon()
     {
         weapon.EnableWeapon();
     }
     public void DisableWeapon()
     {
         weapon.DisableWeapon();
     }*/
    private void OnAttack(InputValue value)
    {
        // UI를 클릭하고 있을때 커멘드가 안되도록 해준다.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}