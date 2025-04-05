using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] Transform _player;
    private bool _isPause = true;

    [SerializeField] GameObject _playButton;
    [SerializeField] GameObject _exitButton;
    [SerializeField] Text _score;
    [SerializeField] Text _record;

    private float _recordValue;

    void Start()
    {
        _isPause = true;
        // Загружаем рекорд при старте
        _recordValue = PlayerPrefs.GetFloat("Record", 0);
        _record.text = Mathf.Round(_recordValue).ToString();
    }

    void Update()
    {
        _score.text = Mathf.Round(_player.position.z).ToString();

        if (_recordValue < _player.position.z)
        {
            _recordValue = _player.position.z;
            _record.text = Mathf.Round(_recordValue).ToString();
            // Сохраняем новый рекорд
            PlayerPrefs.SetFloat("Record", _recordValue);
            PlayerPrefs.Save(); // Сохраняем изменения
        }

        if (_isPause)
        {
            _playButton.SetActive(true);
            _exitButton.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _playButton.SetActive(false);
            _exitButton.SetActive(false);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPause = !_isPause;
        }
    }

    public void Play()
    {
        _isPause = !_isPause;
    }

    public void Exit()
    {
        Application.Quit();
    }
}