using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PriceTag : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;
    private Transform mainCamera;
    public Transform Transform { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main.transform;
        Transform = transform;
    }

    private void Update()
    {
        Transform.LookAt(mainCamera.position);
    }

    public void SetPrice(int price)
    {
        priceText.text = "$" + price;
    }
}
