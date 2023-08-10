using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PowerupEffect : ScriptableObject
{
    public abstract void ApplyEffect(GameObject target);
    public abstract void DisableEffect(GameObject target);
    public abstract float getDuration();
}


