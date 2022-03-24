using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections;

public class MobAdBanner : MonoBehaviour
{
    private BannerView _bannerView;

#if UNITY_ANDROID
    private const string _bannerUnitID = "ca-app-pub-5281254441931005/1657839631"; 
#elif UNITY_IPHONE
    private const string _bannerUnitID = "";
#else
    private const string _bannerUnitID = "unexpected_platform";
#endif

    private void OnEnable()
    {
        _bannerView = new BannerView(_bannerUnitID, AdSize.Banner, AdPosition.Bottom);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerView.LoadAd(adRequest);
        StartCoroutine(ShowBanner());
    }

    private IEnumerator ShowBanner()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        _bannerView.Show();
    }
}
