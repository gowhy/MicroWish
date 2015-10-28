using QDT.Core;
using QDT.Core.Members;

namespace QDT.Services.UserModule
{
    public interface IUserService : IServcie
    {
        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User CreateUser(User user);

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="user"></param>
        void UpdateUser(User user);

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="id"></param>
        void DeleteUser(int[] id);

        /// <summary>
        /// ����ɾ���û�
        /// </summary>
        /// <param name="id"></param>
        void ForeverDelUser(int[] id);

        /// <summary>
        /// �����û����
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns></returns>
        UserGroup CreateUserGroup(UserGroup userGroup);

        /// <summary>
        /// �����û����
        /// </summary>
        /// <param name="userGroup"></param>
        void UpdateUserGroup(UserGroup userGroup);

        /// <summary>
        /// ����ɾ���û����
        /// </summary>
        /// <param name="id"></param>
        void ForeverDelUserGroup(int[] id);

    }
}