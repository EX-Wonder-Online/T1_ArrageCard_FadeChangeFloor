using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*オブジェクトを段々薄くするプログラム
 * 初めの色を記録 color
 * O秒かけて消える(マテリアルのアルファの値を小さくする) inTime
 * 待機 intervalTime
 * O秒かけて表示(マテリアルのアルファの値を大きくする) outTime
 * 待機 intervalTime
 */

public class FadeController : MonoBehaviour
{

    [SerializeField]
    private Renderer rend;
    [SerializeField]
    private MeshCollider meshCollider;
    [SerializeField]
    private float intervalTime, inTime, outTime;
    private Color color;
    private float nowTime;
    [SerializeField] [Range(0, 1)] private float border;

    // Start is called before the first frame update
    void Start()
    {
        color = rend.material.color;
        StartCoroutine(Move());
        
    }

    // Update is called once per frame
    void Update()
    {
        //meshCollider.enabled = (color.a >= 0.4f);
        if(color.a >= border) {
            meshCollider.enabled = true;
        } else {
            meshCollider.enabled = false;
        }
    }

    IEnumerator Move() {
        
        while (true) {
            yield return FadeIn(); // 段々薄く
            yield return new WaitForSeconds(intervalTime);
            yield return FadeOut(); // 段々濃く
            yield return new WaitForSeconds(intervalTime);
        }

    }

    IEnumerator FadeIn() {
        float diff = 0, rate = 0;
        nowTime = Time.timeSinceLevelLoad;
        while (rate <= 1.0f) {
            diff = Time.timeSinceLevelLoad - nowTime;
            rate = diff / inTime;
            color.a = rate;
            rend.material.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut() {
        float diff = 0, rate = 0;
        nowTime = Time.timeSinceLevelLoad;
        while (rate <= 1.0f) {
            diff = Time.timeSinceLevelLoad - nowTime;
            rate = diff / outTime;
            color.a = 1 - rate;
            rend.material.color = color;
            yield return null;
        }
    }
}
