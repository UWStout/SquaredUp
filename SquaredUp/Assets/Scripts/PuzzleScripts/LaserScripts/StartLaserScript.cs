using UnityEngine;

public class StartLaserScript : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = 1;
    [SerializeField] private LineRenderer lineRenderer = null;

    private Lazer lazer = null;


    private void Awake()
    {
        lazer = new Lazer(lineRenderer, transform.position, transform.up, layerMask);
    }
    private void Update()
    {
        lazer.UpdateLazer();
    }
}
