using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class ControlJugador : MonoBehaviour
{
    public float fuerzaEmpuje = 1f;
    public float maxVelocidad = 5f;
    public GameObject boosterFlame;
    public UIDocument UIDocument;    // Referencia al documento UI para mostrar el score
    public GameObject Explota;//    Referencia al efecto de explosión
    private float timeGame = 0f;
    private float score = 0f;
    private float multiplicaScore = 10f;
    private Button reiniciaboton;
    Rigidbody2D rb;
    private Label scoreText;    // Referencia al Label de score en el UI Document
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = UIDocument.rootVisualElement.Q<Label>("ScoreLabel");// Obtener el Label de score del UI Document
        reiniciaboton = UIDocument.rootVisualElement.Q<Button>("reiniciaBoton");// Obtener el botón de reinicio del UI Document
        reiniciaboton.style.display = DisplayStyle.None; // Ocultar el botón al inicio
        reiniciaboton.clicked += ReiniciaJuego; // Asignar la función de reinicio al botón

    }
    // Update is called once per frame
    void Update()
    {
        // Actualizar el tiempo de juego
        timeGame += Time.deltaTime;
        // Calcular el score
        score = Mathf.FloorToInt( timeGame * multiplicaScore);
        
        Debug.Log("Score: " + score + " segundos");        // Comprobar si se ha presionado el botón izquierdo del ratón
        if (Mouse.current.leftButton.isPressed)
        {
            // Obtener la posición del ratón en la pantalla
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value); // Convertir la posición del ratón a coordenadas del mundo
            scoreText.text = "Score: " + score.ToString(); // Actualizar el texto del score en el UI Document
            // Calcular la dirección desde el jugador hasta el ratón
            Vector2 direccion = (mousePosition - transform.position).normalized;
            transform.up = direccion;            // Normalizar la dirección para obtener un vector unitario
            rb.AddForce(direccion * fuerzaEmpuje);            // Aplicar una fuerza en esa dirección

            if (rb.linearVelocity.magnitude > maxVelocidad)            // Limitar la velocidad máxima del jugador
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxVelocidad;                // Normalizar la velocidad y multiplicar por la velocidad máxima
            }
            boosterFlame.SetActive(true);            // Activar la llama del booster
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)        {
            boosterFlame.SetActive(false);            // Desactivar la llama del booster al soltar el botón
        }
    }
    // Detectar colisiones con otros objetos
    private void OnCollisionEnter2D(Collision2D collision)    {
        Destroy(gameObject);        // Si colisiona con un obstáculo, destruir el jugador
        Instantiate(Explota, transform.position, transform.rotation); // Instanciar el efecto de explosión en la posición del jugador
        reiniciaboton.style.display = DisplayStyle.Flex; // Mostrar el botón de reinicio
    }
    void ReiniciaJuego()    {
        // Reiniciar la escena actual
      SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
