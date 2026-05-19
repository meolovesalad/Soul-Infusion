using UnityEngine;

public interface IPooling
{
    Bullet GetBullet(string tag, Vector2 position, Quaternion rotation);
}
