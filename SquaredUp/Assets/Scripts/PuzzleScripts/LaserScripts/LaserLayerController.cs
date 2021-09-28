using UnityEngine;

public class LaserLayerController : MonoBehaviour
{
    public LayerMask laserLayerMask => _laserLayerMask;
    [SerializeField] private LayerMask _laserLayerMask = 0;


    public static LaserLayerController instance { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
