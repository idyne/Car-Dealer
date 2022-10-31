using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FateGames;
using Dreamteck.Splines;
using TMPro;

[RequireComponent(typeof(DealerButtonController))]
public class Dealer : MonoBehaviour
{
    [SerializeField] private Animator garageAnimator;
    private float baseCarSpawningPeriod = 5;
    private float carSpawningPeriod = 5;
    private DealerButtonController buttonController;
    public static Dealer Instance { get; private set; } = null;
    private List<Upgrader> upgraders;
    private List<Counter> counters;
    private Road road;
    private List<Car> cars = new();
    private Queue<int> carsInQueue = new();
    [SerializeField] private TextMeshProUGUI queueCountText;
    private List<Upgrader> activeUpgraders = new();
    private List<Counter> activeCounters = new();
    private int startMoney = 0;
    public static int income = 0;
    [Header("Cost Tables")]
    [SerializeField] private CostTable upgraderBuyingCostTable;
    [SerializeField] private CostTable carBuyingCostTable;
    [SerializeField] private CostTable roadLengtheningCostTable;
    [SerializeField] private CostTable incomeIncreasingCostTable;
    [SerializeField] private CostTable carLevelCostTable;
    [SerializeField] private CostTable frequencyIncreasingCostTable;

    private int CarLevel { get => PlayerProgression.PlayerData.CarLevel; set => PlayerProgression.PlayerData.CarLevel = value; }
    private int UpgraderBuyingLevel { get => PlayerProgression.PlayerData.UpgraderBuyingLevel; set => PlayerProgression.PlayerData.UpgraderBuyingLevel = value; }
    private int RoadLengthLevel { get => PlayerProgression.PlayerData.RoadLengthLevel; set => PlayerProgression.PlayerData.RoadLengthLevel = value; }
    private int IncomeLevel { get => PlayerProgression.PlayerData.IncomeLevel; set => PlayerProgression.PlayerData.IncomeLevel = value; }
    private int FrequencyLevel { get => PlayerProgression.PlayerData.FrequencyLevel; set => PlayerProgression.PlayerData.FrequencyLevel = value; }
    private int CarBuyingCost { get => carBuyingCostTable.Costs[Mathf.Clamp(CarLevel, 0, carBuyingCostTable.Costs.Count - 1)]; }
    private int CarLevelBuyingCost { get => carLevelCostTable.Costs[Mathf.Clamp(CarLevel, 0, carLevelCostTable.Costs.Count - 1)]; }
    private int IncomeIncreasingCost { get => incomeIncreasingCostTable.Costs[Mathf.Clamp(IncomeLevel, 0, incomeIncreasingCostTable.Costs.Count - 1)]; }
    private int RoadLengtheningCost { get => roadLengtheningCostTable.Costs[Mathf.Clamp(RoadLengthLevel, 0, roadLengtheningCostTable.Costs.Count - 1)]; }
    private int UpgraderBuyingCost { get => upgraderBuyingCostTable.Costs[Mathf.Clamp(UpgraderBuyingLevel, 0, upgraderBuyingCostTable.Costs.Count - 1)]; }
    private int FrequencyIncreasingCost { get => frequencyIncreasingCostTable.Costs[Mathf.Clamp(FrequencyLevel, 0, frequencyIncreasingCostTable.Costs.Count - 1)]; }

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
            Instance = this;
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
        #endregion
        upgraders = new(FindObjectsOfType<Upgrader>());
        upgraders.Sort((a, b) => Mathf.CeilToInt(a.Distance - b.Distance));
        foreach (Upgrader upgrader in upgraders)
            upgrader.gameObject.SetActive(false);

