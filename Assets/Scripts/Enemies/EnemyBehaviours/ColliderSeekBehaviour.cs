using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSeekBehaviour : SeekBehaviour
{
    protected override void ExecuteBehaviour()
    {
        if (objetive == null || ExecuteEnemyAvoidance())
        {
            return;
        }

        Vector2 raycastDirection = objetive.transform.position - gameObject.transform.position;

        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, raycastDirection.normalized, raycastDirection.magnitude);
        bool collisionDetected = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && (hit.collider.gameObject.tag == "Wall"))
            {
                collisionDetected = true;
                break;
            }
        }

        if(collisionDetected)
        {
            Vector2 verticaDirection = new Vector2(raycastDirection.x, 0f);
            RaycastHit2D[] verticalHits = Physics2D.RaycastAll(gameObject.transform.position, verticaDirection.normalized, raycastDirection.magnitude);
            collisionDetected = false;
            foreach (RaycastHit2D hit in verticalHits)
            {
                if (hit.collider != null && (hit.collider.gameObject.tag == "Wall"))
                {
                    collisionDetected = true;
                    break;
                }
            }

            if (collisionDetected == false)
            {
                moveDirection = new Vector2(raycastDirection.x, 0f);
                Move();
                return;
            }
            else
            {
                Vector2 horizontalDirection = new Vector2(0f, raycastDirection.y);
                RaycastHit2D[] horizontalHits = Physics2D.RaycastAll(gameObject.transform.position, horizontalDirection.normalized, raycastDirection.magnitude);
                collisionDetected = false;
                foreach (RaycastHit2D hit in horizontalHits)
                {
                    if (hit.collider != null && (hit.collider.gameObject.tag == "Wall"))
                    {
                        collisionDetected = true;
                        break;
                    }
                }

                if(collisionDetected == false)
                {
                    moveDirection = new Vector2(0f, raycastDirection.y);
                    Move();
                    return;
                }
            }
        }
        else
        {
            base.ExecuteBehaviour();
        }
        
    }

    public bool ExecuteEnemyAvoidance()
    {
        Vector2 raycastDirection = objetive.transform.position - gameObject.transform.position;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(gameObject.transform.position, 1f, raycastDirection.normalized, Time.deltaTime);

        bool collisionDetected = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && (hit.collider.gameObject.tag == "Enemy" && hit.collider.gameObject != gameObject))
            {
                Vector2 enemyDirection = hit.collider.gameObject.transform.position - gameObject.transform.position;
                Vector2 objectiveDirection = objetive.transform.position - gameObject.transform.position;

                if (enemyDirection.x * objectiveDirection.x > 0f)
                {
                    raycastDirection = new Vector2(0f, raycastDirection.y);
                }

                if (enemyDirection.y * objectiveDirection.y > 0f)
                {
                    raycastDirection = new Vector2(raycastDirection.x, 0f);
                }


                collisionDetected = true;
                break;
            }
        }

        if (collisionDetected)
        {
            moveDirection = raycastDirection;
            Move();
            return true;
        }

        return false;
    }
}
