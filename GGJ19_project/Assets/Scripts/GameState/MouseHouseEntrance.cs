using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MouseHouseEntrance : MonoBehaviour
{
    public static Action PlayerHitEntranceEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != Tags.Player) { return; }

        if(PlayerHitEntranceEvent != null)
        {
            PlayerHitEntranceEvent();
        }
    }
}