        counters = new(FindObjectsOfType<Counter>());
        counters.Sort((a, b) => Mathf.CeilToInt(a.Distance - b.Distance));
        foreach (Counter counter in counters)
            counter.gameObject.SetActive(false);
        road = FindObjectOfType<Road>();
        buttonController = GetComponent<DealerButtonController>();
        PlayerProgression.OnMoneyChanged.AddListener((money) =>
        {
            bool roadNeededForUpgrader = activeUpgraders.Count >= upgraders.Count || RoadLengthLevel < upgraders[activeUpgraders.Count].RequiredRoadLevel;
            bool roadNeededForIncome = activeCounters.Count >= counters.Count || RoadLengthLevel < counters[activeCounters.Count].RequiredRoadLevel;
            buttonController.UpdateUpgraderButton(UpgraderBuyingCost, CanAfford(UpgraderBuyingCost), roadNeededForUpgrader, upgraderBuyingCostTable.Costs.Count - 1 <= UpgraderBuyingLevel);
            //buttonController.UpdateCarButton(cars, CarBuyingCost, CanAfford(CarBuyingCost));
            buttonController.UpdateIncomeButton(IncomeIncreasingCost, CanAfford(IncomeIncreasingCost), roadNeededForIncome, incomeIncreasingCostTable.Costs.Count - 1 <= IncomeLevel);
            buttonController.UpdateFrequencyButton(FrequencyIncreasingCost, CanAfford(FrequencyIncreasingCost), frequencyIncreasingCostTable.Costs.Count - 1 <= FrequencyLevel);
            buttonController.UpdateRoadButton(RoadLengtheningCost, CanAfford(RoadLengtheningCost), roadLengtheningCostTable.Costs.Count - 1 <= RoadLengthLevel);
            buttonController.UpdateNextCarButton(CarLevelBuyingCost, CanAfford(CarLevelBuyingCost), carLevelCostTable.Costs.Count - 1 <= CarLevel);
        });
    }
    private void Start()
    {
        Initialize();
        //Camera.main.depthTextureMode = DepthTextureMode.Depth;
        road.Slider.maxValue = carSpawningPeriod;
        StartCoroutine(AddCarCoroutine());
        startMoney = PlayerProgression.MONEY;
        //InvokeRepeating(nameof(PrintIncome), 20, 20);
    }

    private void Update()
    {
        //AddCarFromQueue();

    }

    private void PrintIncome()
    {
        print(income);
        print("Income: " + (income) / 20f);
        income = 0;
    }

    private IEnumerator AddCarCoroutine()
    {
        AddCar(CarLevel);
        yield return new WaitForSeconds(carSpawningPeriod);
        yield return AddCarCoroutine();
    }

    private void Initialize()
    {
        SetRoadLength(RoadLengthLevel);
        for (int i = 0; i < UpgraderBuyingLevel; i++)
            AddUpgrader();
        for (int i = 0; i < IncomeLevel; i++)
            AddCounter();
        SetCarIncomeLevel(IncomeLevel);
        SetFrequency();
        PlayerProgression.MONEY += PlayerProgression.PlayerData.Debt;
        PlayerProgression.PlayerData.Debt = 0;
    }

    public void LevelUpCar()
    {
        if (!CanAfford(CarLevelBuyingCost)) return;
        CarLevel++;
        PlayerProgression.MONEY = 0;
        PlayerProgression.PlayerData.FrequencyLevel = 0;
        PlayerProgression.PlayerData.UpgraderBuyingLevel = 0;
        PlayerProgression.PlayerData.RoadLengthLevel = 1;
        PlayerProgression.PlayerData.IncomeLevel = 1;
        HapticManager.DoHaptic();
        SceneManager.FinishLevel(true);
    }

    public void BuyCar()
    {
        if (!CanAfford(CarBuyingCost) && cars.Count > 0) return;
        int cost = !CanAfford(CarBuyingCost) ? 0 : CarBuyingCost;
        if (cars.Count > 0 && cars[^1].GetDistance() < 4)
            AddCarToQueue();
        else
            AddCar(CarLevel);
        PlayerProgression.MONEY -= cost;
        PlayerProgression.PlayerData.Debt += cost;
        HapticManager.DoHaptic();
    }

    public void IncreaseFrequency()
    {
        if (!CanAfford(FrequencyIncreasingCost)) return;
        int cost = FrequencyIncreasingCost;
        FrequencyLevel++;
        SetFrequency();
        PlayerProgression.MONEY -= cost;
        HapticManager.DoHaptic();
    }
    private void SetFrequency()
    {
        carSpawningPeriod = baseCarSpawningPeriod - EaseOutQuad(FrequencyLevel / (float)frequencyIncreasingCostTable.Costs.Count) * 4.5f;
        //print("Car Spawning Period: " + carSpawningPeriod);
        road.Slider.maxValue = carSpawningPeriod;
    }
    
    private float EaseOutQuad(float x)
    {
        x = Mathf.Clamp(x, 0, 1);
        return 1 - (1 - x) * (1 - x);
    }
    private void AddCar(int carLevel)
    {
        Car car = ObjectPooler.Instance.SpawnFromPool("Car " + carLevel, road.CarSpawnPoint.position, road.CarSpawnPoint.rotation).GetComponent<Car>();
        int price = carBuyingCostTable.Costs[carLevel];
        car.SetPrice(price);
        car.OnEndReached = () => { SellCar(car); };
        car.StartFollowing();
        garageAnimator.SetTrigger("Open");
        road.Slider.value = 0;
        cars.Add(car);
    }

    private void AddCarToQueue()
    {
        carsInQueue.Enqueue(CarLevel);
        queueCountText.text = carsInQueue.Count.ToString();
    }

    private void AddCarFromQueue()
    {
        if (!(carsInQueue.Count > 0 && (cars.Count == 0 || cars[^1].GetDistance() >= 4))) return;
        AddCar(carsInQueue.Dequeue());
        queueCountText.gameObject.SetActive(carsInQueue.Count != 0);
        queueCountText.text = carsInQueue.Count.ToString();
    }

    public void SellCar(Car car)
    {
        RemoveCar(car);
        foreach (Upgrader upgrader in activeUpgraders)
        {
            upgrader.RemoveCar(car);
        }
        foreach (Counter counter in activeCounters)
        {
            counter.RemoveCar(car);
        }
        car.Sell();
        //ObjectPooler.Instance.SpawnFromPool("Money Effect", car.Transform.position, Quaternion.identity);
        //HapticManager.DoHaptic();
    }
    private bool CanAfford(int cost)
    {
        return PlayerProgression.MONEY >= cost;
    }
    public void LengthenRoad()
    {
        if (!CanAfford(RoadLengtheningCost)) return;
        int cost = RoadLengtheningCost;
        SetRoadLength(++RoadLengthLevel);
        PlayerProgression.MONEY -= cost;
        HapticManager.DoHaptic();
    }

    private void SetRoadLength(int lengthLevel)
    {
        List<float> carDistances = new();
        for (int i = 0; i < cars.Count; i++)
        {
            carDistances.Add(cars[i].GetDistance());
            cars[i].StopFollowing();
        }
        road.SetLength(lengthLevel);
        foreach (Upgrader upgrader in activeUpgraders)
        {
            upgrader.SetDistance();
        }
        foreach (Counter counter in activeCounters)
        {
            counter.SetDistance();
        }
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].StartFollowing();
        }
    }

    public void BuyUpgrader()
    {
        if (!CanAfford(UpgraderBuyingCost) || activeUpgraders.Count >= upgraders.Count || RoadLengthLevel < upgraders[activeUpgraders.Count].RequiredRoadLevel) return;
        AddUpgrader();
        int cost = UpgraderBuyingCost;
        UpgraderBuyingLevel++;
        PlayerProgression.MONEY -= cost;
        HapticManager.DoHaptic();
    }
    private void AddUpgrader()
    {
        Upgrader upgrader = upgraders[activeUpgraders.Count];
        upgrader.Activate();
        activeUpgraders.Add(upgrader);
    }

    private void AddCounter()
    {
        Counter counter = counters[activeCounters.Count];
        counter.Activate();
        activeCounters.Add(counter);
    }
    public void IncreaseIncome()
    {
        if (!CanAfford(IncomeIncreasingCost) || activeCounters.Count >= counters.Count || RoadLengthLevel < counters[activeCounters.Count].RequiredRoadLevel) return;
        AddCounter();
        int cost = IncomeIncreasingCost;
        SetCarIncomeLevel(++IncomeLevel);
        PlayerProgression.MONEY -= cost;
        HapticManager.DoHaptic();
    }
    private void SetCarIncomeLevel(int incomeLevel)
    {
        Car.IncomeLevel = incomeLevel;
    }
    private void RemoveCar(Car car)
    {
        cars.Remove(car);
    }
}

