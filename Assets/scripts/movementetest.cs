using UnityEngine;
using System.IO;
using System.Text;


public class MoverObjeto : MonoBehaviour
{
    public GeneralShape shape;
    public Sprite[] sprites;

    public SpriteRenderer spriteRenderer;

    public int id;

    void Start()
    {
        gameObject.name = shape.number.ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();
        id = shape.id;
        spriteRenderer.sprite = sprites[id];
        float x = shape.x;
        float y = shape.y;
        transform.position = new Vector3((x / 10) - 30, (y / 10) - 20, 0);
    }

    void Update()
    {
        setFigura(SocketClient.Shapes[shape.number]);
        if (id != shape.id)
        {
            id = shape.id;
            spriteRenderer.sprite = sprites[id];
        }
        float x = shape.x;
        float y = shape.y;
        transform.position = new Vector3((x / 10) - 30, (y / 10) - 20, 0);
    }

    void setFigura(GeneralShape data)
    {
        this.shape = data;
    }
}
