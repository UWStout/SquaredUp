using UnityEngine;

public class LazerRemoveObstructionModifier : LazerColliderModifier
{
    [SerializeField] private GameObject obstacle = null;
    [SerializeField] private bool colorSpecific = false;
    [SerializeField] private Material requiredColor = null;


    public override void HandleLazer(Lazer lazer, Vector2 hitPoint, Vector2 incidentDirection)
    {
        if (colorSpecific)
        {
            if (lazer.GetLineColor() == requiredColor.color)
            {
                obstacle.SetActive(false);
            }
        }
        else
        {
            obstacle.SetActive(false);
        }
    }

    public override void LazerRemoved(Lazer lazer)
    {
        obstacle.SetActive(true);
    }
}
