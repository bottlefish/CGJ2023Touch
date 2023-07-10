using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RbCollector : MonoBehaviour
{
    public List<GameObject> stars;

    public Dictionary<string, Vector2> forceMap = new Dictionary<string, Vector2>();

    private void Start()
    {
        GameObject[] stones = GameObject.FindGameObjectsWithTag("Stone");
        GameObject[] starObjs = GameObject.FindGameObjectsWithTag("Star");

        stars.AddRange(starObjs);
        stars.AddRange(stones);
    }

    /// </summary>
    /// <param name="mass">源质量</param>
    /// <param name="position">源位置</param>
    /// <param name="name">源天体名称</param>
    public void AddGrivity(float mass, Vector2 position, string name)
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if (stars[i].gameObject == null || stars[i].gameObject.IsDestroyed())
            {
                continue;
            }
            // 向自己之外的所有天体施加重力
            if (stars[i].gameObject.name != name) 
            {
                string currentName = stars[i].gameObject.name;
                // 计算重力
                Vector2 gravity = stars[i].GetComponent<GeneralGravity>().CalcGrivity(mass, position);
                // 加入哈希表
                if (forceMap.ContainsKey(currentName))
                {
                    forceMap[currentName] += gravity;
                }
                else
                {
                    forceMap.Add(currentName, gravity);
                }
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if (stars[i].gameObject == null || stars[i].gameObject.IsDestroyed())
            {
                continue;
            }

            string currentName = stars[i].gameObject.name;
            GeneralGravity currentGravity = stars[i].GetComponent<GeneralGravity>();

            if (forceMap.ContainsKey(currentName))
            {
                currentGravity.AddGravity(forceMap[currentName]);
            }
        }

        forceMap = new Dictionary<string, Vector2>();
    }
}
