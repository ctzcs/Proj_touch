using DG.Tweening;
using Shapes;
using UnityEngine;


/// <summary>
/// 标识当前场景的枚举
/// </summary>
public enum ECurrentState
{
    Start,
    EntryLevel,
    FinishLevel,
    BackToStart,
}

/// <summary>
/// 动画导演
/// </summary>
public class AnimDirector:MonoBehaviour
{
    public Polygon tPolygon;
    public Polygon hPolygon;
    public TShape tShape;
    public HShape hShape;

    public Transform oTrans;
    public Transform uTrans;
    public Transform cTrans;
    public Disc oDisc;
    
    public CryArc cryArc;
    public Disc smile;
    
    #region 动画序列容器
    //开场动画
    public Tweener[] _tweeners_T = new Tweener[4];
    public Tweener[] _tweeners_H = new Tweener[1];
    public Tweener[] _tweeners_O = new Tweener[2];
    public Tweener[] _tweeners_U = new Tweener[1];
    public Tweener[] _tweeners_C = new Tweener[1];
    
    //关卡动画
    public Tweener[] _tweeners_T_Level = new Tweener[1];
    public Tweener[] _tweeners_H_Level = new Tweener[1];
    public Tweener[] _tweeners_O_Level = new Tweener[3];
    public Tweener[] _tweeners_U_Level = new Tweener[1];
    public Tweener[] _tweeners_C_Level = new Tweener[1];
    public Tweener[] _tweeners_CryArc_Level = new Tweener[1];
    
    //完成关卡动画
    public Tweener[] _tweeners_CryArc_Finish = new Tweener[2];
    public Tweener[] _tweeners_Smile_Finish = new Tweener[1];
    
    //回到开始的动画
    public Tweener[] _tweeners_CryArc_BackToStart = new Tweener[1];
    #endregion

    #region 目标参数
    //开始动画的目标参数
    public float TargetHeight_T = 10;
    public float TargetWidthRight_T = 11;
    public float TargetWidthLeft_T = 1;
    public float TargetRightOffset_T = 1f;
    
    public float TargetHeight_H = 5f;
    public float TargetWidthRight_H = 2;

    public float TargetUpOffset_O = 2;
    public float TargetRightOffset_O = 0.5f;

    public float TargetDownOffset_U = -0.5f;

    public float TargetUpOffset_C = 2;
    
    //关卡动画的目标参数
    private float levelAnimDuringTime = 0.5f;
    public float TargetX_T = -10;
    public float TargetThickness_O_Level = 0.25f;
    public float TargetRadius_O_Level = 6;

    public float TargetX_H = 10;
    public float TargetY_U = -10;
    public float TargetY_C = 10;
    public float TargetY_CryArc = -10;
    
    //完成关卡参数
    public float TargetSmileDuration = 1.5f;
    
    //BackToStart参数
    public float TargetBackToStartDuration = 4;
    #endregion

    #region 场景标识
    public ECurrentState state;
    #endregion
    
    


