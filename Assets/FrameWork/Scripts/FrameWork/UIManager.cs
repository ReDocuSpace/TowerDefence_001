using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public enum EGame_Dialog
{
}

namespace CompanyName.FrameWork
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [Header("Fade In/Out")]
        [SerializeField] private Image fadeImg;

        [SerializeField] private Transform dialogTrans;
        Dictionary<EGame_Dialog, GameObject> dialogDic;

        public override void Init()
        {
            dialogDic = new Dictionary<EGame_Dialog, GameObject>();

            for (int i = 0; i < dialogTrans.childCount; i++)
            {
                dialogDic.Add((EGame_Dialog)i, dialogTrans.GetChild(i).gameObject);

                IDialogManager dialogManager = dialogDic[(EGame_Dialog)i].GetComponent<IDialogManager>();

                if (dialogManager != null)
                {
                    dialogManager.OnEnter();
                }

                dialogDic[(EGame_Dialog)i].SetActive(false);
            }
        }

        public override void Release()
        {
            foreach (var dialog in dialogDic)
            {
                IDialogManager dialogManager = dialog.Value.GetComponent<IDialogManager>();

                if (dialogManager != null)
                {
                    dialogManager.OnExit();
                }
            }
            dialogDic.Clear();
        }

        public void PlayDialog(EGame_Dialog dialog)
        {

            IDialogManager dialogManager = dialogDic[dialog].GetComponent<IDialogManager>();

            if (dialogManager != null)
            {
                dialogManager.OnPlay();
            }


            if (!dialogDic[dialog].activeSelf)
                dialogDic[dialog].SetActive(true);
        }

        public void CloseDialog(EGame_Dialog dialog)
        {
            IDialogManager dialogManager = dialogDic[dialog].GetComponent<IDialogManager>();

            if (dialogManager != null)
            {
                dialogManager.OnEnd();
            }

            if (dialogDic[dialog].activeSelf)
                dialogDic[dialog].SetActive(false);
        }

        public void CloseDialog()
        {
            foreach (var dialog in dialogDic)
            {
                IDialogManager dialogManager = dialog.Value.GetComponent<IDialogManager>();

                if (dialogManager != null)
                {
                    dialogManager.OnEnd();
                }

                if (dialog.Value.activeSelf)
                    dialog.Value.SetActive(false);
            }
        }


        // Command Function
        public void FadeIn(float time = 0.5f)
        {
            Fade(true, time);
        }

        public void FadeOut(float time = 0.5f)
        {
            Fade(false, time);
        }

        private void Fade(bool fade, float time)
        {
            if (fadeImg != null)
            {
                Color color = fadeImg.color;
                color.a = fade ? 0.0f : 1.0f;
                fadeImg.DOFade(fade ? 1.0f : 0.0f, time);
            }
        }
    }
}

