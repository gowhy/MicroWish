using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace LoveBank.Common.Plugins.Email
{

    public class Smtp {
        /// <summary>
        /// �����ڲ������SmtpClientʵ��
        /// </summary>
        public SmtpClient SmtpClient { get; set; }

        #region   ���캯��

        #region SMTP������ ��Ҫ�����֤ƾ��

        /// <summary>
        /// ���� Smtp ʵ��
        /// </summary>
        /// <param name="host">���� SMTP ��������</param>
        /// <param name="port">�˿ں�</param>
        /// <param name="enableSsl">ָ�� SmtpClient �Ƿ�ʹ�ð�ȫ�׽��ֲ� (SSL) �������ӡ�</param>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
        public Smtp(string host, int port, bool enableSsl, string userName, string password)
        {
            Check.Argument.IsNotEmpty(host,"host");
            Check.Argument.IsNotEmpty(userName,"userName");
            Check.Argument.IsNotEmpty(password,"password");

            SmtpClient = new SmtpClient(host, port);

            SmtpClient.EnableSsl = enableSsl;
            SmtpClient.UseDefaultCredentials = false;
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClient.Credentials = new NetworkCredential(userName, password);
            SmtpClient.Timeout = 100000;
        }

        /// <summary>
        /// ���� Smtp ʵ��
        /// </summary>
        /// <param name="host">���� SMTP ��������</param>
        /// <param name="port">�˿ں�</param>
        /// <param name="enableSsl">ָ�� SmtpClient �Ƿ�ʹ�ð�ȫ�׽��ֲ� (SSL) �������ӡ�</param>
        /// <param name="credential">����������֤��������ݵ�ƾ�ݡ�</param>
        public Smtp(string host, int port, bool enableSsl, NetworkCredential credential)
        {
            Check.Argument.IsNotEmpty(host, "host");
            Check.Argument.IsNotNull(credential, "credential");

            SmtpClient = new SmtpClient(host, port);
            SmtpClient.EnableSsl = enableSsl;
            SmtpClient.UseDefaultCredentials = false;
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClient.Credentials = credential;
            SmtpClient.Timeout = 100000;
        }
        #endregion

        #region SMTP������ ���� useDefaultCredentials ����������SMTP�������Ƿ���ϵͳĬ��ƾ֤

        // useDefaultCredentials
        // false�������ӵ�������ʱ�Ὣ Credentials ���������õ�ֵ����ƾ�ݡ�
        //        ���UseDefaultCredentials��������Ϊ false ������δ���� Credentials ���ԣ����ʼ���������ʽ���͵���������
        //        ��SMTP ������Ҫ������֤�ͻ��˵��������׳��쳣����
        // true��System.Net.CredentialCache.DefaultCredentials ��Ӧ�ó���ϵͳƾ֤����������һ���͡�

        /// <summary>
        /// ���� Smtp ʵ��
        /// </summary>
        /// <param name="host">���� SMTP ��������</param>
        /// <param name="port">�˿ں�</param>
        /// <param name="enableSsl">ָ�� SmtpClient �Ƿ�ʹ�ð�ȫ�׽��ֲ� (SSL) �������ӡ�</param>
        /// <param name="useDefaultCredentials">SMTP�������Ƿ���ϵͳĬ��ƾ֤��</param>
        public Smtp(string host, int port, bool enableSsl, bool useDefaultCredentials)
        {
            Check.Argument.IsNotEmpty(host, "host");

            SmtpClient.Host = host;
            SmtpClient.Port = port;
            SmtpClient.EnableSsl = enableSsl;
            SmtpClient.UseDefaultCredentials = useDefaultCredentials;
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClient.Timeout = 100000;
        }

        #endregion

        #endregion

        /// <summary>
        /// ����SmtpClient.Send() ���õĳ�ʱʱ�䡣
        /// SmtpClientĬ�� Timeout =  ��100��=100*1000���룩��
        /// Ӧ�����ݡ��ʼ���С��������С�����ܺ�ʱ�������ؽ��е���
        /// </summary>
        public Smtp SetTimeout(int timeout)
        {
            if (timeout > 0)
            {
                SmtpClient.Timeout = timeout;                
            }
            return this;
        }

        /// <summary>
        /// ���� SmtpClient ��δ�������ĵ����ʼ���
        /// </summary>
        /// <param name="deliveryMethod">
        /// 0��Network��Ĭ�ϣ��������ʼ�ͨ�����緢�͵� SMTP ��������
        /// 1��SpecifiedPickupDirectory���������ʼ����Ƶ� SmtpClient.PickupDirectoryLocation ����ָ����Ŀ¼��Ȼ�����ⲿӦ�ó����͡�
        /// 2��PickupDirectoryFromIis���������ʼ����Ƶ�ʰȡĿ¼��Ȼ��ͨ������ Internet ��Ϣ���� (IIS) ���͡�
        /// </param>
        public Smtp SetDeliveryMethod(int deliveryMethod)
        {
            if (deliveryMethod < 0 || deliveryMethod > 2)
                deliveryMethod = 0;     //  Network��Ĭ�ϣ�

            SmtpClient.DeliveryMethod = (SmtpDeliveryMethod)deliveryMethod;

            return this;
        }

        /// <summary>
        /// ��ӽ�����ȫ�׽��ֲ� (SSL) ���ӵ�֤��
        /// </summary>
        public Smtp AddClientCertificate(X509Certificate certificate)
        {
            Check.Argument.IsNotNull(certificate, "certificate");

            SmtpClient.EnableSsl = true;
            SmtpClient.ClientCertificates.Add(certificate);

            return this;
        } 
    }
}