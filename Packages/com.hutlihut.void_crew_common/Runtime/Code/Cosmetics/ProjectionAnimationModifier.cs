using System;
using Unity.Mathematics;
using UnityEngine;

namespace VC.Common.Cosmetics
{
    [Flags]
    public enum ProjectionAxis
    {
        X = 1,
        Y = 2,
        Both = X | Y
    }

    public enum EasingFunction
    {
        Linear,
        QuadraticEaseIn,
        QuadraticEaseOut,
        QuadraticEaseInOut,
        CubicEaseIn,
        CubicEaseOut,
        CubicEaseInOut,
        QuarticEaseIn,
        QuarticEaseOut,
        QuarticEaseInOut,
        QuinticEaseIn,
        QuinticEaseOut,
        QuinticEaseInOut,
        SineEaseIn,
        SineEaseOut,
        SineEaseInOut,
        CircularEaseIn,
        CircularEaseOut,
        CircularEaseInOut,
        ExponentialEaseIn,
        ExponentialEaseOut,
        ExponentialEaseInOut,
        ElasticEaseIn,
        ElasticEaseOut,
        ElasticEaseInOut,
        BackEaseIn,
        BackEaseOut,
        BackEaseInOut,
        BounceEaseIn,
        BounceEaseOut,
        BounceEaseInOut,
        InverseLinear,
        InverseQuarticEaseInOut
    }

    [Serializable]
    public abstract class ProjectionAnimationModifier
    {
        public int TargetFPS = 10;
    }

    [Serializable]
    public class TileScrollProjectionAnimationModifier : ProjectionAnimationModifier
    {
        public Vector2 ScrollSpeed;
    }

    [Serializable]
    public class RotationProjectionAnimationModifier : ProjectionAnimationModifier
    {
        public float RotationSpeed = 30;
    }

    [Serializable]
    public class SineProjectionAnimationModifier : ProjectionAnimationModifier
    {
        public ProjectionAxis AffectedAxis = ProjectionAxis.Y;
        public float Amplitude = 0.25f;
        public float Frequency = 1f;
    }


    [Serializable]
    public class ScaleProjectionAnimationModifier : ProjectionAnimationModifier
    {
        public float ScaleFactor = 1.5f;
        public EasingFunction EasingFunction = EasingFunction.Linear;
        public float Speed = 1.0f;
    }

    [Serializable]
    public class TextureSheetProjectionAnimationModifier : ProjectionAnimationModifier
    {
        public int RowImages = 2;
        public int ColumnImages = 2;
        public int LingerFrameIndex = 0;
        public int LingerDuration = 0;
    }

    [Serializable]
    public class RandomizerProjectionAnimationModifier : ProjectionAnimationModifier
    {
        public int RandomizeEveryXFrames = 1;
        public bool RandomizeRotation;
        public float2 RotationRange = new(0f, 360f);
        public bool RandomizeOffset;
        public Vector2 OoffsetRange = new(0.5f, 0.5f);
        public bool RandomizeScale;
        public float2 TilingRange = new(0.5f, 1.5f);
    }
}