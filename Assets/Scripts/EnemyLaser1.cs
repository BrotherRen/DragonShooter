using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLaser1 : MonoBehaviour
{
    [SerializeField]
    private float _eLSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("enemy coroutine start");
        StartCoroutine(DestroyAfterDelay(1.0f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _eLSpeed * Time.deltaTime);
        


    }
}

