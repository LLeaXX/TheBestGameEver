using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;

    public float damage = 10;

    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyFireball", lifetime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveFixedUpdate();
    }
    void MoveFixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }


    private void DestroyFireball()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DamageEnemy(collision);  
        DestroyFireball();
    }

    private void DamageEnemy(Collision collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(damage);
        }
    }
}   