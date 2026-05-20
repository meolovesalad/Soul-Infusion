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
    private PlayerTransform playerTransform;
    private Transform target;

    private void Awake()
    {
        playerTransform = FindFirstObjectByType<PlayerTransform>();
    }

    public void SetTarget(Transform player)
    {
        target = player;
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
    private void Update()
    {
        MoveToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            playerTransform.CurrentPoint += soulPoint;
            Destroy(gameObject);
        }
    }
}