using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DealerButtonController : MonoBehaviour
{
    [SerializeField] private DealerButton upgraderButton, roadButton, carButton, incomeButton, nextCarButton, frequencyButton;

    public void UpdateUpgraderButton(int cost, bool canAfford, bool roadNeeded, bool maxedOut)
    {
        upgraderButton.Button.gameObject.SetActive(!roadNeeded && !maxedOut);
        if (roadNeeded || maxedOut) return;
        upgraderButton.Text.text = GenerateMoneyText(cost);
        upgraderButton.Button.interactable = canAfford;
    }

    public void UpdateRoadButton(int cost, bool canAfford, bool maxedOut)
    {
        if (maxedOut)
        {
            roadButton.Text.text = "MAX";
        }
        else
        {
            roadButton.Text.text = GenerateMoneyText(cost);
        }
        roadButton.Button.interactable = canAfford && !maxedOut;
    }

    public void UpdateCarButton(List<Car> cars, int cost, bool canAfford)
    {
        if (cars.Count == 0 && !canAfford)
            carButton.Text.text = "FREE";
        else
            carButton.Text.text = GenerateMoneyText(cost);
        carButton.Button.interactable = (cars.Count == 0 && !canAfford) || canAfford;
    }
    public void UpdateNextCarButton(int cost, bool canAfford, bool maxedOut)
    {
        if (maxedOut)
        {
            nextCarButton.Text.text = "MAX";
        }
        else
        {
            nextCarButton.Text.text = GenerateMoneyText(cost);
        }
        nextCarButton.Button.interactable = canAfford && !maxedOut;
    }
    public void UpdateIncomeButton(int cost, bool canAfford, bool roadNeeded, bool maxedOut)
    {
        if (maxedOut)
        {
            incomeButton.Text.text = "MAX";
        }
        else if (roadNeeded)
        {
            incomeButton.Text.text = "Upgrade Road";
        }
        else
        {
            incomeButton.Text.text = GenerateMoneyText(cost);
        }
        incomeButton.Button.interactable = canAfford && !roadNeeded && !maxedOut;
    }

    public void UpdateFrequencyButton(int cost, bool canAfford, bool maxedOut)
    {
        if (maxedOut)
        {
            frequencyButton.Text.text = "MAX";
        }
        else
        {
            frequencyButton.Text.text = GenerateMoneyText(cost);
        }
        frequencyButton.Button.interactable = canAfford && !maxedOut;
    }

    private string GenerateMoneyText(int money)
    {
        string text;
        if (money >= 1000000 && money - (money / 1000000) * 1000000 >= 10000) text = (money / 1000000f).ToString("0.00").Replace(',', '.') + "M";
        else if (money >= 1000000) text = (money / 1000000f).ToString("0").Replace(',', '.') + "M";
        else if (money >= 10000 && money - (money / 10000) * 10000 >= 100) text = (money / 1000f).ToString("0.0").Replace(',', '.') + "K";
        else if (money >= 10000) text = (money / 1000f).ToString("0").Replace(',', '.') + "K";
        else if (money >= 1000 && money - (money / 1000) * 1000 >= 10) text = (money / 1000f).ToString("0.00").Replace(',', '.') + "K";
        else if (money >= 1000) text = (money / 1000f).ToString("0").Replace(',', '.') + "K";
        else text = money.ToString();
        return text;
    }

    [System.Serializable]
    public struct DealerButton
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button button;

        public TextMeshProUGUI Text { get => text; }
        public Button Button { get => button; }
    }
}
