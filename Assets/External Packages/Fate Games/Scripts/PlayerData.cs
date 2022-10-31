using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FateGames
{
    [System.Serializable]
    public class PlayerData : Data
    {
        public int CurrentLevel = 1;
        public int Money = 0;
        public int UpgraderBuyingLevel = 0;
        public int RoadLengthLevel = 1;
        public int IncomeLevel = 1;
        public int CarLevel = 0;
        public int Debt = 0;
        public int FrequencyLevel = 0;
    }

}
