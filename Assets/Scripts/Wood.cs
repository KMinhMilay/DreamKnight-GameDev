using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider2D _collider;
    private bool _playerOnPlatform;
    private void Start()
    {
        _collider=GetComponent<Collider2D>();
    }
     private void Update()
    {
        if(_playerOnPlatform && Input.GetAxis("Vertical") < 0)
        {
            _collider.enabled=false;
            StartCoroutine(EnabledCollider());
        }
    }
    private IEnumerator EnabledCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled=true;
    }

    // Update is called once per frame
    private void SetPlayerOnPlatform(Collision2D other,bool value)
    {
        var player =other.gameObject.GetComponent<PlayerMovement>();
        if(player!=null)
        {
            _playerOnPlatform=value;
        }
    }
  private void OnCollisionEnter2D(Collision2D other) {
        SetPlayerOnPlatform(other,value:true);
    }
    
     private void OnCollisionExit2D(Collision2D other) {
        SetPlayerOnPlatform(other,value:true);
    }
  
}
