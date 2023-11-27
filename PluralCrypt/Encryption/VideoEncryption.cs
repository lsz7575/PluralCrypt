namespace PluralCrypt.Encryption
{
    public class VideoEncryption
    {
        private static int currentClipReadCrypto;

        private static string string1V2 = "pluralsight";

        private static string string2V2 = "#©>Å£Q\u0005¤°";

        private const string String1 = "pluralsight";

        private const string String2 = "\u0006?zY¢²\u0085\u009fL¾î0Ö.ì\u0017#©>Å£Q\u0005¤°\u00018Þ^\u008eú\u0019Lqß'\u009d\u0003ßE\u009eM\u0080'x:\0~¹\u0001ÿ 4³õ\u0003Ã§Ê\u000eAË¼\u0090è\u009eî~\u008b\u009aâ\u001b\u00b8UD<\u007fKç*\u001döæ7H\v\u0015Arý*v÷%Âþ¾ä;pü";

        internal static readonly string[][] CryptoKeys = new string[5][]
        {
        new string[2] { "pluralsight", "\u0006?zY¢²\u0085\u009fL¾î0Ö.ì\u0017#©>Å£Q\u0005¤°\u00018Þ^\u008eú\u0019Lqß'\u009d\u0003ßE\u009eM\u0080'x:\0~¹\u0001ÿ 4³õ\u0003Ã§Ê\u000eAË¼\u0090è\u009eî~\u008b\u009aâ\u001b\u00b8UD<\u007fKç*\u001döæ7H\v\u0015Arý*v÷%Âþ¾ä;pü" },
        new string[2] { String1V2, String2V2 },
        new string[1] { "os22$!sKHyy9jnGlgHB&vP21CK96tx!l2uhK1K%Fbubree9%o0wT44zwvJ446iAdA%M!@RopKCmOWMOqTt1*BIw@lF68x3itctw" },
        new string[1] { "XlmDvIlD*^uyZAfCMZ%M0h#o6Z7!4eMZZSBs@dZ12%rMvubV#2iFJLfh8@LSyVWhu37#b%z3MCF3u4244%SRMBO@zIG2YEi!i6y" },
        new string[1] { "YUg2z3ev2G7WpeqYFf6B2Acw68pJPBgNbJzf39Bs2hZEpGRkGcsoqbNV2GRcfanLgRFG3pE8bp6LjViDryFCxACwuRh2jF2a4CZN" }
        };

        public static string String1V2
        {
            get
            {
                return string1V2;
            }
            set
            {
                CryptoKeys[1][0] = value;
                string1V2 = value;
            }
        }

        public static string String2V2
        {
            get
            {
                return string2V2;
            }
            set
            {
                CryptoKeys[1][1] = value;
                string2V2 = value;
            }
        }

        private static void XorBuffer(byte[] buff, int length, long position)
        {
            XorBuffer(currentClipReadCrypto, buff, length, position);
        }

        internal static void XorBuffer(int key, byte[] buff, int length, long position)
        {
            for (int i = 0; i < length; i++)
            {
                string[] array = CryptoKeys[key];
                string text = array[0];
                int num = (int)position + i;
                char c = text[num % text.Length];
                for (int j = 1; j < array.Length; j++)
                {
                    text = array[j];
                    c ^= text[num % text.Length];
                }

                int num2 = c ^ (num % 251);
                buff[i] = (byte)(buff[i] ^ num2);
            }
        }

        public static void EncryptBuffer(byte[] buff, int length, long position)
        {
            XorBuffer(CryptoKeys.Length - 1, buff, length, position);
        }

        public static void DecryptBuffer(byte[] buff, int length, long position)
        {
            if (position == 0L)
            {
                for (int num = CryptoKeys.Length - 1; num >= 0; num--)
                {
                    currentClipReadCrypto = num;
                    XorBuffer(buff, length, position);
                    bool flag = buff.Length != 0;
                    for (int i = 0; i < buff.Length && i < 3; i++)
                    {
                        flag = flag && buff[i] == 0;
                    }

                    if (flag)
                    {
                        return;
                    }

                    XorBuffer(buff, length, position);
                }
            }

            XorBuffer(buff, length, position);
        }
    }
}

