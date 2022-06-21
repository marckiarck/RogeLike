using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float health;
    [SerializeField]
    private float damage;

    private Vector2 movingDirection = Vector2.zero;
    private AttributeSet playerAttributes;

    public float Health { get => playerAttributes.GetAttribute(AttributeNames.HEALTH); set => playerAttributes.SetAttributeSafe(AttributeNames.HEALTH, value); }
    public float Speed { get => playerAttributes.GetAttribute(AttributeNames.SPEED); set => playerAttributes.SetAttributeSafe(AttributeNames.SPEED, value); }
    public float Damage { get => playerAttributes.GetAttribute(AttributeNames.DAMAGE); set => playerAttributes.SetAttributeSafe(AttributeNames.DAMAGE, value); }

    private void Awake()
    {
        playerAttributes = gameObject.AddComponent<AttributeSet>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();

        transform.position += (Vector3)movingDirection * speed * Time.deltaTime;

        //if (Health == 0f)
        //{
        //    Destroy(gameObject);
        //}
        print(playerAttributes.GetAttribute(AttributeNames.HEALTH));
    }

    private void UpdateInput()
    {
        movingDirection = new Vector2(Input.GetAxis("Horizontal"), movingDirection.y);
        movingDirection = new Vector2(movingDirection.x, Input.GetAxis("Vertical"));
    }
}
