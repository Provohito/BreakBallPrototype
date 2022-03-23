using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private BannerView bannerAd;
    static bool bannerAdRequested = false;

    private InterstitialAd interstitial;
    private RewardedAd rewardBasedVideo;
    bool isRewarded = false;
    private UIManagerGame _ui;

    public static AdManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {

        _ui = GameObject.Find("UIManagerGame").GetComponent<UIManagerGame>();
        MobileAds.Initialize(InitializationStatus => { });
        // Get singleton reward based video ad reference.
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        this.rewardBasedVideo = new RewardedAd(adUnitId);

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardedAdLoaded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;

        this.RequestRewardBasedVideo();

        if (bannerAdRequested)
            return;

        this.RequestBanner();
        bannerAdRequested = true;
    }
    private void Update()
    {
        if (isRewarded)
        {
            isRewarded = false;
            _ui.GGame();
        }
    }
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        this.bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Clean up banner ad before creating a new one.
        if (this.bannerAd != null)
        {
            this.bannerAd.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        this.bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Load a banner ad.
        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestRewardBasedVideo()
    {
        this.rewardBasedVideo.LoadAd(this.CreateAdRequest());
    }
    public void ShowRewardBasedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }
    }

    #region RewardBasedVideo callback handlers


    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        isRewarded = true;
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
    }

    #endregion
}
