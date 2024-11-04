using Unity.PolySpatial;
using UnityEngine;

public class MarkDirtyScript : MonoBehaviour
{

    public RenderTexture renderTexture;
    void Update()
    {
        PolySpatialObjectUtils.MarkDirty(renderTexture);
    }
}
