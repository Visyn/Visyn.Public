using Visyn.Public.HTML;

namespace Visyn.Public.Mathematics
{
    public interface IColor : IHtmlColor
    {
        byte R { get; }
        byte G { get; }
        byte B { get; }
        byte Alpha { get; }
    }

    public struct ColorStruct : IColor
    {
        #region Implementation of IColor

        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public byte Alpha { get; }

        #endregion
        public ColorStruct(byte red, byte green, byte blue, byte alpha=0)
        {
            R = red;
            G = green;
            B = blue;
            Alpha = alpha;
        }

        public static ColorStruct AliceBlue => new ColorStruct(240, 248, 255);
        public static ColorStruct AntiqueWhite => new ColorStruct(250, 235, 215);
        public static ColorStruct Aqua => new ColorStruct(0, 255, 255);
        public static ColorStruct Aquamarine => new ColorStruct(127, 255, 212);
        public static ColorStruct Azure => new ColorStruct(240, 255, 255);
        public static ColorStruct Beige => new ColorStruct(245, 245, 220);
        public static ColorStruct Bisque => new ColorStruct(255, 228, 196);
        public static ColorStruct Black => new ColorStruct(0, 0, 0);
        public static ColorStruct BlanchedAlmond => new ColorStruct(255, 235, 205);
        public static ColorStruct Blue => new ColorStruct(0, 0, 255);
        public static ColorStruct BlueViolet => new ColorStruct(138, 43, 226);
        public static ColorStruct Brown => new ColorStruct(165, 42, 42);
        public static ColorStruct BurlyWood => new ColorStruct(222, 184, 135);
        public static ColorStruct CadetBlue => new ColorStruct(95, 158, 160);
        public static ColorStruct Chartreuse => new ColorStruct(127, 255, 0);
        public static ColorStruct Chocolate => new ColorStruct(210, 105, 30);
        public static ColorStruct Coral => new ColorStruct(255, 127, 80);
        public static ColorStruct CornflowerBlue => new ColorStruct(100, 149, 237);
        public static ColorStruct Cornsilk => new ColorStruct(255, 248, 220);
        public static ColorStruct Crimson => new ColorStruct(220, 20, 60);
        public static ColorStruct Cyan => new ColorStruct(0, 255, 255);
        public static ColorStruct DarkBlue => new ColorStruct(0, 0, 139);
        public static ColorStruct DarkCyan => new ColorStruct(0, 139, 139);
        public static ColorStruct DarkGoldenRod => new ColorStruct(184, 134, 11);
        public static ColorStruct DarkGray => new ColorStruct(169, 169, 169);
        public static ColorStruct DarkGrey => new ColorStruct(169, 169, 169);
        public static ColorStruct DarkGreen => new ColorStruct(0, 100, 0);
        public static ColorStruct DarkKhaki => new ColorStruct(189, 183, 107);
        public static ColorStruct DarkMagenta => new ColorStruct(139, 0, 139);
        public static ColorStruct DarkOliveGreen => new ColorStruct(85, 107, 47);
        public static ColorStruct DarkOrange => new ColorStruct(255, 140, 0);
        public static ColorStruct DarkOrchid => new ColorStruct(153, 50, 204);
        public static ColorStruct DarkRed => new ColorStruct(139, 0, 0);
        public static ColorStruct DarkSalmon => new ColorStruct(233, 150, 122);
        public static ColorStruct DarkSeaGreen => new ColorStruct(143, 188, 143);
        public static ColorStruct DarkSlateBlue => new ColorStruct(72, 61, 139);
        public static ColorStruct DarkSlateGray => new ColorStruct(47, 79, 79);
        public static ColorStruct DarkSlateGrey => new ColorStruct(47, 79, 79);
        public static ColorStruct DarkTurquoise => new ColorStruct(0, 206, 209);
        public static ColorStruct DarkViolet => new ColorStruct(148, 0, 211);
        public static ColorStruct DeepPink => new ColorStruct(255, 20, 147);
        public static ColorStruct DeepSkyBlue => new ColorStruct(0, 191, 255);
        public static ColorStruct DimGray => new ColorStruct(105, 105, 105);
        public static ColorStruct DimGrey => new ColorStruct(105, 105, 105);
        public static ColorStruct DodgerBlue => new ColorStruct(30, 144, 255);
        public static ColorStruct FireBrick => new ColorStruct(178, 34, 34);
        public static ColorStruct FloralWhite => new ColorStruct(255, 250, 240);
        public static ColorStruct ForestGreen => new ColorStruct(34, 139, 34);
        public static ColorStruct Fuchsia => new ColorStruct(255, 0, 255);
        public static ColorStruct Gainsboro => new ColorStruct(220, 220, 220);
        public static ColorStruct GhostWhite => new ColorStruct(248, 248, 255);
        public static ColorStruct Gold => new ColorStruct(255, 215, 0);
        public static ColorStruct GoldenRod => new ColorStruct(218, 165, 32);
        public static ColorStruct Gray => new ColorStruct(128, 128, 128);
        public static ColorStruct Grey => new ColorStruct(128, 128, 128);
        public static ColorStruct Green => new ColorStruct(0, 128, 0);
        public static ColorStruct GreenYellow => new ColorStruct(173, 255, 47);
        public static ColorStruct HoneyDew => new ColorStruct(240, 255, 240);
        public static ColorStruct HotPink => new ColorStruct(255, 105, 180);
        public static ColorStruct IndianRed => new ColorStruct(205, 92, 92);
        public static ColorStruct Indigo => new ColorStruct(75, 0, 130);
        public static ColorStruct Ivory => new ColorStruct(255, 255, 240);
        public static ColorStruct Khaki => new ColorStruct(240, 230, 140);
        public static ColorStruct Lavender => new ColorStruct(230, 230, 250);
        public static ColorStruct LavenderBlush => new ColorStruct(255, 240, 245);
        public static ColorStruct LawnGreen => new ColorStruct(124, 252, 0);
        public static ColorStruct LemonChiffon => new ColorStruct(255, 250, 205);
        public static ColorStruct LightBlue => new ColorStruct(173, 216, 230);
        public static ColorStruct LightCoral => new ColorStruct(240, 128, 128);
        public static ColorStruct LightCyan => new ColorStruct(224, 255, 255);
        public static ColorStruct LightGoldenRodYellow => new ColorStruct(250, 250, 210);
        public static ColorStruct LightGray => new ColorStruct(211, 211, 211);
        public static ColorStruct LightGrey => new ColorStruct(211, 211, 211);
        public static ColorStruct LightGreen => new ColorStruct(144, 238, 144);
        public static ColorStruct LightPink => new ColorStruct(255, 182, 193);
        public static ColorStruct LightSalmon => new ColorStruct(255, 160, 122);
        public static ColorStruct LightSeaGreen => new ColorStruct(32, 178, 170);
        public static ColorStruct LightSkyBlue => new ColorStruct(135, 206, 250);
        public static ColorStruct LightSlateGray => new ColorStruct(119, 136, 153);
        public static ColorStruct LightSlateGrey => new ColorStruct(119, 136, 153);
        public static ColorStruct LightSteelBlue => new ColorStruct(176, 196, 222);
        public static ColorStruct LightYellow => new ColorStruct(255, 255, 224);
        public static ColorStruct Lime => new ColorStruct(0, 255, 0);
        public static ColorStruct LimeGreen => new ColorStruct(50, 205, 50);
        public static ColorStruct Linen => new ColorStruct(250, 240, 230);
        public static ColorStruct Magenta => new ColorStruct(255, 0, 255);
        public static ColorStruct Maroon => new ColorStruct(128, 0, 0);
        public static ColorStruct MediumAquaMarine => new ColorStruct(102, 205, 170);
        public static ColorStruct MediumBlue => new ColorStruct(0, 0, 205);
        public static ColorStruct MediumOrchid => new ColorStruct(186, 85, 211);
        public static ColorStruct MediumPurple => new ColorStruct(147, 112, 219);
        public static ColorStruct MediumSeaGreen => new ColorStruct(60, 179, 113);
        public static ColorStruct MediumSlateBlue => new ColorStruct(123, 104, 238);
        public static ColorStruct MediumSpringGreen => new ColorStruct(0, 250, 154);
        public static ColorStruct MediumTurquoise => new ColorStruct(72, 209, 204);
        public static ColorStruct MediumVioletRed => new ColorStruct(199, 21, 133);
        public static ColorStruct MidnightBlue => new ColorStruct(25, 25, 112);
        public static ColorStruct MintCream => new ColorStruct(245, 255, 250);
        public static ColorStruct MistyRose => new ColorStruct(255, 228, 225);
        public static ColorStruct Moccasin => new ColorStruct(255, 228, 181);
        public static ColorStruct NavajoWhite => new ColorStruct(255, 222, 173);
        public static ColorStruct Navy => new ColorStruct(0, 0, 128);
        public static ColorStruct OldLace => new ColorStruct(253, 245, 230);
        public static ColorStruct Olive => new ColorStruct(128, 128, 0);
        public static ColorStruct OliveDrab => new ColorStruct(107, 142, 35);
        public static ColorStruct Orange => new ColorStruct(255, 165, 0);
        public static ColorStruct OrangeRed => new ColorStruct(255, 69, 0);
        public static ColorStruct Orchid => new ColorStruct(218, 112, 214);
        public static ColorStruct PaleGoldenRod => new ColorStruct(238, 232, 170);
        public static ColorStruct PaleGreen => new ColorStruct(152, 251, 152);
        public static ColorStruct PaleTurquoise => new ColorStruct(175, 238, 238);
        public static ColorStruct PaleVioletRed => new ColorStruct(219, 112, 147);
        public static ColorStruct PapayaWhip => new ColorStruct(255, 239, 213);
        public static ColorStruct PeachPuff => new ColorStruct(255, 218, 185);
        public static ColorStruct Peru => new ColorStruct(205, 133, 63);
        public static ColorStruct Pink => new ColorStruct(255, 192, 203);
        public static ColorStruct Plum => new ColorStruct(221, 160, 221);
        public static ColorStruct PowderBlue => new ColorStruct(176, 224, 230);
        public static ColorStruct Purple => new ColorStruct(128, 0, 128);
        public static ColorStruct RebeccaPurple => new ColorStruct(102, 51, 153);
        public static ColorStruct Red => new ColorStruct(255, 0, 0);
        public static ColorStruct RosyBrown => new ColorStruct(188, 143, 143);
        public static ColorStruct RoyalBlue => new ColorStruct(65, 105, 225);
        public static ColorStruct SaddleBrown => new ColorStruct(139, 69, 19);
        public static ColorStruct Salmon => new ColorStruct(250, 128, 114);
        public static ColorStruct SandyBrown => new ColorStruct(244, 164, 96);
        public static ColorStruct SeaGreen => new ColorStruct(46, 139, 87);
        public static ColorStruct SeaShell => new ColorStruct(255, 245, 238);
        public static ColorStruct Sienna => new ColorStruct(160, 82, 45);
        public static ColorStruct Silver => new ColorStruct(192, 192, 192);
        public static ColorStruct SkyBlue => new ColorStruct(135, 206, 235);
        public static ColorStruct SlateBlue => new ColorStruct(106, 90, 205);
        public static ColorStruct SlateGray => new ColorStruct(112, 128, 144);
        public static ColorStruct SlateGrey => new ColorStruct(112, 128, 144);
        public static ColorStruct Snow => new ColorStruct(255, 250, 250);
        public static ColorStruct SpringGreen => new ColorStruct(0, 255, 127);
        public static ColorStruct SteelBlue => new ColorStruct(70, 130, 180);
        public static ColorStruct Tan => new ColorStruct(210, 180, 140);
        public static ColorStruct Teal => new ColorStruct(0, 128, 128);
        public static ColorStruct Thistle => new ColorStruct(216, 191, 216);
        public static ColorStruct Tomato => new ColorStruct(255, 99, 71);
        public static ColorStruct Turquoise => new ColorStruct(64, 224, 208);
        public static ColorStruct Violet => new ColorStruct(238, 130, 238);
        public static ColorStruct Wheat => new ColorStruct(245, 222, 179);
        public static ColorStruct White => new ColorStruct(255, 255, 255);
        public static ColorStruct WhiteSmoke => new ColorStruct(245, 245, 245);
        public static ColorStruct Yellow => new ColorStruct(255, 255, 0);
        public static ColorStruct YellowGreen => new ColorStruct(154, 205, 50);

        #region Implementation of IHtmlColor

        public string GetHtmlColor() => this.HtmlString();

        #endregion

        #region Overrides of ValueType

        /// <summary>Returns the fully qualified type name of this instance.</summary>
        /// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
        public override string ToString() => Alpha > 0 ? $"{R} {G} {B} - {Alpha}" : $"{R} {G} {B}";

        #endregion
    }
}
