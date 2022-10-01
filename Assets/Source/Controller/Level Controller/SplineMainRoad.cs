using System;
using Dreamteck.Splines;
using UnityEngine;

public class SplineMainRoad : ObjectModel
{
    public SplineComputer Computer => computer;
    [SerializeField] private SplineComputer computer;
    [SerializeField] private SplineMesh splineMesh;
    [SerializeField] private bool showSplineComputer;


    private void OnValidate()
    {

        if (showSplineComputer)
            Computer.hideFlags = HideFlags.None;
        else
        {
            computer.hideFlags = HideFlags.HideInInspector;
        }
    }

    private void Reset()
    {
        if (gameObject.GetComponent<SplineComputer>() != null)
        {
            computer = gameObject.GetComponent<SplineComputer>();
        }
        else
        {
            computer = gameObject.AddComponent<SplineComputer>();
        }

        if (gameObject.GetComponent<SplineMesh>() != null)
        {
            splineMesh = gameObject.GetComponent<SplineMesh>();
        }
        else
        {
            splineMesh = gameObject.AddComponent<SplineMesh>();
        }

        computer.hideFlags = HideFlags.HideInInspector;
    }
}