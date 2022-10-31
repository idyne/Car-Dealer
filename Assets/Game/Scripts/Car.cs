using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using FateGames;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SplineFollower))]
public class Car : MonoBehaviour, IPooledObject
{
    [SerializeField] private Transform meshContainer;
    [SerializeField] private int carLevel;
    private Rigidbody rb;
    private int price = 0;
    private int upgradeLevel = 0;
    private SplineFollower splineFollower;
    private PriceTag priceTag;
    public Action OnEndReached;
    public static int IncomeLevel = 1;
    public int Price { get => price; }
    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
        rb = GetComponent<Rigidbody>();
        splineFollower = GetComponent<SplineFollower>();
        rb.isKinematic = true;
        priceTag = GetComponentInChildren<PriceTag>();
        splineFollower.spline = Road.Instance.SplineComputer;
        splineFollower.onEndReached += _OnEndReached;
        StopFollowing();
    }

    public void Upgrade()
    {
        if (meshContainer.childCount <= upgradeLevel + 1) return;
        meshContainer.GetChild(upgradeLevel).gameObject.SetActive(false);
        upgradeLevel++;
        EarnMoney();
        meshContainer.GetChild(upgradeLevel).gameObject.SetActive(true);
    }

    public void EarnMoney()
    {
        if (GameManager.Instance.State == GameState.COMPLETE_SCREEN) return;
        int gain = Mathf.CeilToInt((EaseInSine((upgradeLevel + 1) / 10f)) * 150);
        gain = Mathf.Clamp(gain, 3, 150);
        PlayerProgression.MONEY += gain;
        Dealer.income += gain;
        LevitatingText levitatingText = ObjectPooler.Instance.SpawnFromPool("Levitating Money Text", Transform.position, Quaternion.identity).GetComponent<LevitatingText>();
        levitatingText.SetText("$" + gain);
    }

    private float EaseInCubic(float x)
    {
        x = Mathf.Clamp(x, 0, 1);
        return x * x * x;
    }

    private float EaseInSine(float x)
    {
        x = Mathf.Clamp(x, 0, 1);
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }

    public float GetDistance()
    {
        float result = splineFollower.spline.CalculateLength() * ((float)splineFollower.GetPercent());
        return result;
    }

    public void StopFollowing()
    {
        splineFollower.enabled = false;
        splineFollower.follow = false;
    }

    public void StartFollowing()
    {
        splineFollower.enabled = true;
        splineFollower.follow = true;

    }

    private void _OnEndReached(double percent)
    {
        OnEndReached();
    }

    public void Sell()
    {
        //PlayerProgression.MONEY += price;
        //LevitatingText levitatingText = ObjectPooler.Instance.SpawnFromPool("Levitating Money Text", Transform.position, Quaternion.identity).GetComponent<LevitatingText>();
        //levitatingText.SetText("$" + price);
        //PlayerProgression.PlayerData.Debt -= price;
        gameObject.SetActive(false);
    }

    public void SetPrice(int price)
    {
        this.price = price;
        priceTag.SetPrice(price);
    }

    public void OnObjectSpawn()
    {
        splineFollower.SetDistance(0);
        for (int i = 0; i < meshContainer.childCount; i++)
            meshContainer.GetChild(i).gameObject.SetActive(false);
        upgradeLevel = 0;
        meshContainer.GetChild(upgradeLevel).gameObject.SetActive(true);
        StopFollowing();
    }
}
