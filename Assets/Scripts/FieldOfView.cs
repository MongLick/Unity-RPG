using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    private void Update()
    {
        FindTarget();
    }

    Collider[] colliders = new Collider[20];
    private void FindTarget()
    {
        // 범위내에 있는가
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetMask);
        for (int i = 0; i < size; i++)
        {
            // 각도내에 있는가
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            if(Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
            {
                continue;
            }

            // 중간에 장애물이 있는가
            if (Physics.Raycast(transform.position, dirToTarget, obstacleMask))
            {
                continue;
            }
        }
    }
}
