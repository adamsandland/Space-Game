using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float Hull_Health = 100f;
    void Start()
    {
        
    }

    void Update(){
        if(Hull_Health<=0f){
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision){
        Debug.Log("Hit Something!");
        if(collision.gameObject.tag != "Player" || collision.gameObject.tag != "Bullet"){
            Hull_Health-=50f;
        }else if(collision.gameObject.tag == "Bullet"){
            Hull_Health-=5f;
        }
    }
}
