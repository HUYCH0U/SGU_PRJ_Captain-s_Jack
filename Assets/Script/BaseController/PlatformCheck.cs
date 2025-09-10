using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCheck : MonoBehaviour
{
  [SerializeField] private LayerMask JumpAbleLayer;
  private LayerMask Enemy;
  private LayerMask ContainObject;
  private Collider2D coll;
  void Start()
  {
    coll = GetComponent<Collider2D>();
    Enemy = LayerMask.GetMask("Enemy");
    ContainObject = LayerMask.GetMask("ItemContainObject");
  }
  public bool CheckGround(float RayLength)
  {
    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, RayLength, JumpAbleLayer);
  }
  public bool PlayerCheckGround(float RayLength)
  {
    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, RayLength, Enemy)
    || Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, RayLength, JumpAbleLayer) ||
    Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, RayLength, ContainObject);
  }

  public bool CheckGroundInFront(float RayLength)
  {
    Vector2 direction = -transform.right;
    return Physics2D.Raycast(coll.bounds.center + (Vector3)(direction * 0.1f), direction, RayLength, JumpAbleLayer);
  }
  public bool CheckGroundInBack(float RayLength)
  {
    Vector2 direction = transform.right;
    return Physics2D.Raycast(coll.bounds.center + (Vector3)(direction * 0.1f), direction, RayLength, JumpAbleLayer);
  }

  public bool CheckLeftRight(float RayLength)
  {
    bool hasObstacleLeft = Physics2D.Raycast(coll.bounds.center, Vector2.left, RayLength, JumpAbleLayer);
    bool hasObstacleRight = Physics2D.Raycast(coll.bounds.center, Vector2.right, RayLength, JumpAbleLayer);
    return hasObstacleLeft || hasObstacleRight;
  }
}
