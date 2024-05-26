using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Runtime.CompilerServices;
using VMFramework.Core.Generic;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [PreviewComposite]
    [TypeValidation]
    public abstract partial class KCubeConfig<TPoint> : BaseConfig, IKCubeConfig<TPoint>, ICloneable
        where TPoint : struct, IEquatable<TPoint>
    {
        protected const string WRAPPER_GROUP = "WrapperGroup";

        protected const string MIN_MAX_VALUE_GROUP =
            WRAPPER_GROUP + "/MinMaxValueGroup";

        protected const string INFO_VALUE_GROUP = WRAPPER_GROUP + "/InfoValueGroup";

        protected virtual string pointName => "点";

        protected virtual string sizeName => "尺寸";

        protected virtual bool requireCheckSize => true;

        [LabelText(@"@""最小"" + pointName"), HorizontalGroup(WRAPPER_GROUP),
         VerticalGroup(MIN_MAX_VALUE_GROUP)]
        [JsonProperty]
        public TPoint min;

        [LabelText(@"@""最大"" + pointName"), VerticalGroup(MIN_MAX_VALUE_GROUP)]
        [InfoBox(@"@""最大"" + pointName + ""不能小于最小"" + pointName",
            InfoMessageType.Error, nameof(displayMaxLessThanMinError))]
        [JsonProperty]
        public TPoint max;

        [LabelText("@" + nameof(sizeName)), VerticalGroup(INFO_VALUE_GROUP)]
        [ShowInInspector, DisplayAsString]
        public abstract TPoint size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        [LabelText("中心"), VerticalGroup(INFO_VALUE_GROUP)]
        [ShowInInspector, DisplayAsString]
        public abstract TPoint pivot
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #region GUI

        private bool displayMaxLessThanMinError => requireCheckSize && max
            .AnyNumberBelow(min);

        #endregion

        public abstract object Clone();

        public override string ToString()
        {
            return $"[{min}, {max}]";
        }
    }
}