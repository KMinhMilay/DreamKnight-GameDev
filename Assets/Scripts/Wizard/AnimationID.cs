using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationID
{
    public static int RunAnim = Animator.StringToHash("run");
    public static int IdleAnim = Animator.StringToHash("idle");
    public static int NormalAtkAnim = Animator.StringToHash("attack1");
    public static int SkillAnim = Animator.StringToHash("attack2");
    public static int DieAnim = Animator.StringToHash("die");
}
