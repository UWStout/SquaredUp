using UnityEngine;

/// <summary>Base class for animations that swap between states with the given data.</summary>
/// <typeparam name="T">Data to have for states</typeparam>
public abstract class StateChangeAnimation<T> : MonoBehaviour
{
    // If we want the transitions to be random or sequential
    [SerializeField] private bool randomOrder = false;
    // Delay before changing
    [SerializeField] private float waitTime = 0f;

    // States to change between
    private T[] states = null;
    // The index of the previous state
    private int prevIndex = 0;

    // Called 1st
    // Initialization
    private void Start()
    {
        states = Initialize();
        prevIndex = 0;
        ChangeNextState();
    }


    /// <summary>Initialize the states that will be changed between.</summary>
    protected abstract T[] Initialize();
    /// <summary>What to do when the given state is supposed to be changed to.</summary>
    protected abstract void ChangeStateCall(T stateToChangeTo);

    /// <summary>Starts morphing the current shape to the next one.</summary>
    private void ChangeNextState()
    {
        int nextIndex;
        // Pick random index
        if (randomOrder)
        {
            nextIndex = PickRandomMeshIndex();
        }
        // Get next index in list
        else
        {
            nextIndex = NextMeshIndex();
        }

        prevIndex = nextIndex;
        ChangeStateCall(states[nextIndex]);
    }
    /// <summary>Start morphing after the delay</summary>
    protected void ChangeAfterDelay()
    {
        Invoke("ChangeNextState", waitTime);
    }

    /// <summary>Picks a random index for state that is not the previous index.</summary>
    private int PickRandomMeshIndex()
    {
        // Create a list of indices except for the last index
        int[] tempRandomList = new int[states.Length - 1];
        int count = 0;
        for (int i = 0; i < states.Length; ++i)
        {
            if (i != prevIndex)
            {
                tempRandomList[count] = i;
                ++count;
            }
        }
        // Return a random index in the list
        return tempRandomList[Random.Range(0, tempRandomList.Length)];
    }

    /// <summary>Returns the index of the state after the previous state. Loops the list so 0 is after the last index.</summary>
    private int NextMeshIndex()
    {
        if (prevIndex + 1 < states.Length)
        {
            return prevIndex + 1;
        }
        else
        {
            return 0;
        }
    }
}
