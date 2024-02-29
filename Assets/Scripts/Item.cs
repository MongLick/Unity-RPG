using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public void Get()
    {
        Debug.Log("아이템을 인벤토리에 넣기");
        Destroy(gameObject);
    }
}
