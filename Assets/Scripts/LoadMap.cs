using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Scene.instance.NextLevel();

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
