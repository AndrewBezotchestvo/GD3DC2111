using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] Transform _player;

    private bool _isPause = true;

    [SerializeField] GameObject _playButton;
    [SerializeField] GameObject _exitButton;
    
    void Start()
    {
        _isPause = true;
    }

    void Update()
    {
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

        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            _isPause = ! _isPause;
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

