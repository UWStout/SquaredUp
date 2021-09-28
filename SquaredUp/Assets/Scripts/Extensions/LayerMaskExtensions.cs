using UnityEngine;

public static class LayerMaskExtensions
{
    public static LayerMask AddAdditionalLayer(this LayerMask originalLayerMask, int additionalLayer)
    {
        return originalLayerMask | (1 << additionalLayer);
    }
    public static LayerMask RemoveLayer(this LayerMask originalLayerMask, int removeLayer)
    {
        return originalLayerMask & ~(1 << removeLayer);
    }
}
