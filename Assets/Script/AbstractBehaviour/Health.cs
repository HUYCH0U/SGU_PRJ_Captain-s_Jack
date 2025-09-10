using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{   
    public abstract int MaxHealth { get; set;}
    public abstract int CurrentHealth { get; set;}
}
