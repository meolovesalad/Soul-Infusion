using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private float soulSpeed;

    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        // 1. Get the player's position (the actual target)
        Vector2 targetPosition = PlayerController.Instance.transform.position;

        // 2. Calculate the new position and assign it to the transform, 
        // using Time.deltaTime to ensure smooth, frame-rate independent movement.
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, soulSpeed * Time.deltaTime);
    }
}