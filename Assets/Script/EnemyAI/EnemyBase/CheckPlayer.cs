using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    private LayerMask PlayerLayer;
    private LayerMask GroundLayer;

    private Collider2D coll;

    void Start()
    {
        PlayerLayer = LayerMask.GetMask("Player");
        GroundLayer = LayerMask.GetMask("Ground");
        coll = GetComponent<Collider2D>();
    }

    public bool CheckLeftRightPlayer(float RayLength)
    {
        bool hasObstacleLeft = Physics2D.Raycast(coll.bounds.center, Vector2.left, RayLength, PlayerLayer);
        bool hasObstacleRight = Physics2D.Raycast(coll.bounds.center, Vector2.right, RayLength, PlayerLayer);
        return hasObstacleLeft || hasObstacleRight;
    }


    public bool CheckPlayerInFront(float rayLength)
    {
        Vector2 direction = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(
            coll.bounds.center + (Vector3)(direction * 0.1f),
            direction,
            rayLength,
            PlayerLayer | GroundLayer
        );
        if (hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & GroundLayer) != 0)
            {
                return false;
            }

            if (((1 << hit.collider.gameObject.layer) & PlayerLayer) != 0)
            {
                return true;
            }
        }

        return false;
    }
    public bool CheckPlayerInBack(float rayLength)
    {
        Vector2 direction = -transform.right;
        RaycastHit2D hit = Physics2D.Raycast(
            coll.bounds.center + (Vector3)(direction * 0.1f),
            direction,
            rayLength,
            PlayerLayer | GroundLayer
        );
        if (hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & GroundLayer) != 0)
            {
                return false;
            }

            if (((1 << hit.collider.gameObject.layer) & PlayerLayer) != 0)
            {
                return true;
            }
        }

        return false;
    }

    public void IgnorePlayerCollider(GameObject player)
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);
    }
    public bool heightDiff(GameObject player)
    {
        return Mathf.Abs(player.transform.position.y - transform.position.y) > 1;
    }

    public bool DistanceCheck(GameObject player, int far)
    {
        return Vector2.Distance(transform.position, player.transform.position) > far;

    }

    public int GetPlayerSide(GameObject player)
    {
        if (transform.position.x > player.transform.position.x)
            return 1;
        else
            return -1;
    }

}