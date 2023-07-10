using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    public LevelManager levelManager; // Drag the LevelManager instance here in Unity Editor
    public GameObject target;

    //levelManager.Progress();

    void Update()
    {
        if(target == null)
        {
            return;
        }
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance < 2f)
        {
            Destroy(target);  // 如果你想要销毁第一个物体
            // 或
            levelManager.Progress(); // 如果你想要增加进度
        }
    }

    
}