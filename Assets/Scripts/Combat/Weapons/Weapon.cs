﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 10f, attackRate = 1f;
    public bool canAttack = false;

    private float attackTimer = 0f;

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 1 / attackRate)
        {
            canAttack = true;
        }
    }

    // In the derivative weapon class, call the 'base.Attack()' function to reset the ability to attack
    public virtual void Attack()
    {
        attackTimer = 0;
        canAttack = false;
    }
}
