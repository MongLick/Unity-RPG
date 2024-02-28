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
        // �������� �ִ°�
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, targetMask);
        for (int i = 0; i < size; i++)
        {
            // �������� �ִ°�
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            if(Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
            {
                continue;
            }

            // �߰��� ��ֹ��� �ִ°�
            if (Physics.Raycast(transform.position, dirToTarget, obstacleMask))
            {
                continue;
            }
        }
    }
}