    private void Start()
    {
        //开始的时候设置为开始状态
        state = ECurrentState.Start;
        #region 开场动画
        
        //T
        _tweeners_T[0] = DOTween.To(() => tShape.height, value => {tShape.height = value;},TargetHeight_T,1);
        _tweeners_T[1] = DOTween.To(() => tShape.widthRight, value => {tShape.widthRight = value;},TargetWidthRight_T,1);
        _tweeners_T[2] = tShape?.transform.DOMoveX(tShape.transform.position.x + TargetRightOffset_T, 1);
        _tweeners_T[3] = DOTween.To(() => tShape.widthLeft, value => { tShape.widthLeft = value;},TargetWidthLeft_T,1);
        TweenSetPauseAndSave(_tweeners_T);
        //H
        _tweeners_H[0] = DOTween.To(() => hShape.heightLeft, value => {hShape.heightLeft = value;},TargetHeight_H,1);
        TweenSetPauseAndSave(_tweeners_H);
        
        //O
        _tweeners_O[0] = oTrans.DOMoveY(oTrans.position.y + TargetUpOffset_O, 1);
        _tweeners_O[1] = oTrans.DOMoveX(oTrans.position.x + TargetRightOffset_O, 1);
        TweenSetPauseAndSave(_tweeners_O);
        
        //U
        _tweeners_U[0] = uTrans.DOMoveY(uTrans.position.y + TargetDownOffset_U, 1);
        TweenSetPauseAndSave(_tweeners_U);
        
        //C
        _tweeners_C[0] = cTrans.DOLocalMoveY(cTrans.localPosition.y + TargetUpOffset_C, 1);
        TweenSetPauseAndSave(_tweeners_C);
        
        #endregion
        #region Level动画
        //T
        _tweeners_T_Level[0] = tShape.transform.DOMoveX(tShape.transform.position.x + TargetX_T , levelAnimDuringTime);
        TweenSetPauseAndSave(_tweeners_T_Level);
        //H
        _tweeners_H_Level[0] = hShape.transform.DOMoveX(hShape.transform.position.x + TargetX_H , levelAnimDuringTime);
        TweenSetPauseAndSave(_tweeners_H_Level);
        //U
        _tweeners_U_Level[0] = uTrans.DOMoveY(uTrans.position.y + TargetY_U, levelAnimDuringTime);
        TweenSetPauseAndSave(_tweeners_U_Level);
        //C
        _tweeners_C_Level[0] = cTrans.DOMoveY(cTrans.position.y + TargetY_C, levelAnimDuringTime);
        TweenSetPauseAndSave(_tweeners_C_Level);
        //O
        _tweeners_O_Level[0] = DOTween.To(() => oDisc.Thickness, value => oDisc.Thickness = value,TargetThickness_O_Level, levelAnimDuringTime);
        _tweeners_O_Level[1] = oTrans.DOMove(Vector2.zero, levelAnimDuringTime);
        _tweeners_O_Level[2] = DOTween.To(() => oDisc.Radius, value => oDisc.Radius = value,TargetRadius_O_Level, levelAnimDuringTime);
        TweenSetPauseAndSave(_tweeners_O_Level);
        
        //CryArc
        _tweeners_CryArc_Level[0] = cryArc.transform.DOMoveY(cryArc.transform.position.y + TargetY_CryArc, levelAnimDuringTime);
        TweenSetPauseAndSave(_tweeners_CryArc_Level);
        

        #endregion

        #region 完成Level动画
        //眨眼
        
        //笑脸
        
        _tweeners_CryArc_Finish[0] = DOTween.ToAlpha(() => cryArc.fill.Color, value=>
        {
            cryArc.fill.Color = value;
            cryArc.handleDisc.Color = value;
        }, 0, TargetSmileDuration);
        _tweeners_CryArc_Finish[1] = DOTween.ToAlpha(() => cryArc.bg.Color, value=>
        {
            cryArc.bg.Color = value;
        }, 0, TargetSmileDuration);
        TweenSetPauseAndSave(_tweeners_CryArc_Finish);
        
        _tweeners_Smile_Finish[0] = DOTween.ToAlpha(() => smile.Color, value=>
        {
            smile.Color = value;
        }, 1, TargetSmileDuration);
        TweenSetPauseAndSave(_tweeners_Smile_Finish);
        #endregion
        
        #region 回到开场动画
        _tweeners_CryArc_BackToStart[0] = DOTween.To(() => 1f, value =>
        {
            cryArc.SetRange(value);
            cryArc.range = value;
            for (int i = 0; i < _tweeners_T.Length; i++)
            {
                _tweeners_T[i].fullPosition = cryArc.range;
            }
            for (int i = 0; i < _tweeners_H.Length; i++)
            {
                _tweeners_H[i].fullPosition = cryArc.range;
            }
            for (int i = 0; i < _tweeners_O.Length; i++)
            {
                _tweeners_O[i].fullPosition = cryArc.range;
            }
            for (int i = 0; i < _tweeners_U.Length; i++)
            {
                _tweeners_U[i].fullPosition = cryArc.range;
            }
            for (int i = 0; i < _tweeners_C.Length; i++)
            {
                _tweeners_C[i].fullPosition = cryArc.range;
            }
            tShape.UpdateShapePoints();
            hShape.UpdateShapePoints();
        },0,TargetBackToStartDuration);
        
        TweenSetPauseAndSave(_tweeners_CryArc_BackToStart);

        #endregion
    }

    private void Update()
    {
        /*switch (state)
        {
            case ECurrentState.Start:
                EntryStartAnim();
                break;
            case ECurrentState.EntryLevel:
                if (cryArc.range >= 1)
                {
                    EntryLevelAnim();
                }
                break;
            case ECurrentState.FinishLevel:
                FinishLevelAnim();
                break;
            case ECurrentState.BackToStart:
                BackToStartAnim();
                break;
        }*/
        //其实这里应该有一个状态机，现在用if代替了
        if (state == ECurrentState.Start)
        {
            EntryStartAnim();
            if (cryArc.range >= 1)
            {
                //进入关卡场景动画逻辑
                EntryLevelAnim();
            }
        }
        
        
        if (state == ECurrentState.FinishLevel)
        {
            FinishLevelAnim();
        }

        if (state == ECurrentState.BackToStart)
        {
            BackToStartAnim();
        }
        
    }

