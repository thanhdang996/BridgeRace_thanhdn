using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    [SerializeField] DataColor dataColor;
    [SerializeField] private ColorType color;

    [SerializeField] private MeshRenderer mesh;

    [SerializeField] private BoxCollider wall;

    public bool IsSameColor(ColorType colorType) => this.color == colorType;
    public void ChangeColor(ColorType colorType)
    {
        this.color = colorType;
        mesh.material = dataColor.GetMat(colorType);
    }

    public void TurnOnWall()
    {
        wall.enabled = true;
    }

    public void TurnOffWall()
    {
        wall.enabled = false;
    }

}
