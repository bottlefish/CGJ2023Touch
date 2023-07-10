using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameEndCheck : MonoBehaviour
{
    public UnityEvent successEvent;

    public UnityEvent failedEvent;

    public AudioSource winsound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �������жϣ�����Ŀ��������ʤ������֮ʧ��
        if (collision.gameObject.name == "TargetStar")
        {
            winsound.Play();
            //successEvent?.Invoke();
            StartCoroutine(golevel("Success"));

        }
        else
        {
            //failedEvent?.Invoke();
            SceneManager.LoadScene("Failed");
        }
    }
    IEnumerator golevel(string levelname)
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(levelname);


    }
}
