using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float bossHp = 1000;
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    public Text bosshp;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        slider.value = bossHp;
        bosshp.text = bossHp + " / 1000";
    }

    public void TakeDamage(int Damage)
    {
        if(bossHp <= 1)
        {
            Die();
        }
        
        if(bossHp >= 1)
        {
            bossHp -= Damage;
            StartCoroutine(color());
        }
    }

    void Die(){
        Destroy(this.gameObject);
        DataManager.Instance.isWin = true;
    }

    IEnumerator color()
    {
        sprite.color = new Color32(255,0,0,255);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color32(255,255,255,255);
    }

}
