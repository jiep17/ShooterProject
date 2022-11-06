using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisionScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;
    public Text PointsValue;

    public void Shoot()
    {

        RaycastHit hit;

        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {

            if (hit.transform.name == "balloon1(Clone)" || hit.transform.name == "balloon2(Clone)" || hit.transform.name == "balloon3(Clone)")
            {
                JuegoScript.points += 1;
                PointsValue.text = JuegoScript.points.ToString();
                Destroy(hit.transform.gameObject);
                Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal)).AddComponent<AutoDestruct>();
            }
        }
    }

}
