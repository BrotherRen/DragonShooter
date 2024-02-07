using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _lSpeed = 8.0f;
    //private float startTime;

    //speed variable 8f
    void Start()
    {
        StartCoroutine(DestroyAfterDelay(2.0f));
    }
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }


    void Update()
    {
        //translate laser up

        transform.Translate(Vector3.right * _lSpeed * Time.deltaTime);

        //if laser position is greater than 8 on y 
        //destroy the object

       // if (Time.time - startTime >= 3.0f)
      //  {
         //   Destroy(gameObject);
       // }
    }
}
