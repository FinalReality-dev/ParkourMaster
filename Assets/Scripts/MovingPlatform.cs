using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject[] targetPoints;
    public int currentPoint = 0;
    public float speed = 5.0f;
    public float waitTime = 4.0f;
    bool nextPointRunnning = false;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoints[currentPoint].transform.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, targetPoints[currentPoint].transform.position) <= 1.0f && !nextPointRunnning){
            nextPointRunnning = true;
            StartCoroutine(WaitForNextPoint());
        }
    }

    IEnumerator WaitForNextPoint(){
        yield return new WaitForSeconds(waitTime);
        currentPoint += 1;
        if(currentPoint >= targetPoints.Length){
            currentPoint = 0;
        }
        nextPointRunnning = false;
    }
}
