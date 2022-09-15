using System.Collections;
using UnityEngine;


namespace CompanyName.FrameWork
{
    public abstract class IPlayManager : MonoBehaviour
    {
        private Coroutine _corGameFlow;

        private void Awake()
        {
            GameManager.Instance.currentPlay = this;
            UIManager.Instance.FadeOut();

            if (_corGameFlow == null) _corGameFlow = StartCoroutine(_cGameFlow());

            OnEnter();
        }

        private void OnDestroy()
        {
            if (_corGameFlow != null) StopCoroutine(_corGameFlow);
            _corGameFlow = null;

            OnExit();
        }

        // Game 시작
        protected abstract void OnEnter();

        // Game 중 [Update 기능]
        protected abstract IEnumerator _cGameFlow();

        // Game 종료
        protected abstract void OnExit();
    }

}
