using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections; // Para IEnumerator y corutinas
using System.Collections.Generic; // Para otras colecciones genéricas si es necesario
using System.Globalization;


public class SocketClient : MonoBehaviour
{
    public static List<GeneralShape> Shapes;
    public GameObject[] prefabShape; // Prefab de la figura que queremos generar


    public static SocketClient instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        try
        {

            Application.targetFrameRate = 24;
            Application.runInBackground = true;
            Shapes = new List<GeneralShape>();


            StartCoroutine(actualizar());
        }
        catch (Exception e)
        {
            Debug.LogError("Error de conexión: " + e.Message);
        }


    }
    private IEnumerator actualizar()
    {

        while (true)
        {
            try
            {
                // Dirección y puerto del servidor (debe coincidir con el servidor Python)
                string serverAddress = "127.0.0.1";
                int port = 10001;

                // Crear un socket TCP para conectarse al servidor
                TcpClient client = new TcpClient(serverAddress, port);

                // Obtener el stream del cliente para recibir datos
                NetworkStream stream = client.GetStream();

                // Buffer para leer los datos
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Convertir los bytes recibidos en una cadena
                //string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Separar los valores 'x' y 'y' utilizando la coma como delimitador

                if ((message == "") || (message == "[]"))
                {
                    Debug.Log("No data");
                }
                else
                {
                    string message1 = message.Replace("[", "");
                    string message2 = message1.Replace("]", "");
                    string[] formas = message2.Split(", '?'");
                    Array.Resize(ref formas, formas.Length - 1);

                    if (!(formas.Length <= Shapes.Count))
                    {


                        int count = 0;
                        foreach (var forma in formas)
                        {
                            if (count == 0)
                            {
                                string[] data = forma.Split(',');

                                GeneralShape figura = new GeneralShape(int.Parse(data[0]), int.Parse(data[1]), float.Parse(data[2], CultureInfo.InvariantCulture), float.Parse(data[3], CultureInfo.InvariantCulture), float.Parse(data[4], CultureInfo.InvariantCulture));
                                Shapes.Add(figura);

                                Vector3 position = new Vector3(figura.x, figura.y, 0);
                                GameObject prefabElejido = prefabShape[figura.id];
                                GameObject newShape = Instantiate(prefabElejido, position, Quaternion.identity);

                                MoverObjeto shapeScript = newShape.GetComponent<MoverObjeto>();
                                shapeScript.numero = figura.number;

                            }
                            else
                            {
                                string formaAux = forma.Remove(0, 1);
                                string[] data = formaAux.Split(',');
                                GeneralShape figura = new GeneralShape(int.Parse(data[0]), int.Parse(data[1]), float.Parse(data[2], CultureInfo.InvariantCulture), float.Parse(data[3], CultureInfo.InvariantCulture), float.Parse(data[4], CultureInfo.InvariantCulture));
                                Shapes.Add(figura);
                                Vector3 position = new Vector3(figura.x, figura.y, 0);
                                GameObject prefabElejido = prefabShape[figura.id];
                                GameObject newShape = Instantiate(prefabElejido, position, Quaternion.identity);
                                MoverObjeto shapeScript = newShape.GetComponent<MoverObjeto>();
                                shapeScript.numero = figura.number;
                            }
                            count = count + 1;

                        }
                    }
                    else
                      if (!(formas.Length == Shapes.Count))
                    {

                        Shapes.Clear();
                        foreach (var shape in Shapes)
                        {
                            Destroy(GameObject.Find(shape.number.ToString()));
                        }
                    }

                }




                stream.Close();
                client.Close();


            }
            catch (Exception e)
            {

                Debug.LogError("Error de conexión: " + e.Message);
            }
            yield return new WaitForSeconds(0f);
        }
    }

}


