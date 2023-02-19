using UnityEngine;

public class ExitDoor : InteractionObject
{
    public void TryUse(LiftedThing thing)
    {
        if (thing is ExitKey) {
            Debug.Log("Leveel Completed");
        }
    }
}
