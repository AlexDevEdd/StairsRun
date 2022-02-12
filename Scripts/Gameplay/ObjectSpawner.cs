using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{ 
    protected Transform SpawnObject(Transform prefab, Transform spawnPoint)
    {
        Transform newRoad = Instantiate(prefab, spawnPoint);
        return newRoad;
    }
   
}
