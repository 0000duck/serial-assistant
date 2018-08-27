using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace WPFSerialAssistant
{
    public static class Utilities
    {
        public static string BytesToText(List<byte> bytesBuffer, ReceiveMode mode, Encoding encoding)
        {
            StringBuilder sb = new StringBuilder();
            //string result = "";

            if (mode == ReceiveMode.Character)
            {
                return encoding.GetString(bytesBuffer.ToArray<byte>());
            }

            foreach (var item in bytesBuffer)
            {
                switch (mode)
                {
                    case ReceiveMode.Hex:
                        {
                            string tmp = Convert.ToString(item, 16).ToUpper();
                            if ( tmp.Length < 2)
                            {
                                sb.Append("0");
                            }
                            sb.Append(tmp).Append(" ");
                        }
                        break;
                    case ReceiveMode.Decimal:
                        sb.Append(Convert.ToString(item, 10)).Append(" ");
                        break;
                    case ReceiveMode.Octal:
                        sb.Append(Convert.ToString(item, 8)).Append(" ");
                        break;
                    case ReceiveMode.Binary:
                        {
                            string tmp = Convert.ToString(item, 2);
                            if ( tmp.Length < 8 )
                            {
                                sb.Append('0', 8 - tmp.Length);
                            }
                            sb.Append(tmp).Append(" ");
                        }
                        break;
                    default:
                        break;
                }
            }

            return sb.ToString();
        }

        public static string ToSpecifiedText(string text, SendMode mode, Encoding encoding)
        {
            string result = "";
            switch (mode)
            {
                case SendMode.Character:
                    text = text.Trim();

                    // 转换成字节
                    List<byte> src = new List<byte>();

                    string[] grp = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in grp)
                    {
                        try
                        {
                            byte data = Convert.ToByte(item, 16);
                            src.Add(data);
                        } catch
                        {

                        }
                    }

                    // 转换成字符串
                    result = encoding.GetString(src.ToArray<byte>());
                    break;
                    
                case SendMode.Hex:
                    
                    byte[] byteStr = encoding.GetBytes(text.ToCharArray());

                    foreach (var item in byteStr)
                    {
                        result += Convert.ToString(item, 16).ToUpper() + " ";
                    }
                    break;
                default:
                    break;
            }

            return result.Trim();
        }

    }
}
