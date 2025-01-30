using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [Header("Life Controller")]
    [SerializeField] private float currentLife;
    [SerializeField] private float maxLife;
    [SerializeField] protected bool canTakeDamage;
    private bool isDeath;

    protected virtual void Start()
    {
        currentLife = maxLife;
        canTakeDamage = true;
    }

    public void TakeDamage(float _dmg)
    {
        if (!canTakeDamage) return;

        currentLife = Mathf.Max(currentLife - _dmg, 0);

        if (currentLife <= 0) Death();
    }

    public void GainLife(float _life)
    {
        currentLife = Mathf.Min(currentLife + _life, maxLife);
    }

    private void Death()
    {
        isDeath = true;
    }
}
