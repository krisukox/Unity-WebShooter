using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapControllerScript : MonoBehaviour
{
    public GameObject prefabMap;
    public GameObject prefabLowerBarrier;
    public GameObject prefabUpperBarrier;

    private GameObject[,] lowerBarriers = new GameObject[3, 3];
    private GameObject[,] upperBarriers = new GameObject[3, 3];

    private MapScript[] mapScripts = new MapScript[3];
    private Transform[] mapTransform = new Transform[3];

    private static float score;
    private float lastXCamera;

    private static System.Random ran = new System.Random();

    private Text textScore;

    private Camera mainCamera;

    private Transform playerTransform;

    private float maxCamera;
	void Start ()
    {
        maxCamera = (Camera.main.orthographicSize * 2 * 16) / 9f;
        score = 0;
        mainCamera = Camera.main;
        Text[] allText = FindObjectsOfType<Text>();

        foreach(Text text in allText)
        {
            if (text.tag == "ScoreText")
                textScore = text;
        }

        Transform[] allTransform = FindObjectsOfType<Transform>();
        GameObject obj;

        foreach (Transform transform in allTransform)
        {
            if (transform.tag == "Player")
                playerTransform = transform;
        }

        for (int i = 0;i < 3;i++)
        {
            obj = Instantiate(prefabMap,transform) as GameObject;
            mapScripts[i] = obj.GetComponent<MapScript>();
            mapTransform[i] = obj.GetComponent<Transform>();
            mapScripts[i].mapNumber = i;
        }

        for(int x = 0;x < 3;x++)
        {
            for (int i = 0; i < 3; i++)
            {
                if (x == 0)
                {
                    upperBarriers[x, i] = Instantiate(prefabUpperBarrier, mapTransform[x]) as GameObject;
                    upperBarriers[x, i].transform.localPosition = new Vector3((maxCamera / 6f) * (-2) * (i - 1) / prefabMap.transform.lossyScale.x, 7, 15);
                    lowerBarriers[x, i] = Instantiate(prefabLowerBarrier, mapTransform[x]) as GameObject;
                    lowerBarriers[x, i].transform.localPosition = new Vector3((maxCamera / 6f) * (-2) * (i - 1) / prefabMap.transform.lossyScale.x, -7, 15);
                    upperBarriers[x, i].SetActive(false);
                    lowerBarriers[x, i].SetActive(false);
                }
                else
                {
                    upperBarriers[x, i] = Instantiate(prefabUpperBarrier, mapTransform[x]) as GameObject;
                    upperBarriers[x, i].transform.localPosition = new Vector3((maxCamera / 6f) * (-2) * (i - 1) / prefabMap.transform.lossyScale.x, 7, 15);
                    lowerBarriers[x, i] = Instantiate(prefabLowerBarrier, mapTransform[x]) as GameObject;
                    lowerBarriers[x, i].transform.localPosition = new Vector3((maxCamera / 6f) * (-2) * (i - 1) / prefabMap.transform.lossyScale.x, -7, 15);
                    if (ran.Next(0, 2) == 0)
                    {
                        upperBarriers[x, i].SetActive(false);
                        lowerBarriers[x, i].transform.localPosition = new Vector3(lowerBarriers[x, i].transform.localPosition.x, ran.Next(-6, -4), lowerBarriers[x, i].transform.localPosition.z);
                    }
                    else
                    {
                        lowerBarriers[x, i].SetActive(false);
                        upperBarriers[x, i].transform.localPosition = new Vector3(upperBarriers[x, i].transform.localPosition.x, ran.Next(5, 7), upperBarriers[x, i].transform.localPosition.z);
                    }
                }
            }
        }

        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(9, 10);

        lastXCamera = mainCamera.transform.position.x;
    }
	
	void Update ()
    {
        if (mainCamera.transform.position.x >= maxCamera * 1.3f)
        {
            playerTransform.position -= new Vector3(maxCamera, 0, 0);

            for(int i = 0;i < 3;i++)
            {
                if (mapScripts[i].mapNumber == 0)
                {
                    mapTransform[i].position += new Vector3(maxCamera * 2, 0, 0);
                    for(int x = 0;x < 3;x++)
                    {
                        if (ran.Next(0, 2) == 0)
                        {
                            upperBarriers[i, x].SetActive(false);
                            lowerBarriers[i, x].SetActive(true);
                            lowerBarriers[i, x].transform.localPosition = new Vector3(lowerBarriers[i, x].transform.localPosition.x, ran.Next(-6, -4), lowerBarriers[i, x].transform.localPosition.z);
                        }
                        else
                        {
                            lowerBarriers[i, x].SetActive(false);
                            upperBarriers[i, x].SetActive(true);
                            upperBarriers[i, x].transform.localPosition = new Vector3(upperBarriers[i, x].transform.localPosition.x, ran.Next(5, 7), upperBarriers[i, x].transform.localPosition.z);
                        }
                    }
                }
                else
                    mapTransform[i].position -= new Vector3(maxCamera, 0, 0);
                mapScripts[i].mapNumber--;
                if (mapScripts[i].mapNumber < 0)
                    mapScripts[i].mapNumber = 2;
            }
            lastXCamera = mainCamera.transform.position.x;
        }
        else
        {
            score += mainCamera.transform.position.x - lastXCamera;
            lastXCamera = mainCamera.transform.position.x;
        }
        textScore.text = " " + (int)score;
	}

    public static int Score()
    {
        return (int)score;
    }
}
