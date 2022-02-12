using UnityEngine;

public class Block : MonoBehaviour
{
    public float GetBlockHight()
    {
        var bounds = GetComponentInChildren<Renderer>().bounds;
        if (bounds == null) return 0f;
        return bounds.size.y;
    }

   
}

