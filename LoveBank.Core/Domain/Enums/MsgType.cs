using LoveBank.Common;

namespace LoveBank.Core.Domain.Enums {
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public enum MsgType {
        /// <summary>
        /// ����
        /// </summary>
        [EnumItemDescription("����")]
        ShortMsg = 0,
        /// <summary>
        /// �ʼ�
        /// </summary>
        [EnumItemDescription("�ʼ�")]
        Email = 1
    }
}