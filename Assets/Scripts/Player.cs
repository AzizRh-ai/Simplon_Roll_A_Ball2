using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Event
    public delegate void OnScoreMessage(int value);
    public static event OnScoreMessage OnScoreUpdate;

    [SerializeField] private TextMeshProUGUI scoreText;

    private Rigidbody _rigidbody;
    private int ScoreValue = 0;
    [SerializeField] private float speed = 15f;

    // Scriptable Object
    [SerializeField] private Scenario _scenario;
    [SerializeField] private GameObject _wallPrefab;

    void Start()
    {
        _scenario.Score = 0;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            ScoreValue = PlayerPrefs.GetInt("Score");
            scoreText.text = ScoreValue.ToString();
        }
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput * speed * Time.deltaTime, 0f, verticalInput * speed * Time.deltaTime);
        _rigidbody.AddForce(direction * 0.5f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            Destroy(other.gameObject);

            UpdateScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);

            UpdateScore();

        }
    }
    private void UpdateScore()
    {
        ScoreValue++;

        PlayerPrefs.SetInt("Score", ScoreValue);
        //j'invoque OnScoreUpdate
        OnScoreUpdate?.Invoke(ScoreValue);

        _scenario.Score = ScoreValue;

        Instantiate(_wallPrefab, _scenario.Wall[_scenario.Score - 1], Quaternion.Euler(0f, 90f, 0f));
        if (ScoreValue == 8)
        {
            _scenario.Score = ScoreValue;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
