using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Counter : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private int requiredRoadLevel;
    [SerializeField] private Transform iconContainer;
    private List<Car> upgradedCars = new();
    private SplinePositioner splinePositioner;
    private Animator animator;

    public int RequiredRoadLevel { get => requiredRoadLevel; }

    public float Distance { get => distance; }

    private void Awake()
    {
        splinePositioner = GetComponent<SplinePositioner>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Car car = other.GetComponent<Car>();
            if (!upgradedCars.Contains(car))
            {
                animator.SetTrigger("Open");
                car.EarnMoney();
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
