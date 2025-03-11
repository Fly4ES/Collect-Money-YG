using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public bool music = true;

        public int money = 0;
        public int level = 0;
        public int levelText = 1;

        public List<bool> saveSkin = new List<bool>();
        public List<bool> selectionSkin = new List<bool>();

        public long serverTime;
        public bool hasRewardSaved = false;
        public List<bool> saveReward = new List<bool>();
        public List<bool> saveTookTheReward = new List<bool>();
    }  
}