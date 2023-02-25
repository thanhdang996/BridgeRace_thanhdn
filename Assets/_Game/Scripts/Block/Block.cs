using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private ColorType color;

    [SerializeField] private Vector3 oriPos;


    public bool IsSameColor(ColorType colorType) => this.color == colorType;

    public void SavePos()
    {
        oriPos = transform.localPosition;
    }
    public void LoadPos()
    {
        transform.localPosition = oriPos;
        transform.rotation = Quaternion.identity;
    }
}
