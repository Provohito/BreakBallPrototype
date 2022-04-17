using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Example
{
    public class RewardedAd : MonoBehaviour
    {
        IRewardedAd ad;
        string adUnitId = "Rewarded_Android";
        string gameId = "4601141";
        private UIManagerGame _ui;
        
        private void Start()
        {
            InitServices();
        }
        public async void InitServices()
        {
            try
            {
                InitializationOptions initializationOptions = new InitializationOptions();
                Debug.Log("1");
                initializationOptions.SetGameId(gameId);
                Debug.Log("2");
                await UnityServices.InitializeAsync(initializationOptions);
                Debug.Log("3");
                InitializationComplete();
            }
            catch (Exception e)
            {
                InitializationFailed(e);
            }
        }

        public void SetupAd()
        {
            //Create
            ad = MediationService.Instance.CreateRewardedAd(adUnitId);

            //Subscribe to events
            ad.OnLoaded += AdLoaded;
            ad.OnFailedLoad += AdFailedLoad;

            ad.OnShowed += AdShown;
            ad.OnFailedShow += AdFailedShow;
            ad.OnClosed += AdClosed;
            ad.OnClicked += AdClicked;
            ad.OnUserRewarded += UserRewarded;

            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
        }

        public void ShowAd(GameObject obj)
        {
            obj.GetComponent<Button>().interactable = false;
            if (ad.AdState == AdState.Loaded)
            {
                ad.Show();
            }
        }

        void InitializationComplete()
        {
            
            SetupAd();
            ad.Load();
        }

        void InitializationFailed(Exception e)
        {
            Debug.Log("Initialization Failed: " + e.Message);
        }

        void AdLoaded(object sender, EventArgs args)
        {
            Debug.Log("Ad loaded");
        }

        void AdFailedLoad(object sender, LoadErrorEventArgs args)
        {
            Debug.Log("Failed to load ad");
            Debug.Log(args.Message);
        }

        void AdShown(object sender, EventArgs args)
        {
            Debug.Log("Ad shown!");
        }

        void AdClosed(object sender, EventArgs e)
        {
            // Pre-load the next ad
            _ui = GameObject.Find("UIManagerGame").GetComponent<UIManagerGame>();
            _ui.GGame();
            ad.Load();
            Debug.Log("Ad has closed");
            // Execute logic after an ad has been closed.
        }

        void AdClicked(object sender, EventArgs e)
        {
            Debug.Log("Ad has been clicked");
            // Execute logic after an ad has been clicked.
        }

        void AdFailedShow(object sender, ShowErrorEventArgs args)
        {
            Debug.Log(args.Message);
        }

        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
        }

        void UserRewarded(object sender, RewardEventArgs e)
        {
            Debug.Log($"Received reward: type:{e.Type}; amount:{e.Amount}");
        }

    }
}