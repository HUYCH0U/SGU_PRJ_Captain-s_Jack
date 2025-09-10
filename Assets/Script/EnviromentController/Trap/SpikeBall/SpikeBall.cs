using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    [SerializeField] private float Angle;
    [SerializeField] private float moveSpeed;
    private float timer;
    private float CurrentAngle=0;
    void Update()
    {
       timer+= Time.deltaTime*moveSpeed;
       float Angle= math.sin(timer)*this.Angle;
       transform.rotation= Quaternion.Euler(new Vector3(0,0,Angle+CurrentAngle));

    }
}
