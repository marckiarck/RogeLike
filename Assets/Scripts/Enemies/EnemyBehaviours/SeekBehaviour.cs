using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : MoveBehaviour
{
    [SerializeField]
    protected GameObject objetive;

    public GameObject Objetive { get => objetive; set => objetive = value; }

    protected override void ExecuteBehaviour()
    {
        if(objetive != null)
        {
            moveDirection = objetive.transform.position - gameObject.transform.position;
            base.ExecuteBehaviour();
        }
    }
}
