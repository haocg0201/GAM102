using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBgBehaviour : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public Material bg2;
    public Material bg3;
    public Material bg4;
    public Material bg5;

    float offset2;
    float offset3;
    float offset4;
    float offset5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset2 += 0.1f * Time.deltaTime; // Time.deltaTime = 1/60 FPS => cứ 1s mới cộng 0,2f (có Time.deltaTime: cộng theo giây, nếu ko có thì theo frame của update - cộng theo frame)
        // Time.fixedDeltaTime; gọi theo 0,2s (Mặc định của fixUpdate (có thể thay đổi))
        offset3 += 0.2f * Time.deltaTime; // nếu parallax chạy theo không kịp thì * playerController.speed; bởi do tốc độ player tăng nhưng giá trị parallax tăng không đổi nên chậm hơn
        offset4 += 0.5f * Time.deltaTime;
        offset5 += 0.1f * Time.deltaTime;

        bg2.mainTextureOffset = new Vector2(offset2, 0); // giá trị y không thay đổi
        bg3.mainTextureOffset = new Vector2(offset3, 0); // giá trị y không thay đổi
        bg4.mainTextureOffset = new Vector2(offset4, 0); // giá trị y không thay đổi
        bg5.mainTextureOffset = new Vector2(offset5, 0); // giá trị y không thay đổi
    }
}
