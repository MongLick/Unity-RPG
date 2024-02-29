using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamagableAdapter : MonoBehaviour, IDamagable
{
    public UnityEvent<int> OnTakeDamaged;
    
    public void TakeDamage(int damage)
    {
        OnTakeDamaged?.Invoke(damage);
    }
}
