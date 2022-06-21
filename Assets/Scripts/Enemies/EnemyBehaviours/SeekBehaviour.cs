using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : MoveBehaviour
{
    [SerializeField]
    private GameObject objetive;
    protected override void ExecuteBehaviour()
    {
        moveDirection = objetive.transform.position - gameObject.transform.position;
        base.ExecuteBehaviour();
    }
}