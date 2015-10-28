

using LoveBank.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoveBank.Core.MSData.Mapping.Members;
using LoveBank.Core.MSData.Mapping;
using LoveBank.Core.Members;
using LoveBank.Core.Domain;
namespace LoveBank.Core.MSData
{
    public partial class LoveBankDBContext : BaseContext
    {
        public LoveBankDBContext()
            : base("conn_MicroWish_db")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new RoleMapping());
            modelBuilder.Configurations.Add(new AdminMapping());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new MachineMap());
            modelBuilder.Configurations.Add(new LoveBank_AdMap());
            modelBuilder.Configurations.Add(new SourceFileMap());
            modelBuilder.Configurations.Add(new VolMap());
            modelBuilder.Configurations.Add(new VolAddScoreRecordeMap());
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new SMSMap());
            modelBuilder.Configurations.Add(new InfoManageMap());
            modelBuilder.Configurations.Add(new SeekHelperMap());
            modelBuilder.Configurations.Add(new SeekHelperRecordeMap());
            modelBuilder.Configurations.Add(new UnionHelpPojectMap());
            modelBuilder.Configurations.Add(new UnionHelpPojectDetailMap());
            modelBuilder.Configurations.Add(new AppVerMap());
            

            

        }
   
        //public virtual DbSet<User> T_User { get; set; }
        public virtual DbSet<Role> T_Role { get; set; }
        public virtual DbSet<AdminUser> T_AdminUser { get; set; }
        public virtual DbSet<Department> T_Department { get; set; }
        public virtual DbSet<Machine> T_Machine { get; set; }
        public virtual DbSet<LoveBank_Ad> T_LoveBank_Ad { get; set; }
        public virtual DbSet<SourceFile> T_SourceFile { get; set; }
        public virtual DbSet<Vol> T_Vol { get; set; }
        public virtual DbSet<VolAddScoreRecorde> T_VolAddScoreRecorde { get; set; }

        public virtual DbSet<AppUser> T_AppUser { get; set; }
        public virtual DbSet<SMS> T_SMS { get; set; }

        public virtual DbSet<InfoManage> T_InfoManage { get; set; }

        public virtual DbSet<SeekHelper> T_SeekHelper { get; set; }
        public virtual DbSet<SeekHelperRecorde> T_SeekHelperRecorde { get; set; }

        public virtual DbSet<UnionHelpPoject> T_UnionHelpPoject { get; set; }
        public virtual DbSet<UnionHelpPojectDetail> T_UnionHelpPojectDetail { get; set; }
        public virtual DbSet<AppVer> T_AppVer { get; set; }
        

        
    }
}
