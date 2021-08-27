using UnityEngine;

/// <summary>
/// Activates the activate object something on the specified layer mask (like the player)
/// comes within the specified range/radius.
/// </summary>
public class ActivateInRange : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = 0;
    [SerializeField] [Min(0.0f)] private float radius = 10.0f;

    [SerializeField] private GameObject[] objectsToActivate = new GameObject[0];
    [SerializeField] private MonoBehaviour[] monosToActivate = new MonoBehaviour[0];

    private bool isFirstTime = true;
    private bool lastResult = false;


    // Update is called once per frame
    private void Update()
    {
        bool inRange = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        if (inRange == lastResult && !isFirstTime)
        {
            return;
        }
        isFirstTime = false;

        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(inRange);
        }
        foreach (MonoBehaviour mono in monosToActivate)
        {
            mono.enabled = inRange;
        }
        lastResult = inRange;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
