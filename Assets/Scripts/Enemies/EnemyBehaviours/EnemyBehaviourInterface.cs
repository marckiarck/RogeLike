using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourInterface : MonoBehaviour
{

    void Update()
    {
        ExecuteBehaviour();
    }

    protected virtual void ExecuteBehaviour()
    {
        print("Behaviour interface");
    }
}