    /// <summary>
    /// 开始的动画判断逻辑
    /// </summary>
    void EntryStartAnim()
    {
        if (state != ECurrentState.Start)
        {
            return;
        }
        
        if (Mathf.Approximately(cryArc.rangeDelta,0))
        {
            for (int i = 0; i < _tweeners_T.Length; i++)
            {
                _tweeners_T[i].Pause();
            }
        }
        else //if (cryArc.rangeDelta > 0)
        {
            for (int i = 0; i < _tweeners_T.Length; i++)
            {
                _tweeners_T[i].fullPosition = cryArc.range;
            }
            for (int i = 0; i < _tweeners_H.Length; i++)
            {
                _tweeners_H[i].fullPosition = cryArc.range;
            }
            for (int i = 0; i < _tweeners_O.Length; i++)
            {
                _tweeners_O[i].fullPosition = cryArc.range;
            }
            for (int i = 0; i < _tweeners_U.Length; i++)
            {
                _tweeners_U[i].fullPosition = cryArc.range;
            }
            for (int i = 0; i < _tweeners_C.Length; i++)
            {
                _tweeners_C[i].fullPosition = cryArc.range;
            }
        }/*else// if(cryArc.rangeDelta < 0)
        {
            for (int i = 0; i < _tweeners_T.Length; i++)
            {
                _tweeners_T[i].fullPosition = cryArc.range;
            }
            /*t.fullPosition = cryArc.range;
            t.PlayBackwards();#1#
        }*/

        tShape.UpdateShapePoints();
        hShape.UpdateShapePoints();
    }

    /// <summary>
    /// 进入关卡的动画播放逻辑
    /// </summary>
    void EntryLevelAnim()
    {
        state = ECurrentState.EntryLevel;
        TweenPlayForward(_tweeners_O_Level);
        TweenPlayForward(_tweeners_T_Level);
        TweenPlayForward(_tweeners_H_Level);
        TweenPlayForward(_tweeners_U_Level);
        TweenPlayForward(_tweeners_C_Level);
        TweenPlayForward(_tweeners_CryArc_Level);
    }

    void FinishLevelAnim()
    {
        TweenPlayBack(_tweeners_O_Level);
        TweenPlayBack(_tweeners_T_Level);
        TweenPlayBack(_tweeners_H_Level);
        TweenPlayBack(_tweeners_U_Level);
        TweenPlayBack(_tweeners_C_Level);
        TweenPlayBack(_tweeners_CryArc_Level);
        TweenPlayForward(_tweeners_CryArc_Finish);
        TweenPlayForward(_tweeners_Smile_Finish);
    }

    void BackToStartAnim()
    {
        //
        TweenPlayBack(_tweeners_CryArc_Finish);
        //笑脸消失
        TweenPlayBack(_tweeners_Smile_Finish);
        //这里只有这一个动画，先这样了
        //cryArc的range归零
        if (_tweeners_CryArc_BackToStart[0].IsComplete())
        {
            //其他的动画都倒回了，这个没有，所以要重新开始
            _tweeners_CryArc_BackToStart[0].Restart();
        }
        else
        {
            TweenPlayForward(_tweeners_CryArc_BackToStart,BackToStartState);
        }
        
        
        /*state = ECurrentState.Start;*/
        
    }

    #region Helper
    void TweenSetPauseAndSave(Tweener[] tween)
    {
        for (int i = 0; i < tween.Length; i++)
        {
            tween[i].Pause();
            tween[i].SetAutoKill(false);
            tween[i].SetLoops(0);
        }
    }

    void TweenPlayForward(Tweener[] tween,TweenCallback callback = null)
    {
        for (int i = 0; i < tween.Length; i++)
        {
            tween[i].PlayForward();
            
            //每个都回调似乎不太对的
            tween[i].onComplete += callback;
        }
    }

    void TweenPlayBack(Tweener[] tween)
    {
        for (int i = 0; i < tween.Length; i++)
        {
            tween[i].PlayBackwards();
        }
    }
    
    

    /// <summary>
    /// 给关卡结束之后调用的接口
    /// </summary>
    /// <param name="state"></param>
    public void SetAnimState(ECurrentState state)
    {
        this.state = state;
    }

    void BackToStartState()
    {
        SetAnimState(ECurrentState.Start);
        cryArc.IsLevel = false;
    }
    #endregion
    
}
