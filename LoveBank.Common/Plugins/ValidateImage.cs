using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace LoveBank.Common.Plugins
{
    public class ValidateImage
    {
        /// <summary>
        ///     ������֤��
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string CreateValidateCode(int length)
        {
            var randMembers = new int[length];
            var validateNums = new int[length];
            string validateNumberStr = "";
            //������ʼ����ֵ
            var seekSeek = unchecked((int) DateTime.Now.Ticks);
            var seekRand = new Random(seekSeek);
            int beginSeek = seekRand.Next(0, Int32.MaxValue - length*10000);
            var seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //�����������
            for (int i = 0; i < length; i++)
            {
                var rand = new Random(seeks[i]);
                int pownum = 1*(int) Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //��ȡ�������
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                var rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //������֤��
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }

        /// <summary>
        ///     ������֤���ͼƬ
        /// </summary>
        /// <param name="validateCode"></param>
        /// <returns></returns>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            var image = new Bitmap((int) Math.Ceiling(validateCode.Length*16.0), 26);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //�������������
                var random = new Random();
                //���ͼƬ����ɫ
                g.Clear(Color.White);
                //��ͼƬ�ĸ�����
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                var font = new Font("Arial", 14, (FontStyle.Bold | FontStyle.Italic));
                var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                                                    Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //��ͼƬ��ǰ�����ŵ�
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //��ͼƬ�ı߿���
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //����ͼƬ����
                var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //���ͼƬ��
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}