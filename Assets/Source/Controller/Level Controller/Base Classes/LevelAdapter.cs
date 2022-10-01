using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[DisallowMultipleComponent]
public abstract class LevelAdapter : ObjectModel
{
    [SerializeField] protected MultiplePoolModel EnemyPools;
    [SerializeField] protected MultiplePoolModel CollectablePools;
    public abstract void SaveAll(LevelModel level, List<ItemModel> worldItemModels);

    public abstract void LoadLevel(LevelModel level);
}
