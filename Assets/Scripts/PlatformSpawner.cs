using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public Vector2 lastPlatPos;
    public GameObject startingPlatform, platform;
    public float minDistX, minDistY, maxDistX, maxDistY;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPlatPos = startingPlatform.transform.position;
        for (int i = 0; i < 10; i++)
        {
            SpawnPlatform();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlatform()
    {
        float x = Random.Range(minDistX, maxDistX);
        float y = Random.Range(minDistY, maxDistY);

        Vector2 newPos = new Vector2(x + lastPlatPos.x, y + lastPlatPos.y);
        var newPlatform = Instantiate(platform, newPos, Quaternion.identity);
        lastPlatPos = newPos = newPos;
    }
}
