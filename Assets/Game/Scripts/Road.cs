using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dreamteck.Splines;
using FateGames;

[RequireComponent(typeof(SplineComputer))]
public class Road : MonoBehaviour
{
    public static Road Instance { get; private set; }
    public SplineComputer SplineComputer { get; private set; }
    [SerializeField] private SplinePositioner spawnMesh, endMesh;
    [SerializeField] private List<Vector3> points;
    [SerializeField] private Transform carSpawnPoint;
    [SerializeField] private Slider slider;
    [SerializeField] private Transform sliderContainer;
    private Transform cameraTransform;
    public Transform CarSpawnPoint { get => carSpawnPoint; }
    public Slider Slider { get => slider; }

    private void Awake()
    {
        if (!Instance) Instance = this;
        else { DestroyImmediate(gameObject); return; };
        SplineComputer = GetComponent<SplineComputer>();
        cameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        sliderContainer.LookAt(cameraTransform);
        slider.value += Time.deltaTime;
    }
    public void SetLength(int lengthLevel)
    {
        int numberOfPoints = Mathf.Clamp((lengthLevel + 1) * 2, 0, points.Count);
        SetSplinePoints(numberOfPoints);
        spawnMesh.RebuildImmediate();
        endMesh.RebuildImmediate();
        //print(SplineComputer.CalculateLength());
    }

    private void SetSplinePoints(int numberOfPoints)
    {
        SplinePoint[] splinePoints = new SplinePoint[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            splinePoints[i] = new SplinePoint(points[i]);
        }
        SplineComputer.SetPoints(splinePoints);
        SplineComputer.RebuildImmediate(forceUpdateAll: true);
    }
}
