using UnityEngine;

public class code : MonoBehaviour
{
    // variables para el tamaño y velocidad aleatoria
    public float minSize = 1.0f;
    public float maxSize = 3.0f;
    public float minSpeed = 50.0f;
    public float maxSpeed = 200.0f;

    //Componente para añadir fuersza a los obstaculos
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Generar un tamaño aleatorio
        float randomSize = Random.Range(minSize, maxSize);
        //Cambiar el tamaño del obstaculo
        transform.localScale = new Vector3(randomSize, randomSize, 2.0f);
        //Obtener el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        //Aplicar una fuerza aleatoria en el eje X
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        //Generar una direccion aleatoria
        Vector2 randonDireccion = Random.insideUnitCircle;
        //Aplicar la fuerza
        rb.AddForce(randonDireccion * randomSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
