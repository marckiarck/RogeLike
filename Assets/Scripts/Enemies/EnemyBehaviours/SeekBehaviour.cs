using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : MoveBehaviour
{
    [SerializeField]
    protected GameObject objetive;
    protected override void ExecuteBehaviour()
    {
        if(objetive != null)
        {
            moveDirection = objetive.transform.position - gameObject.transform.position;
            base.ExecuteBehaviour();
        }
    }
}
