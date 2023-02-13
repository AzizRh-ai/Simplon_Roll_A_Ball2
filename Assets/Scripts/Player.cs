using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Event
    public delegate void OnScoreMessage(int value);
    public static event OnScoreMessage OnScoreUpdate;


    [Header("Ball")]
    [SerializeField] private float speed = 15f;
    private Rigidbody _rigidbody;

    [Header("Score Value")]
    private int ScoreValue = 0;

    // Scriptable Object
    [Header("Scenario (Scriptable Object)")]
    [SerializeField] private Scenario _scenario;

    //Prefab
    [Header("Prefab linked to Scenario")]
    [SerializeField] private GameObject _wallPrefab;

    // Score
    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    // Score
    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOver;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            ScoreValue = _scenario.Score;
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


        //j'invoque OnScoreUpdate
        OnScoreUpdate?.Invoke(ScoreValue);

        // je save jusqu'a la fin de session
        _scenario.Score = ScoreValue;

        //je save aussi dans le registre la clé Score
        //HKEY_CURRENT_USER\Software\Unity\UnityEditor\DefaultCompany\Roll_A_Ball
        PlayerPrefs.SetInt("Score", ScoreValue);

        //J'instancie le mur a une position suivant l'index du tableau dans scenario..
        Instantiate(_wallPrefab, _scenario.Wall[_scenario.Score - 1], Quaternion.identity);

        // si score =8 niveau suivant
        if (ScoreValue == 8)
        {
            // Je save score dans scenario pour le niveau suivant
            _scenario.Score = ScoreValue;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (ScoreValue == 16)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnDestroy()
    {
        //On supprime la clé de registre une fois terminer.
        PlayerPrefs.DeleteKey("Score");
    }
}
