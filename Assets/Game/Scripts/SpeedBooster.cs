using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using Dreamteck.Splines;
using FateGames;
using DG.Tweening;
using TMPro;

public class SpeedBooster : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private SplinePositioner cameraSplinePositioner;
    [SerializeField] private TextMeshProUGUI tapText;
    public static UnityEvent OnStartBoost { get; private set; } = new();
    public static UnityEvent OnStopBoost { get; private set; } = new();

    private double desiredPercent = 0;
    private float a = 0.5f;
    private float b = 0;
    private Tween tapTween = null;

    private void Update()
    {
        cameraSplinePositioner.SetPercent(Mathf.MoveTowards((float)cameraSplinePositioner.GetPercent(), (float)desiredPercent, (Time.deltaTime / 4) * Time.timeScale));
        if (Time.time > b + a)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1, Time.deltaTime);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        desiredPercent = cameraSplinePositioner.GetPercent() + eventData.delta.y * 0.005f;
        //cameraSplinePositioner.SetPercent(cameraSplinePositioner.GetPercent() + eventData.delta.y * 0.005f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        BoostSpeed();
        OnStartBoost.Invoke();
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        OnStopBoost.Invoke();
    }

    private void BoostSpeed()
    {
        Time.timeScale = 2;
        b = Time.time;
        tapTween.Kill();
        tapText.gameObject.SetActive(false);
        tapTween = DOVirtual.DelayedCall(5, () => { tapText.gameObject.SetActive(true); });
        HapticManager.DoHaptic();
    }

}

