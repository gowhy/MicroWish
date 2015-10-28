using LoveBank.Core.Domain;
using LoveBank.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveBank.Core.MSData.Mapping
{
    public class SeekHelperRecordeMap:EntityTypeConfiguration<SeekHelperRecorde>
    {
        public SeekHelperRecordeMap()
        {
            ToTable("SeekHelperRecorde");
        }
    }
}
