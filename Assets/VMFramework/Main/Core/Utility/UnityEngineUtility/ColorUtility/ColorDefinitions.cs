using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ColorDefinitions
    {
        public static Color zero = new(0, 0, 0, 0);
        public static Color one = new(1, 1, 1, 1);
        
        public static Color slateGray = new(0.4392157f, 0.5019608f, 0.5647059f, 1.0f);
        public static Color black = new(0.0f, 0.0f, 0.0f, 1.0f);
        public static Color lightBlack = new(0.211764708f, 0.211764708f, 0.211764708f, 1.0f);
        public static Color darkSlateGray = new(0.184313729f, 0.309803933f, 0.309803933f, 1.0f);
        public static Color darkGrey = new(0.6627451f, 0.6627451f, 0.6627451f, 1.0f);
        public static Color dimGray = new(0.4117647f, 0.4117647f, 0.4117647f, 1.0f);
        public static Color gray = new(0.745098054f, 0.745098054f, 0.745098054f, 1.0f);
        public static Color lightGray = new(0.827451f, 0.827451f, 0.827451f, 1.0f);
        public static Color ghostWhite = new(0.972549f, 0.972549f, 1.0f, 1.0f);
        public static Color white = new(1.0f, 1.0f, 1.0f, 1.0f);
        public static Color springGreen = new(0.0f, 1.0f, 0.498039216f, 1.0f);
        public static Color lightGreen = new(0.5647059f, 0.933333337f, 0.5647059f, 1.0f);
        public static Color greenYellow = new(0.6784314f, 1.0f, 0.184313729f, 1.0f);
        public static Color seaGreen = new(0.180392161f, 0.545098066f, 0.34117648f, 1.0f);
        public static Color oliveDrab = new(0.419607848f, 0.5568628f, 0.137254909f, 1.0f);
        public static Color darkOliveGreen = new(0.333333343f, 0.419607848f, 0.184313729f, 1.0f);
        public static Color darkGreen = new(0.0f, 0.392156869f, 0.0f, 1.0f);
        public static Color green = new(0.0f, 1.0f, 0.0f, 1.0f);
        public static Color brown = new(0.5019608f, 0.2509804f, 0.0f, 1.0f);
        public static Color firebrick = new(0.698039234f, 0.13333334f, 0.13333334f, 1.0f);
        public static Color indianRed = new(0.8039216f, 0.360784322f, 0.360784322f, 1.0f);
        public static Color maroon = new(0.5660378f, 0.243350461f, 0.243350461f, 1.0f);
        public static Color darkRed = new(0.545098066f, 0.0f, 0.0f, 1.0f);
        public static Color red = new(1.0f, 0.0f, 0.0f, 1.0f);
        public static Color turquoise = new(0.2509804f, 0.8784314f, 0.8156863f, 1.0f);
        public static Color midnightBlue = new(0.09803922f, 0.09803922f, 0.4392157f, 1.0f);
        public static Color blue = new(0.0f, 0.0f, 1.0f, 1.0f);
        public static Color gold = new(1.0f, 0.843137264f, 0.0f, 1.0f);
        public static Color yellow = new(1.0f, 1.0f, 0.0f, 1.0f);
        public static Color lightSeaGreen = new(0.1254902f, 0.698039234f, 0.6666667f, 1.0f);
        public static Color dodgerBlue = new(0.117647059f, 0.5647059f, 1.0f, 1.0f);
        public static Color deepSkyBlue = new(0.0f, 0.7490196f, 1.0f, 1.0f);
        public static Color skyBlue = new(0.5294118f, 0.807843149f, 0.921568632f, 1.0f);
        public static Color azure = new(0.9411765f, 1.0f, 1.0f, 1.0f);
        public static Color aqua = new(0.0f, 1.0f, 1.0f, 1.0f);
        public static Color mediumOrchid = new(0.7294118f, 0.333333343f, 0.827451f, 1.0f);
        public static Color violet = new(0.933333337f, 0.509803951f, 0.933333337f, 1.0f);
        public static Color plum = new(0.8666667f, 0.627451f, 0.8666667f, 1.0f);
        public static Color slateBlue = new(0.41568628f, 0.3529412f, 0.8039216f, 1.0f);
        public static Color mediumPurple = new(0.5764706f, 0.4392157f, 0.858823538f, 1.0f);
        public static Color purple = new(0.627451f, 0.1254902f, 0.9411765f, 1.0f);
        public static Color darkMagenta = new(0.545098066f, 0.0f, 0.545098066f, 1.0f);
        public static Color magenta = new(1.0f, 0.0f, 1.0f, 1.0f);
        public static Color tomato = new(1.0f, 0.3882353f, 0.2784314f, 1.0f);
        public static Color coral = new(1.0f, 0.498039216f, 0.3137255f, 1.0f);
        public static Color orange = new(1.0f, 0.5019608f, 0.0f, 1.0f);
        public static Color hotPink = new(1.0f, 0.4117647f, 0.7058824f, 1.0f);
        public static Color thistle = new(0.847058833f, 0.7490196f, 0.847058833f, 1.0f);
        public static Color pink = new(1.0f, 0.7529412f, 0.796078444f, 1.0f);
        public static Color wheat = new(0.9607843f, 0.870588243f, 0.7019608f, 1.0f);

        public static readonly IReadOnlyDictionary<string, Color> colorDictionary =
            new Dictionary<string, Color>()
            {
                { "slateGray", slateGray },
                { "black", black },
                { "lightBlack", lightBlack },
                { "darkSlateGray", darkSlateGray },
                { "darkGrey", darkGrey },
                { "dimGray", dimGray },
                { "gray", gray },
                { "lightGray", lightGray },
                { "ghostWhite", ghostWhite },
                { "white", white },
                { "springGreen", springGreen },
                { "lightGreen", lightGreen },
                { "greenYellow", greenYellow },
                { "seaGreen", seaGreen },
                { "oliveDrab", oliveDrab },
                { "darkOliveGreen", darkOliveGreen },
                { "darkGreen", darkGreen },
                { "green", green },
                { "brown", brown },
                { "firebrick", firebrick },
                { "indianRed", indianRed },
                { "maroon", maroon },
                { "darkRed", darkRed },
                { "red", red },
                { "turquoise", turquoise },
                { "midnightBlue", midnightBlue },
                { "blue", blue },
                { "gold", gold },
                { "yellow", yellow },
                { "lightSeaGreen", lightSeaGreen },
                { "dodgerBlue", dodgerBlue },
                { "deepSkyBlue", deepSkyBlue },
                { "skyBlue", skyBlue },
                { "azure", azure },
                { "aqua", aqua },
                { "mediumOrchid", mediumOrchid },
                { "violet", violet },
                { "plum", plum },
                { "slateBlue", slateBlue },
                { "mediumPurple", mediumPurple },
                { "purple", purple },
                { "darkMagenta", darkMagenta },
                { "magenta", magenta },
                { "tomato", tomato },
                { "coral", coral },
                { "orange", orange },
                { "hotPink", hotPink },
                { "thistle", thistle },
                { "pink", pink },
                { "wheat", wheat }
            };
    }
}
