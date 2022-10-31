using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

[RequireComponent(typeof(SplinePositioner))]
public class Upgrader : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private int requiredRoadLevel;
    [SerializeField] private Transform iconContainer;
    private List<Car> upgradedCars = new();
    private SplinePositioner splinePositioner;
    private Transform cameraTransform;

    public int RequiredRoadLevel { get => requiredRoadLevel; }
    public float Distance { get => distance; }

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        splinePositioner = GetComponent<SplinePositioner>();
        
    }

    private void Update()
    {
        //iconContainer.LookAt(cameraTransform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Car car = other.GetComponent<Car>();
            if (!upgradedCars.Contains(car))
            {
                car.Upgrade();
                upgradedCars.Add(car);
            }
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        SetDistance();
    }

    public void SetDistance()
    {
        splinePositioner.SetDistance(distance);
    }
    public void RemoveCar(Car car)
    {
        upgradedCars.Remove(car);
    }
}
