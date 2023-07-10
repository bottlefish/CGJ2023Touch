using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public List<Plant> plants; // 需要在Unity编辑器里手动添加所有的植物到这里
    public DayNightCycleController dayNightCycleController; // 在Unity编辑器里将DayNightCycleController的实例拖拽到这里
    public AudioSource growsound;
    void Update()
    {
        // 检查Cycle是否被触摸，如果没有，直接返回
        if (!dayNightCycleController.isCycleBeingTouched)
        {
            return;
        }

        // 初始时，假设所有的植物都未被触摸
        List<Plant> untouchedPlants = new List<Plant>(plants);

        // 处理每一个触摸事件
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

            if (hit.collider != null)
            {
                foreach (var plant in plants)
                {
                    if (hit.transform == plant.sprite1.parent || hit.transform == plant.sprite2.parent)
                    {
                        // 这个植物被触摸了，更新状态并从未触摸列表中移除
                        plant.ReactToTouch();
                        untouchedPlants.Remove(plant);
                    }
                }
            }
        }

        // 对没有被触摸的植物，停止它们的移动
        foreach (var plant in untouchedPlants)
        {
            plant.StopMovement();
            
        }
    }
}