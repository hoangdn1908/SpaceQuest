using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    float backgroundImageWidth;
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundImageWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    
    void Update()
    {
        MoveBackground();
    }
    public void MoveBackground() 
    {
        float moveX = (moveSpeed * PlayerController.instance.boost) * Time.deltaTime;
        transform.position += new Vector3(moveX, 0f);
        if(Mathf.Abs(transform.position.x) > backgroundImageWidth) 
        {
            transform.position = new Vector3(0f, transform.position.y);
        }
    }
}
