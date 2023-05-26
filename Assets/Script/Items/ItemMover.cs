using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    public int ItemId;
    public float MoveSpeed = 1;

    public ItemSpwanner itemSpwanner;

    private void Start() {
        
    }
    private void Update() {
        MoveSpeed = itemSpwanner._currentItemSpeed;
        Move();
    }
    private void OnEnable() {
        // LeanTween.move(gameObject, new Vector3(transform.position.x, -10, transform.position.z), MoveSpeed).setEaseLinear().setOnComplete(()=>{gameObject.SetActive(false);});
    }

    public void Move(){

        transform.Translate(-transform.up*MoveSpeed*Time.deltaTime);
        if(transform.position.y <= -10){
            SendBackToItemSpwannerList();  
        }
    }

    public void SendBackToItemSpwannerList(){
        if (!itemSpwanner._item[ItemId]._item.Contains(this.transform))
            {
                itemSpwanner._item[ItemId]._item.Add(this.transform);
                transform.position = Vector3.zero;
                this.transform.gameObject.SetActive(false);
                itemSpwanner.SetParant(this.transform,ItemId);
            }
    }
}
