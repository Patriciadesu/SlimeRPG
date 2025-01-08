using System;
using System.Collections;
using UnityEngine;

public abstract class NormalAttack : Skill
{
    public static implicit operator NormalAttack(bool v)
    {
        throw new NotImplementedException();
    }
}
