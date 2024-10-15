using Unity.PolySpatial;
using UnityEngine;

public class MarkDirtyScript : MonoBehaviour
{

    public RenderTexture renderTexture;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PolySpatialObjectUtils.MarkDirty(renderTexture);
    }
}
