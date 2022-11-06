using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuegoScript : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] ballons;
    public Text timerText;

    public static int points;
    private float startTime;
    private bool finnished;
    // Start is called before the first frame update
    void Start()
    {
        finnished = false;
        points = 0;
        startTime = Time.time;
        StartCoroutine(Juego());
    }

    void Update ()
    {
        if (finnished)
            return;

        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }

    IEnumerator Juego()
    {
        if (points < 10)
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < 5; i++)
            {
                int valor = Random.Range(0, 5);
                int color = Random.Range(0, 3);
                if (valor == 0 || valor == 4)
                {
                    Instantiate(ballons[color], spawnPoints[i].position, Quaternion.identity).AddComponent<AutoDestruct>();
                }
            }
            StartCoroutine(Juego());
        } else
        {
            finnished = true;
        }

    }

}

public class AutoDestruct : MonoBehaviour
{

    // Lifetime of gameObject
    // Destroy this gameObject in this many seconds after being instantiated
    public float lifeTime = 5f;

    void Start()
    {
        GameObject.Destroy(this.gameObject, lifeTime);
    }

}

