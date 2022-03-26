using UnityEngine;
using UnityEngine.SceneManagement;

public class Birds : MonoBehaviour
{
    private Vector3 _initialPosition;
    private float _timeSittingAround;
    private bool _birdWasLaunched;

    [SerializeField] private float _launchPower = 250;

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);


        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > 20 ||
            transform.position.x > 20 ||
            transform.position.y < -20 ||
            transform.position.x < -20 ||
            _timeSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

    }

   private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPostion = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPostion * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

}
