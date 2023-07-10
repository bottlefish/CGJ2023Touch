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
    /// <param name="mass">Դ����</param>
    /// <param name="position">Դλ��</param>
    /// <param name="name">Դ��������</param>
    public void AddGrivity(float mass, Vector2 position, string name)
    {
        for (int i = 0; i < stars.Count; i++)
        {
            if (stars[i].gameObject == null || stars[i].gameObject.IsDestroyed())
            {
                continue;
            }
            // ���Լ�֮�����������ʩ������
            if (stars[i].gameObject.name != name) 
            {
                string currentName = stars[i].gameObject.name;
                // ��������
                Vector2 gravity = stars[i].GetComponent<GeneralGravity>().CalcGrivity(mass, position);
                // �����ϣ��
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
