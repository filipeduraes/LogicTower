using System;
using LogicTower.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LogicTower.LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private ChallengeSettings[] challenges;
        [SerializeField] private LoadTransitionHandler transitionHandler;

        public static event Action<ChallengeSettings> OnLevelLoaded = delegate { };
        private ChallengeSettings CurrentChallengeSettings => challenges[_currentChallenge];

        private static LevelLoader levelLoader;
        private static bool isLoadingLevel;
        
        private int _currentChallenge = -1;
        private AsyncOperationHandle _loadOperation;
        private AsyncOperationHandle<GameObject> _instantiateOperation;

        private void Start()
        {
            levelLoader = this;
            LoadNextChallenge();
            transitionHandler.SetBlackScreen();

            transitionHandler.OnTransitionFinished += ReleaseAndLoadNextLevel;
        }

        private void OnDestroy()
        {
            transitionHandler.OnTransitionFinished -= ReleaseAndLoadNextLevel;
        }

        public static void LoadNextChallenge()
        {
            if (!isLoadingLevel)
            {
                isLoadingLevel = true;
                
                if (levelLoader.HasLoadedLevel())
                    levelLoader.transitionHandler.FadeOut();
                else
                    levelLoader.LoadNextChallengeInternal();
            }
        }

        public static void ReloadCurrentChallenge()
        {
            levelLoader.ReloadCurrentChallengeInternal();
        }
        
        private void ReleaseAndLoadNextLevel()
        {
            ReleaseCurrentChallenge();
            LoadNextChallengeInternal();
        }

        private void ReloadCurrentChallengeInternal()
        {
            if (_loadOperation.IsValid() && _instantiateOperation.IsValid())
            {
                Addressables.ReleaseInstance(_instantiateOperation.Result);
                _instantiateOperation = Addressables.InstantiateAsync(CurrentChallengeSettings.LevelPrefab);
                _instantiateOperation.Completed += RevealLevel;
            }
        }

        private void LoadNextChallengeInternal()
        {
            _currentChallenge++;
            
            _loadOperation = Addressables.LoadAssetAsync<GameObject>(CurrentChallengeSettings.LevelPrefab);
            _loadOperation.Completed += SpawnLevel;
        }

        private void SpawnLevel(AsyncOperationHandle handle)
        {
            _loadOperation.Completed -= SpawnLevel;
            _instantiateOperation = Addressables.InstantiateAsync(CurrentChallengeSettings.LevelPrefab);
            _instantiateOperation.Completed += RevealLevel;
        }

        private void RevealLevel(AsyncOperationHandle<GameObject> operation)
        {
            _instantiateOperation.Completed -= RevealLevel;
            IChallengeHandler[] challengeHandlers = _instantiateOperation.Result.GetComponents<IChallengeHandler>();

            if (challengeHandlers != null && challengeHandlers.Length > 0)
            {
                foreach (IChallengeHandler handler in challengeHandlers)
                    handler.PopulateSettings(CurrentChallengeSettings);
            }
            
            isLoadingLevel = false;
            OnLevelLoaded(CurrentChallengeSettings);
            transitionHandler.FadeIn();
        }

        private void ReleaseCurrentChallenge()
        {
            Addressables.ReleaseInstance(_instantiateOperation.Result);
            Addressables.Release(_loadOperation);
        }

        private bool HasLoadedLevel()
        {
            return _currentChallenge >= 0 && _loadOperation.IsValid() && _loadOperation.IsDone && _instantiateOperation.IsValid() && _instantiateOperation.IsDone;
        }
    }
}