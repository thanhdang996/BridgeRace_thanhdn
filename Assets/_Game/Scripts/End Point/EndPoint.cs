using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public int level;
    public bool isWinPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterProps character))
        {
            if (level - 1 != character.AtFloor) return;

            character.HandleNextFloor();
        }
    }
}
