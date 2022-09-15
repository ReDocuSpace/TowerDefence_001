using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompanyName.FrameWork
{
    public class LogoManager : MonoBehaviour
    {
        private Coroutine _corlogoFlow = null;

        [SerializeField] private GameObject[] logoMain;
        [Header("처음 전환 로딩 Scene")]
        [SerializeField] private EPlay_Scene playScene;

        private void Awake()
        {
            for (int i = 0; i < logoMain.Length; i++)
            {
                logoMain[i].gameObject.SetActive(false);
            }

            if (_corlogoFlow == null)
            {
                _corlogoFlow = StartCoroutine(cLogoFlow());
            }
        }
        private void OnDestroy()
        {
            if (_corlogoFlow != null)
            {
                StopCoroutine(_corlogoFlow);
            }

            _corlogoFlow = null;
        }

        IEnumerator cLogoFlow()
        {
            // 첫번째 로고
            yield return YieldInstructionCache.WaitForSecond(1.0f);

            for (int i = 0; i < logoMain.Length; i++)
            {
                logoMain[i].gameObject.SetActive(true);
                UIManager.Instance.FadeOut();

                yield return YieldInstructionCache.WaitForSecond(2.0f);

                UIManager.Instance.FadeIn();

                yield return YieldInstructionCache.WaitForSecond(1.0f);

                logoMain[i].gameObject.SetActive(false);
            }

            UIManager.Instance.FadeOut();

            if (playScene != EPlay_Scene.None)
                GameManager.Instance.ChangeScene(playScene, ELoad_Scene.Loading_Title);

            
        }
    }

}
