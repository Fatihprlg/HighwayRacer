using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SplineLevelAdapter : LevelAdapter
{
    public override void SaveAll(LevelModel level, List<ItemModel> worldItemModels)
    {
        EnemyPools.GetPools();
        CollectablePools.GetPools();
        var enemies = worldItemModels.Where(i => i.GetType() == typeof(SplineEnemyModel))
            .Select(a => a as SplineWorldItemModel).ToList();
        var collectables = worldItemModels.Where(i => i.GetType() == typeof(SplineCollectableModel))
            .Select(a => a as SplineWorldItemModel).ToList();
        SaveSplineItemDataArray(level.Collectables, collectables);
        SaveSplineItemDataArray(level.Enemies, enemies);
    }

    public override void LoadLevel(LevelModel level)
    {
        for (int i = 0; i < level.Enemies.Count; i++)
        {
            ActivatePoolItems(level.Enemies[i], WorldItemType.Enemy);
        }
        for (int i = 0; i < level.Collectables.Count; i++)
        {
            ActivatePoolItems(level.Collectables[i], WorldItemType.Colletcable);
        }
    }

    private void ActivatePoolItems(SplineItemDataModel levelItemData, WorldItemType type)
    {
        switch (type)
        {
            case WorldItemType.Enemy:
                var enemy = EnemyPools.GetDeactiveItem<SplineWorldItemModel>(0);
                GetActiveObjectFromPool(levelItemData, enemy);
                break;
            case WorldItemType.Colletcable:
                var collectable = CollectablePools.GetDeactiveItem<SplineWorldItemModel>(0);
                GetActiveObjectFromPool(levelItemData, collectable);
                break;
            default:
                break;
        }
        
    }


    private void GetActiveObjectFromPool(SplineItemDataModel item, SplineWorldItemModel poolObj)
    {
        poolObj.SetPositionAndOffset(item.percentage, item.offset);
        poolObj.gameObject.SetActive(true);
    }

    public void SaveSplineItemDataArray(List<SplineItemDataModel> levelItems, List<SplineWorldItemModel> sceneItems)
    {
        levelItems.Clear();
        for (int i = 0; i < sceneItems.Count; i++)
        {
            levelItems.Add(sceneItems[i].GetData());
        }
    }
}
