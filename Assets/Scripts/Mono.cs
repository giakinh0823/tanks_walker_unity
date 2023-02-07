using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mono : MonoBehaviour
{
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = RandomTarget(Camera.main);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            target = RandomTarget(Camera.main);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
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

    private Vector3 RandomTarget(Camera camera)
    {
        Bounds bounds = OrthographicBounds(camera);
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y), -Camera.main.transform.position.z);
    }
}
