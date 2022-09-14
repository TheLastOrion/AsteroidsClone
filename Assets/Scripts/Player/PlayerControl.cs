using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private bool _isInvulnerable = false;
    private int _remainingLives;
    [SerializeField] private Transform _visualsTransform;
    [SerializeField] private float _invulnerabilityTime = 3f;
    private Coroutine _invulnerabilityCoroutine;
    private void Awake()
    {
        
        GameEvents.GameStarted += GameEventsOnGameStarted;
        GameEvents.GameOver += GameEventsOnGameOver;
    }

    private void GameEventsOnGameOver()
    {
        gameObject.SetActive(false);
    }

    private void GameEventsOnGameStarted()
    {
        _remainingLives = Constants.PLAYER_LIVES;
        Debug.LogError("Remaining Lives: " + _remainingLives);
        _visualsTransform.gameObject.SetActive(true);
        gameObject.SetActive(true);
        SetInvulnerability(false);
    }


    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player Hit!");
            GameEvents.FirePlayerHitByAsteroid(otherCollider);

            if (!_isInvulnerable && _remainingLives > 0)
            {
                _remainingLives--;
                SetInvulnerability(true);
                _invulnerabilityCoroutine = StartCoroutine(InvulnerabilityCoroutine());
            }

            else if (_remainingLives == 0)
            {
                Debug.LogError("HERE");
                GameEvents.FireGameOver();
            }
        }

        
    }
    public void SetInvulnerability(bool isInvulnerable)
    {
        _isInvulnerable = isInvulnerable;
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        _visualsTransform.gameObject.SetActive(false);
        //Wait for some destroy animation maybe?
        yield return new WaitForSeconds(1f);
        int flickerAmount = (int)(_invulnerabilityTime / Constants.INVULNERABILITY_FLICKER_FREQUENCY);
        for (float i = flickerAmount; i > 0; i -= Constants.INVULNERABILITY_FLICKER_FREQUENCY)
        {
            _visualsTransform.gameObject.SetActive(!_visualsTransform.gameObject.activeSelf);
            yield return new WaitForSeconds(Constants.INVULNERABILITY_FLICKER_FREQUENCY);
        }
        _visualsTransform.gameObject.SetActive(true);
        SetInvulnerability(false);


    }
}
