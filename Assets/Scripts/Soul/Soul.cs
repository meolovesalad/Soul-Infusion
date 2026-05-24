using UnityEngine;

public class Soul : MonoBehaviour
{
    /*
        1. Tách trách nhiệm
        Player:
        chỉ tìm item
        chỉ ra lệnh hút
        Item:
        tự xử lý movement

        Code sạch hơn.
    */

    [SerializeField] private float soulSpeed;
    [SerializeField] private int soulPoint = 25;
    
    private Transform target;

    public void SetTarget(Transform player)
    {
        target = player;
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                target.position, 
                soulSpeed * Time.deltaTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform != target) return;

        PlayerSoul playerTransform =
            collision.GetComponent<PlayerSoul>();

        if (playerTransform != null)
        {
            playerTransform.AddPoint(soulPoint);
        }

        Destroy(gameObject);
    }
}