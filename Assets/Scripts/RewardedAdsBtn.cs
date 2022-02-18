using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsBtn : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button _showAdButton;

    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";

    private string _adUnitId;

    private bool isAdsRunning = false;

    private void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;

        //Disable button until ad is ready to show
        _showAdButton.interactable = false;
    }
    
    private UIManagerGame _ui;
    private void Start()
    {
        StartCoroutine(LoadRewardedAds());
       
        
    }

    private IEnumerator LoadRewardedAds()
    {
        yield return new WaitForSeconds(1f);
        LoadAd();
        StopCoroutine(LoadRewardedAds());
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }

    // Implement a method to execute when the user clicks the button.
    public void ShowAd()
    {
        if (isAdsRunning == false)
        {
            // Disable the button: 
            _showAdButton.interactable = false;
            // Then show the ad:
            _ui = GameObject.Find("UIManagerGame").GetComponent<UIManagerGame>();
            Debug.Log("SwodAd");
            isAdsRunning = true;
            StartCoroutine(RewardRoutine());

        }
        
    }
    private int _coundAds = 0;

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        isAdsRunning = false;  
    }

    IEnumerator RewardRoutine()
    {
        while (Advertisement.isShowing)
        {
            yield return null;
        }

        // In 4.0 Ads need to be loaded first, after initialization
        // just another flag to make sure everything is initialized :)

        // Show Ads
        Advertisement.Show(_adUnitId, this);

        yield return new WaitForSeconds(0.25f);

        while (isAdsRunning)
        {
            yield return null;
        }
        _ui.GGame();
        // Grant a reward.
        //GameLogic.S.IncrementPoint2AfterAds(1);
        // Load another ad:
        if (_coundAds < 2)
        {
            Advertisement.Load(_adUnitId, this);
        }
        _coundAds++;
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { isAdsRunning = true; }
    public void OnUnityAdsShowClick(string adUnitId) { }

    private void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}
