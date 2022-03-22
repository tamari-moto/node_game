using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class camera : MonoBehaviour
{

    private bool scrollStartFlg = true; // スクロールが始まったかのフラグ
    private Vector2 scrollStartPos = new Vector2(); // スクロールの起点となるタッチポジション
    private static float SCROLL_DISTANCE_CORRECTION = 0.8f; // スクロール距離の調整

    private Vector2 touchPosition = new Vector2(); // タッチポジション初期化
    private Collider2D collide2dObj = null; // タッチ位置にあるオブジェクトの初期化
    Camera _camera ;
    // Use this for initialization
    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographicSize = 10;
        Vector3 pos = this.transform.position;
        pos.x = 0;
        pos.y = 0;
        pos.z = -1;

        this.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        hantei();
        zoom();
    }

    void zoom()
    {
        var scroll = Input.mouseScrollDelta.y;
        if (_camera.orthographicSize < 5) _camera.orthographicSize = 5;
        _camera.orthographicSize -= scroll;
    }
    public void small()
    {
        _camera.orthographicSize += 3;
    }
    public void big()
    {
        _camera.orthographicSize -= 3;
    }


    void hantei()
    {
        if (Input.GetMouseButton(0))
        {

            touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            collide2dObj = Physics2D.OverlapPoint(touchPosition);
            if (!collide2dObj&& scrollStartFlg)
            {
                // タッチした場所に何もない場合、スクロールフラグをtrueに
                drag();
            }
            else
            {
                //scrollStartFlg = false;
            }
        }
        else
        {
            // タッチを離したらフラグを落とし、スクロール開始位置も初期化する 
            scrollStartFlg = true;
            scrollStartPos = new Vector2();
        }
    }
    void drag()
    {
        
        if (scrollStartPos.x == 0.0f)
        {
            // スクロール開始位置を取得
            scrollStartPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            Vector2 touchMovePos = touchPosition;
            if (scrollStartPos.x != touchMovePos.x)
            {
                // 直前のタッチ位置との差を取得する
                float diffPos = SCROLL_DISTANCE_CORRECTION * (touchMovePos.x - scrollStartPos.x);
                float diffPosy = SCROLL_DISTANCE_CORRECTION * (touchMovePos.y - scrollStartPos.y);

                Vector3 pos = this.transform.position;
                pos.x -= diffPos;
                pos.y -= diffPosy;
                pos.z = -1;

                this.transform.position = pos;
                scrollStartPos = touchMovePos;
            }
        }
    }
}