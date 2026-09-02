using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] float moveSpeed=1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,moveSpeed*Time.deltaTime,0);
        if(transform.position.y > 6.5f)//若物件超出范围
        {
            Destroy(gameObject);//删除物件
            transform.parent.GetComponent<FloorManager>().SpawnFloor();//生成物件
        }
    }
}
