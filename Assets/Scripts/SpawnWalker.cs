using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnWalker : MonoBehaviour
{
    [SerializeField]
    GameObject walkerPrefab;
    Timer timer;
    List<GameObject> walkers;
    int number = 0;

    void Start()
    {
        walkers = new List<GameObject>();
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Finished)
        {
            GameObject walker = Instantiate<GameObject>(walkerPrefab);
            walker.name = "ball #" + ++number;
            walker.transform.position = transform.localPosition;
            walker.gameObject.SetActive(true);
            walkers.Add(walker);
            timer.Duration = 2;
            timer.Run();
        }
    }
}
