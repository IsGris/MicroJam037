using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class MonsterWaveController : MonoBehaviour
{
    [Serializable]
    public struct Wave
    {
        public List<FoodType> NeededFoods;
        public float WaveTime;
        public int GoldOnPass;
        public Dictionary<FoodType, int> getNeededFoodsDict() =>
			NeededFoods
				.GroupBy(food => food)
				.ToDictionary(group => group.Key, group => group.Count());
	}

    [SerializeField] List<Wave> needs;
    private MonsterStats monster;
    public int CurrWave = 0;
    
    void Start()
    {
        monster = MonsterStats.Instance;
        monster.NeedsInCurrentWave = needs[CurrWave].getNeededFoodsDict();
        monster.Needs = needs[CurrWave].getNeededFoodsDict();
        monster.WaveTime = needs[CurrWave].WaveTime;
        monster.StartWaveTime = Time.time;
        monster.OnNeedsUpdate?.Invoke();
        monster.OnWaveClear += WaveClearHandler;
	}

    void WaveClearHandler()
    {
        PlayerStats.Instance.Gold += needs[CurrWave].GoldOnPass;
        CurrWave++;
        if (CurrWave > needs.Count - 1)
            CurrWave--;
		monster.NeedsInCurrentWave = needs[CurrWave].getNeededFoodsDict();
		monster.Needs = needs[CurrWave].getNeededFoodsDict();
		monster.WaveTime = needs[CurrWave].WaveTime;
		monster.StartWaveTime = Time.time;
		monster.OnNeedsUpdate?.Invoke();
	}
}
