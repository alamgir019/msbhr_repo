using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for clscashword
/// </summary>
namespace cashword.BLL
{
    public class clscashword
    {
        public string[] str_split(string str, char[] splitter)
        {
            string[] arInfo = new string[4];
            arInfo = str.Split(splitter);

            return arInfo;
        }

        public string getSplitData(string cash, bool flag)
        {
            string[] arInfo = new string[4];
            char[] splitter = { '.' };
            string csh = "";
            arInfo = str_split(cash, splitter);
            if (flag == true)
            {
                csh = arInfo[0];
            }
            else
            {
                csh = arInfo[1];
            }
            return csh;
        }

        public string getCashWord(string cash)
        {
            string word = "";
            string tmpword = "";
            long lngCash = 0;
            string strCash = "";
            string lenWord = "";
            string dgWord = "";
            string subCash = "";
            int len = 0;
            int i = 0;
            strCash = cash;
            cash = getSplitData(strCash, true);
            if (string.IsNullOrEmpty(cash) == false)
            {
                lngCash = Convert.ToInt64(cash);
                len = Convert.ToString(lngCash).Length;
            }
            else
            {
                len = 0;
            }

            if (len > 0)
            {
                if (len <= 2)
                {
                    dgWord = getDigitWord(Convert.ToInt16(lngCash));
                    word = dgWord;
                }
                else
                {
                    for (i = 0; i <= (Convert.ToString(lngCash).Trim()).Length; i++)
                    {
                        if (len <= 2)
                        {
                            if (len == 0)
                            {
                                break;
                            }
                            if (len == 1)
                            {
                                subCash = Convert.ToString(lngCash).Substring(0, 1);
                            }
                            if (len == 2)
                            {
                                subCash = Convert.ToString(lngCash).Substring(0, 2);
                            }
                            dgWord = getDigitWord(Convert.ToInt16(lngCash));
                            word = word + " " + dgWord;
                            break;
                        }
                        else
                        {
                            if (len == 5 || len == 7 || len == 10)
                            {
                                subCash = Convert.ToString(lngCash).Substring(0, 2);
                            }
                            else if (len == 9)
                            {
                                subCash = Convert.ToString(lngCash).Substring(0, 1);
                            }
                            else
                            {
                                subCash = Convert.ToString(lngCash).Substring(0, 1);
                            }

                            dgWord = getDigitWord(Convert.ToInt16(subCash));
                            if (len > 2)
                            {
                                lenWord = getLengthWord(len);
                                tmpword = dgWord + " " + lenWord;
                            }
                        }

                        word = word + " " + tmpword;
                        lngCash = getRemCash(lngCash, len, Convert.ToUInt16(subCash));
                        if (lngCash == 0)
                        {
                            break;
                        }
                        else
                        {
                            len = Convert.ToString(lngCash).Length;
                        }
                        i = 0;
                    }
                }
            }

            cash = getSplitData(strCash, false);
            string paisa = "";
            if (Convert.ToInt64(cash) != 0)
            {
                lngCash = Convert.ToInt64(cash);

                if (Convert.ToInt64(cash).ToString().Length == 2)
                {
                    subCash = Convert.ToString(lngCash).Substring(0, 2);
                }
                else
                {
                    subCash = Convert.ToString(lngCash).Substring(0, 1);
                }

                paisa = getDigitWord(Convert.ToInt16(subCash));
                paisa = " And " + paisa + " Paisa";
            }
            word = word + paisa + " Taka Only";
            return word;
        }

        private string getLengthWord(int len)
        {
            string lenWord = "";
            switch (len)
            {
                case 3:
                    lenWord = "Hundred"; 
                    break;
                case 4:
                    lenWord = "Thousand"; 
                    break;
                case 5:
                    lenWord = "Thousand"; 
                    break;
                case 6:
                    lenWord = "Lac"; 
                    break;
                case 7:
                    lenWord = "Lac"; 
                    break;
                case 8:
                    lenWord = "Crore"; 
                    break;
                case 9:
                    lenWord = "Billion"; 
                    break;
                case 10:
                    lenWord = "Billion"; 
                    break;
                //case 11:
                //    lenWord = "Billion"; break;
            }
            return lenWord;
        }

