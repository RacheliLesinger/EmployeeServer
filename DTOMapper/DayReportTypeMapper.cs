using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DTO;

namespace DTOMapper
{
    public class DayReportTypeMapper
    {
        public static DayReportTypeDTO DALToDTO(DayReportType dr)
        {
            return new DayReportTypeDTO
            {
                Id = dr.Id,
                value = dr.Value
            };
        }

        public static List<DayReportTypeDTO> DALsToDTOs(List<DayReportType> types)
        {

            List<DayReportTypeDTO> DTOList = new List<DayReportTypeDTO>();
            types.ForEach(type => DTOList.Add(DALToDTO(type)));
            return DTOList;
        }

        public static DayReportType DTOToDAL(DayReportTypeDTO dr)
        {
            return new DayReportType
            {
                Id = dr.Id,
                Value = dr.value
            };
        }

        public static List<DayReportType> DTOsToDALs(List<DayReportTypeDTO> types)
        {
            List<DayReportType> DALList = new List<DayReportType>();
            types.ForEach(type => DALList.Add(DTOToDAL(type)));
            return DALList;
        }
    }
}
