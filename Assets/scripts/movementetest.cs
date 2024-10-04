using UnityEngine;
using System.IO;
using System.Text;


public class MoverObjeto : MonoBehaviour
{
    public int numero;

    void Start()
    {
        gameObject.name = numero.ToString();
        float x = SocketClient.Shapes[numero].x;
        float y = SocketClient.Shapes[numero].y;
        transform.position = new Vector3(x / 10, y / 10, 0);
    }

    void Update()
    {
        float x = SocketClient.Shapes[numero].x;
        float y = SocketClient.Shapes[numero].y;
        transform.position = new Vector3(x / 10, y / 10, 0);
    }

    void setNumero(int data)
    {
        this.numero = data;
    }
}
