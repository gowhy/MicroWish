using LoveBank.Common;

namespace LoveBank.P2B.Domain.Messages
{
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public enum MsgType {
        /// <summary>
        /// ����
        /// </summary>
        [EnumItemDescription("����")]
        SMS = 0,
        /// <summary>
        /// �ʼ�
        /// </summary>
        [EnumItemDescription("�ʼ�")]
        Email = 1
    }
}