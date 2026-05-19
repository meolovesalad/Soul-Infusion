using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour, IPooling
{
    public static BulletPoolManager Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public Bullet prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<Bullet>> poolDictionary;
    // Lưu lại prefab để dùng khi hết đạn giữa chừng cần tạo thêm
    private Dictionary<string, Pool> poolConfigConfig;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CreateBullet();
    }

    private void CreateBullet()
    {
        poolDictionary = new Dictionary<string, Queue<Bullet>>();
        poolConfigConfig = new Dictionary<string, Pool>();

        foreach (Pool pool in pools)
        {
            string cleanTag = pool.tag.Trim();
            if (poolDictionary.ContainsKey(cleanTag)) continue;

            poolConfigConfig.Add(cleanTag, pool);
            Queue<Bullet> objectPool = new Queue<Bullet>();

            GameObject container = new GameObject(cleanTag + "_Container");
            container.transform.SetParent(this.transform);

            for (int i = 0; i < pool.size; i++)
            {
                Bullet obj = CreateNewBulletInstance(pool, container.transform);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(cleanTag, objectPool);
        }
    }

    // Hàm phụ trợ tạo đạn để tránh lặp code
    private Bullet CreateNewBulletInstance(Pool pool, Transform parent)
    {
        Bullet obj = Instantiate(pool.prefab, parent);
        obj.gameObject.SetActive(false);
        string cleanTag = pool.tag.Trim();
        obj.SetReturnToPool(Bullet => ReturnToPool(cleanTag, Bullet));
        return obj;
    }

    public Bullet GetBullet(string tag, Vector2 position, Quaternion rotation)
    {
        string cleanTag = tag.Trim();

        if (!poolDictionary.ContainsKey(cleanTag))
        {
            Debug.LogError($"Pool với tag {cleanTag} không tồn tại!");
            return null;
        }

        Bullet objectToSpawn;

        // Nếu hết đạn trong kho, tự động tạo thêm viên mới thay vì trả về null
        if (poolDictionary[cleanTag].Count == 0)
        {
            Transform containerTransform = transform.Find(cleanTag + "_Container");
            objectToSpawn = CreateNewBulletInstance(poolConfigConfig[cleanTag], containerTransform);
        }
        else
        {
            objectToSpawn = poolDictionary[cleanTag].Dequeue();
        }

        objectToSpawn.transform.SetPositionAndRotation(position, rotation);
        objectToSpawn.gameObject.SetActive(true);

        return objectToSpawn;
    }

    public void ReturnToPool(string tag, Bullet Bullet)
    {
        string cleanTag = tag.Trim();
        Bullet.gameObject.SetActive(false);

        if (poolDictionary.ContainsKey(cleanTag))
        {
            poolDictionary[cleanTag].Enqueue(Bullet);
        }
    }
}