using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void DoDamage(int damageAmount, AttributeType damageType);

    bool IsPlayer();
}
