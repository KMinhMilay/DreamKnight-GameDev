using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour
{
    // Start is called before the first frame update
    public float delaySecond=2;
    public string nameScene="Map 2";
    void Start()
    {
        
    }private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "Untagged")
        {
            collision.gameObject.SetActive(false);
            ModeSelect();
        }
    }
    public void ModeSelect()
    {
        StartCoroutine(routine:LoadAfterDelay());
    }
    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delaySecond);
        SceneManager.LoadScene(nameScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
