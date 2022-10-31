using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using FateGames;

public class LevitatingText : MonoBehaviour, IPooledObject
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float delay = 1;
    private Transform cameraTransform;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        _transform.LookAt(cameraTransform);
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void OnObjectSpawn()
    {
        canvasGroup.alpha = 1;
        DOVirtual.DelayedCall(delay, () => { gameObject.SetActive(false); });
        DOTween.To((val) =>
        {
            canvasGroup.alpha = val;
        }, 1, 0, 0.5f * Time.timeScale).SetEase(Ease.InQuint);
        _transform.DOMoveY(_transform.position.y + 4, 0.5f * Time.timeScale);
    }
}