        private long getRemCash(long cash, int len, int value)
        {
            long cash2 = 0;
            switch (len)
            {
                case 3:
                    cash2 = cash - 100 * value;
                    break;
                case 4: cash2 = cash - 1000 * value;
                    break;
                case 5: cash2 = cash - 1000 * value;
                    break;
                case 6: cash2 = cash - 100000 * value;
                    break;
                case 7: cash2 = cash - 100000 * value;
                    break;
                case 8: cash2 = cash - 10000000 * value;
                    break;
                case 9: cash2 = cash - 100000000 * value;
                    break;
                case 10: cash2 = cash - 100000000 * value;
                    break;
                //case 11: cash2 = cash - 100000000 * value;
                //    break;
            }
            return cash2;
        }
        private string getDigitWord(int len)
        {
            string dgWord = "";
            switch (len)
            {
                case 1: dgWord = "One"; break;
                case 2: dgWord = "Two"; break;
                case 3: dgWord = "Three"; break;
                case 4: dgWord = "Four"; break;
                case 5: dgWord = "Five"; break;
                case 6: dgWord = "Six"; break;
                case 7: dgWord = "Seven"; break;
                case 8: dgWord = "Eight"; break;
                case 9: dgWord = "Nine"; break;

                case 10: dgWord = "Ten"; break;
                case 11: dgWord = "Eleven"; break;
                case 12: dgWord = "Twelve"; break;
                case 13: dgWord = "Thirteen"; break;
                case 14: dgWord = "Fourteen"; break;
                case 15: dgWord = "Fifteen"; break;
                case 16: dgWord = "Sixteen"; break;
                case 17: dgWord = "Seventeen"; break;
                case 18: dgWord = "Eighteen"; break;
                case 19: dgWord = "Nineteen"; break;

                case 20: dgWord = "Twenty"; break;
                case 21: dgWord = "Twenty One"; break;
                case 22: dgWord = "Twenty Two"; break;//case 14: dgWord = "Fourteen"; break;
                case 23: dgWord = "Twenty Three"; break;
                case 24: dgWord = "Twenty Four"; break;
                case 25: dgWord = "Twenty Five"; break;
                case 26: dgWord = "Twenty Six"; break;
                case 27: dgWord = "Twenty Seven"; break;
                case 28: dgWord = "Twenty Eight"; break;
                case 29: dgWord = "Twenty Nine"; break;

                case 30: dgWord = "Thirty"; break;
                case 31: dgWord = "Thirty One"; break;
                case 32: dgWord = "Thirty Two"; break;
                case 33: dgWord = "Thirty Three"; break;
                case 34: dgWord = "Thirty Four"; break;
                case 35: dgWord = "Thirty Five"; break;
                case 36: dgWord = "Thirty Six"; break;
                case 37: dgWord = "Thirty Seven"; break;
                case 38: dgWord = "Thirty Eight"; break;
                case 39: dgWord = "Thirty Nine"; break;

                case 40: dgWord = "Fourty"; break;
                case 41: dgWord = "Fourty One"; break;
                case 42: dgWord = "Fourty Two"; break;
                case 43: dgWord = "Fourty Three"; break;
                case 44: dgWord = "Fourty Four"; break;
                case 45: dgWord = "Fourty Five"; break;
                case 46: dgWord = "Fourty Six"; break;
                case 47: dgWord = "Fourty Seven"; break;
                case 48: dgWord = "Fourty Eight"; break;
                case 49: dgWord = "Fourty Nine"; break;

                case 50: dgWord = "Fifty"; break;
                case 51: dgWord = "Fifty One"; break;
                case 52: dgWord = "Fifty Two"; break;
                case 53: dgWord = "Fifty Three"; break;
                case 54: dgWord = "Fifty Four"; break;
                case 55: dgWord = "Fifty Five"; break;
                case 56: dgWord = "Fifty Six"; break;
                case 57: dgWord = "Fifty Seven"; break;
                case 58: dgWord = "Fifty Eight"; break;
                case 59: dgWord = "Fifty Nine"; break;

                case 60: dgWord = "Sixty"; break;
                case 61: dgWord = "Sixty One"; break;
                case 62: dgWord = "Sixty Two"; break;
                case 63: dgWord = "Sixty Three"; break;
                case 64: dgWord = "Sixty Four"; break;
                case 65: dgWord = "Sixty Five"; break;
                case 66: dgWord = "Sixty Six"; break;
                case 67: dgWord = "Sixty Seven"; break;
                case 68: dgWord = "Sixty Eight"; break;
                case 69: dgWord = "Sixty Nine"; break;

                case 70: dgWord = "Seventy"; break;
                case 71: dgWord = "Seventy One"; break;
                case 72: dgWord = "Seventy Two"; break;
                case 73: dgWord = "Seventy Three"; break;
                case 74: dgWord = "Seventy Four"; break;
                case 75: dgWord = "Seventy Five"; break;
                case 76: dgWord = "Seventy Six"; break;
                case 77: dgWord = "Seventy Seven"; break;
                case 78: dgWord = "Seventy Eight"; break;
                case 79: dgWord = "Seventy Nine"; break;

                case 80: dgWord = "Eighty"; break;
                case 81: dgWord = "Eighty One"; break;
                case 82: dgWord = "Eighty Two"; break;
                case 83: dgWord = "Eighty Three"; break;
                case 84: dgWord = "Eighty Four"; break;
                case 85: dgWord = "Eighty Five"; break;
                case 86: dgWord = "Eighty Six"; break;
                case 87: dgWord = "Eighty Seven"; break;
                case 88: dgWord = "Eighty Eight"; break;
                case 89: dgWord = "Eighty Nine"; break;

                case 90: dgWord = "Ninty"; break;
                case 91: dgWord = "Ninty One"; break;
                case 92: dgWord = "Ninty Two"; break;
                case 93: dgWord = "Ninty Three"; break;
                case 94: dgWord = "Ninty Four"; break;
                case 95: dgWord = "Ninty Five"; break;
                case 96: dgWord = "Ninty Six"; break;
                case 97: dgWord = "Ninty Seven"; break;
                case 98: dgWord = "Ninty Eight"; break;
                case 99: dgWord = "Ninty Nine"; break;
            }
            return dgWord;
        }
        public clscashword()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
