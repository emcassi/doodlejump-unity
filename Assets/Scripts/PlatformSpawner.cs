using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public Vector2 lastPlatPos;
    public GameObject startingPlatform, platform;
    public float minDistX, minDistY, maxDistX, maxDistY;
    public PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPlatPos = startingPlatform.transform.position;
        SpawnPlatforms(25);
    }

    void Update()
    {
        if (player.screenBounds.y > lastPlatPos.y)
        {
            SpawnPlatforms(25);
        }
    }

    void SpawnPlatforms(int num)
    {
        for (var i = 0; i < num; i++)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        float x = Random.Range(minDistX, maxDistX);
        float y = Random.Range(minDistY, maxDistY);

        Vector2 newPos = new Vector2(x, y + lastPlatPos.y);
        var newPlatform = Instantiate(platform, newPos, Quaternion.identity, this.transform);
        lastPlatPos = newPos = newPos;
    }
}
