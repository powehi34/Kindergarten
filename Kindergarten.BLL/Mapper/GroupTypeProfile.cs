using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;

namespace Kindergarten.BLL.Mapper
{
    public class GroupTypeProfile : Profile
    {
        public GroupTypeProfile()
        {
            CreateMap<GroupType, GroupTypeDTO>().ReverseMap();
            CreateMap<GroupTypeForCreationDTO, GroupType>();
            CreateMap<GroupTypeForUpdateDTO, GroupType>();
        }
    }
}
