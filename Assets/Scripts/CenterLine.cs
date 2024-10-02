using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterLine : MonoBehaviour
{
    [SerializeField] private GameObject centerLine;

    public void SetCenterLineVisibility(bool isVisible)
    {
        centerLine.SetActive(isVisible);
    }
}