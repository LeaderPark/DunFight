using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    public int enemyHP = 100;
    
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int Damage)
    {
        if(enemyHP <= 0)
        {
            Die();
        }
        else if(enemyHP > 0)
        {
            enemyHP -= Damage;
            Vector2 attack = new Vector2 (2f, 7f);
            rigid.AddForce(attack, ForceMode2D.Impulse);
        }
    }

    void Die(){
        Destroy(this.gameObject);
    }
}
