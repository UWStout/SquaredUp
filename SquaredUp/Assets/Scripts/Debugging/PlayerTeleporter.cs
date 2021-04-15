using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    private void OnEnable()
    {
        InputEvents.HackerTeleportEvent += Teleport;
    }
    private void OnDisable()
    {
        InputEvents.HackerTeleportEvent -= Teleport;
    }

    private void Teleport(int value)
    {
        switch (value)
        {
            // Temple
            case 1:
                transform.position = new Vector3(-246.5f, 260.4f);
                break;
            // Green Pickup
            case 2:
                transform.position = new Vector3(-196.5f, 202.9f);
                break;
            // Color Puzzle 1
            case 3:
                transform.position = new Vector3(-160f, 198.4f);
                break;
            // Pre-connector
            case 4:
                transform.position = new Vector3(64f, 285.4f);
                break;
            // Ice Puzzle
            case 5:
                transform.position = new Vector3(88f, 292.4f);
                break;
            // Light Puzzle
            case 6:
                transform.position = new Vector3(126f, 290.9f);
                break;
            // Guard Puzzle 1
            case 7:
                transform.position = new Vector3(199f, 334.4f);
                break;
            // Door Puzzle
            case 8:
                transform.position = new Vector3(196f, 388.9f);
                break;
            // Guard Puzzle 2
            case 9:
                transform.position = new Vector3(310f, 466.4f);
                break;
            // Color Puzzle 2
            case 10:
                transform.position = new Vector3(517f, 555.4f);
                break;
            // King
            case 11:
                transform.position = new Vector3(298f, 575.4f);
                break;
            default:
                break;
        }
    }
}
