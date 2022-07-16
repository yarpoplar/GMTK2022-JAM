using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a basic interface with a single required
//method.
public interface IDamageable
{
    virtual void ApplyDamage(float damage) { }
}