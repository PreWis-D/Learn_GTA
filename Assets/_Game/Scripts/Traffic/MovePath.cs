using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePath : MonoBehaviour
{
    public MovePoint[] MovePoints {  get; private set; }

    private void Awake()
    {
        MovePoints = transform.GetComponentsInChildren<MovePoint>();
    }
}
