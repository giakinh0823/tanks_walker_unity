using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomWalkerGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject ballPrefab;
    Timer timer;
    List<GameObject> balls;

    // Start is called before the first frame update
    void Start()
    {
        balls = new List<GameObject>();
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.Finished)
        {
            Bounds bounds = OrthographicBounds(GetComponent<Camera>());
            balls.Add(Instantiate<GameObject>(ballPrefab, new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), bounds.size.z), Quaternion.identity));
            timer.Duration = 2;
            timer.Run();
        }
    }

    private Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
        camera.transform.position,
        new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

}
