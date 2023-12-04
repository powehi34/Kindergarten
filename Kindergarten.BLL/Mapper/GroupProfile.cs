using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;

namespace Kindergarten.BLL.Mapper
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<GroupForCreationDTO, Group>();
            CreateMap<GroupForUpdateDTO, Group>();
        }
    }
}
