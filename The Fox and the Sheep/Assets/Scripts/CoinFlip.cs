using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CoinFlip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnCoin();
    }

    // Scale properties
    [SerializeField] private Vector3 enterTargetScale = new Vector3(0.5f, 0.5f, 0.5f); // target size for entering
    [SerializeField] private Vector3 exitTargetScale = new Vector3(0, 0, 0); // target size for exiting size
    [SerializeField] private Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f); // Final size
    private bool _isActive;
    public float scaleSpeed = 3f; // Speed of scaling

    // Animation properties
    [SerializeField] private Animator _animator;

    // Randomize properties 
    [SerializeField] private GameObject[] coins;

    private void Update()
    {
        ScaleCoin(_isActive);
        // The coin stays active for 5 seconds 
    }

    /**
     * Set the animation to out if the coin is triggered
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // And animation is complete 
        {
            _isActive = false;
            _animator.SetBool("CanRotate", true);
            _animator.SetBool("FoxIdle", false);
            _animator.SetBool("SheepIdle", false);
            SpawnCoin();
        }
    }

    /**
     * Animate the coin in and out by scaling it
     */
    private void ScaleCoin(bool entering)
    {
        // Set target size 
        targetScale = entering ? enterTargetScale : exitTargetScale;

        // Smoothly scale towards the target size
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

        // Stop scaling when close enough to target size
        if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
        {
            transform.localScale = targetScale; // Snap to target size
        }
    }

    private void SpawnCoin()
    {
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        var randomTime = Random.Range(3, 10);
        yield return new WaitForSeconds(randomTime); // Wait for random time
        _isActive = true;
    }

    private string FoxOrSheep()
    {
        var x = Random.Range(0, 2);

        return x == 0 ? "SheepIdle" : "FoxIdle";
    }

    private void CountRotations()
    {
        string foxOrSheep = FoxOrSheep();
        if (_isActive)
        {
            _animator.SetBool(foxOrSheep, true);
            _animator.SetBool("CanRotate", false);
        }
    }

    private void FrameReached(string type)
    {
        _animator.SetBool(type, true);
    }
}