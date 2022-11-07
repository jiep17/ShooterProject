using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuegoScript : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] ballons;
    public Text timerText;
    public Button bShoot;
    public Button bJugarDeNuevo;
    public Image iApuntador;
    public Image iGanaste;
    public Image iPerdiste;
    public Text PointsValue;


    public static int points;
    private float startTime;
    private bool finnished;
    // Start is called before the first frame update
    void Start()
    {
        iGanaste.gameObject.SetActive(false);
        iPerdiste.gameObject.SetActive(false);
        bJugarDeNuevo.gameObject.SetActive(false);

        bShoot.gameObject.SetActive(true);
        iApuntador.gameObject.SetActive(true);

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
        int inMinutes = ((int)t / 60);
        if(inMinutes == 1)
        {
            // Ha perdido el juego
            terminarJuego(1);
            return;
        }

        string minutes = inMinutes.ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }


    public void reiniciarJuego()
    {
        iGanaste.gameObject.SetActive(false);
        iPerdiste.gameObject.SetActive(false);
        bJugarDeNuevo.gameObject.SetActive(false);

        bShoot.gameObject.SetActive(true);
        iApuntador.gameObject.SetActive(true);

        finnished = false;
        points = 0;
        PointsValue.text = "00";
        startTime = Time.time;
        StartCoroutine(Juego());
    }

    void terminarJuego(int type)
    {
        finnished = true;
        bShoot.gameObject.SetActive(false);
        iApuntador.gameObject.SetActive(false);
        if(type == 1)
        {
            iPerdiste.gameObject.SetActive(true);
        }
        else
        {
            iGanaste.gameObject.SetActive(true);
        }
        bJugarDeNuevo.gameObject.SetActive(true);

    }

    IEnumerator Juego()
    {
        if (points < 20 && !finnished)
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
            // Si pasa los 10 puntos entonces ha ganado.
            terminarJuego(2);
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

