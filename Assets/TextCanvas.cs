using TMPro;
using UnityEngine;

public class TextCanvas : MonoBehaviour
{
    public static TextCanvas instance;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int _score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        scoreText.text = _score.ToString();
    }

    public void UpdateScore()
    {
        _score++;
        scoreText.text = _score.ToString();
    }
}
