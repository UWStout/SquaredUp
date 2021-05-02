using UnityEngine;
using UnityEditor;

/// <summary>Script to assist in editing the door editors.</summary>
public class DoorOpenerEditor : MonoBehaviour
{
    // Door opener's door controller script to rotate
    [SerializeField] private DoorController doorOpener = null;

    /// <summary>Rotates the door opener and changes its target.</summary>
    public void RotateDoorOpener()
    {
        doorOpener.Interact();
    }
    /// <summary>Update the target this door opener has.</summary>
    public void UpdateDoorTarget()
    {
        doorOpener.UpdateTarget();
    }
}


/// <summary>CustomEditor for the DoorOpenerEditor to give a button to rotate the door opener.</summary>
[CustomEditor(typeof(DoorOpenerEditor))]
public class DoorOpenerEditorUnityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DoorOpenerEditor myDoorOpenerEditor = (DoorOpenerEditor)target;

        if (GUILayout.Button("Rotate"))
        {
            myDoorOpenerEditor.RotateDoorOpener();
        }
        if (GUILayout.Button("Update Target"))
        {
            myDoorOpenerEditor.UpdateDoorTarget();
        }
    }
}