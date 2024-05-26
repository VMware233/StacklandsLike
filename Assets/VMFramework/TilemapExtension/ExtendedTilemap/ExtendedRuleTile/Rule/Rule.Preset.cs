namespace VMFramework.ExtendedTilemap
{
    public partial class Rule
    {
        public static Rule UpperLeftOnly(LimitType upperLowerLeftRightType,
            LimitType upperLeft)
        {
            return new()
            {
                left = upperLowerLeftRightType,
                right = upperLowerLeftRightType,
                upper = upperLowerLeftRightType,
                lower = upperLowerLeftRightType,
                upperLeft = upperLeft
            };
        }

        public static Rule UpperRightOnly(LimitType upperLowerLeftRightType,
            LimitType upperRight)
        {
            return new()
            {
                left = upperLowerLeftRightType,
                right = upperLowerLeftRightType,
                upper = upperLowerLeftRightType,
                lower = upperLowerLeftRightType,
                upperRight = upperRight
            };
        }

        public static Rule LowerLeftOnly(LimitType upperLowerLeftRightType,
            LimitType lowerLeft)
        {
            return new()
            {
                left = upperLowerLeftRightType,
                right = upperLowerLeftRightType,
                upper = upperLowerLeftRightType,
                lower = upperLowerLeftRightType,
                lowerLeft = lowerLeft
            };
        }

        public static Rule LowerRightOnly(LimitType upperLowerLeftRightType,
            LimitType lowerRight)
        {
            return new()
            {
                left = upperLowerLeftRightType,
                right = upperLowerLeftRightType,
                upper = upperLowerLeftRightType,
                lower = upperLowerLeftRightType,
                lowerRight = lowerRight
            };
        }
    }
}