using System.Collections.Generic;
using LoveBank.Core;
using LoveBank.Core.Domain;
using LoveBank.MVC.Security;

namespace LoveBank.Services.AdminModule
{
    public interface IAdminService : IServcie
    {
        /// <summary>
        /// ��������Ա
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        AdminUser CreateAdmin(AdminUser admin);

        /// <summary>
        /// ����Ա�Ƿ����
        /// </summary>
        /// <param name="adminName">����Ա�ʺ�</param>
        /// <returns></returns>
        bool ExistAdmin(string adminName);

        /// <summary>
        /// ��֤����Ա
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Validate(string userName, string password);

        /// <summary>
        /// ��֤����Ա
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        bool Validate(string userName, string password, out AdminUser admin);

        /// <summary>
        /// ��ý�ɫ�б�
        /// </summary>
        /// <returns></returns>
        IList<Role> GetRoles();

        /// <summary>
        /// ��û���վ�Ľ�ɫ�б�
        /// </summary>
        /// <returns></returns>
        IList<Role> GetTrashRoles();

        /// <summary>
        /// ����һ���µĽ�ɫ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="modules"></param>
        /// <param name="nodes"></param>
        /// <param name="isEffect"></param>
        /// <returns></returns>
        Role CreateRole(string name, string[] modules, string[] nodes, bool isEffect = true);

        /// <summary>
        /// ���¹���Ա����
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="isEffect"></param>
        void UpdateRole(int roleId, string roleName, bool isEffect);

        /// <summary>
        /// ���½�ɫ�ķ���Ȩ��
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="modules"></param>
        /// <param name="nodes"></param>
        void UpdateRoleAccess(int roleid, string[] modules, string[] nodes);

        /// <summary>
        /// ��ս�ɫ�����з���Ȩ��
        /// </summary>
        /// <param name="roleid"></param>
        void ClearRoleAccess(int roleid);

        /// <summary>
        /// ������ɫ
        /// </summary>
        /// <param name="roleid"></param>
        void TrashRole(int roleid);

        /// <summary>
        /// �ָ���ɫ
        /// </summary>
        /// <param name="roleid"></param>
        void RecoverRole(int roleid);

        /// <summary>
        /// �����Ƴ���ɫ
        /// </summary>
        /// <param name="roleid"></param>
        void RemoveRole(int roleid);

        /// <summary>
        /// ��ù���Ա
        /// </summary>
        /// <param name="name">����Ա�ʺ�����</param>
        /// <returns></returns>
        AdminUser GetAdmin(string name);

        /// <summary>
        /// ɾ������Ա
        /// </summary>
        /// <param name="id"></param>
        void DelAdmin(int[] id);

        /// <summary>
        /// ɾ������Ա
        /// </summary>
        /// <param name="id"></param>
        void DelAdmin(int id);

        /// <summary>
        /// ɾ������Ա��
        /// </summary>
        /// <param name="id"></param>
        void DelRole(int[] id);

        /// <summary>
        /// ����ɾ������Ա
        /// </summary>
        /// <param name="id"></param>
        void ForeverDelAdmin(int[] id);

        /// <summary>
        /// ����ɾ����ɫ
        /// </summary>
        /// <param name="id"></param>
        void ForeverDelRole(int[] id);

        /// <summary>
        /// ������վ�ָ�����Ա
        /// </summary>
        /// <param name="id"></param>
        void RestoreAdmin(int[] id);

        /// <summary>
        /// ������վ�ָ�����Ա��
        /// </summary>
        /// <param name="id"></param>
        void RestoreRole(int[] id);

        /// <summary>
        /// ����Ĭ�Ϲ���Ա
        /// </summary>
        /// <param name="adminId"></param>
        void SetDefaultAdmin(int adminId);

        /// <summary>
        /// ��ý�ɫ��Ȩ�޵�
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        PermissionCollection GetPermissions(int role);
    }
}