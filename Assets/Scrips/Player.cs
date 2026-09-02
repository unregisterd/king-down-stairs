using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;//在游戏中做出的改变并不会储存下来
    GameObject currentFloor;//记录当前踩上的阶梯
    [SerializeField] int Hp;//记录血量
    [SerializeField] GameObject HpBar;//血条
    [SerializeField] Text scoreText;
    int score;//存放分数
    float scoreTime;//存放游戏时间
    Animator anim;
    SpriteRenderer renderer;
    //AudioSource hurtSound;
    AudioSource walkSound;//走路的音效
    [SerializeField] GameObject replayButton;//暂停按钮
    // Start is called before the first frame update
    void Start()//只有在开始的时候才执行
    {
        Hp = 10;
        score = 0;
        scoreTime = 0f;
        anim=GetComponent<Animator>();
        renderer=GetComponent<SpriteRenderer>();
        //hurtSound = GetComponent<AudioSource>();
        walkSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed*Time.deltaTime,0,0);
            renderer.flipX = false;
            walkSound.Play();
        }else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed*Time.deltaTime,0,0);
            renderer.flipX = true;
            walkSound.Play();
        }
        UpdateScore();   
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Normal")
        {
            //判断法线(精确碰撞范围，当物件的Component为BoxColloder2D时较为适用)
            if(other.contacts[0].normal == new Vector2(0f, 1f))
            {
                
                currentFloor=other.gameObject;
                ModifyHp(1);
            }    
        }else if(other.gameObject.tag == "Nails")
        {
            if(other.contacts[0].normal == new Vector2(0f, 1f))
            {
                
                currentFloor=other.gameObject;
                ModifyHp(-3);
                anim.SetTrigger("hurt");
                other.gameObject.GetComponent<AudioSource>().Play();//播放音效    
            }   
        }else if(other.gameObject.tag == "Ceil")
        {
            
            currentFloor.GetComponent<EdgeCollider2D>().enabled = false;//改变物件功能
            ModifyHp(-3);
            anim.SetTrigger("hurt");
            //other.gameObject.GetComponent<AudioSource>().Play();//播放音效
        }
            
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "DeathLine")
        {
            other.gameObject.GetComponent<AudioSource>().Play();//播放音效
            Die();
        }
    }
    void ModifyHp(int num)
    {
        Hp += num;
        if(Hp > 10)
        {
            Hp = 10;
        }else if(Hp <= 0)
        {
            Hp = 0;
            Die();
        }
        UpdateHpBar();//更新血条
    }
    void UpdateHpBar()
    {
        for(int i=0; i < HpBar.transform.childCount; i++)
        {
            if (Hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);//将子物件设置为不可见
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void UpdateScore()
    {
        scoreTime += Time.deltaTime;//呼叫的间隔时间
        if(scoreTime > 2f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text = score.ToString();
        }
    }
    void Die()
    {
        Time.timeScale=0f;
        replayButton.SetActive(true);
    }

    public void Replay()
    {
        Time.timeScale=1f;
        //重新载入场景
        SceneManager.LoadScene("SampleScene");
    }
}
