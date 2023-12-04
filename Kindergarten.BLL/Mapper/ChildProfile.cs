using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;

namespace Kindergarten.BLL.Mapper
{
    public class ChildProfile : Profile
    {
        public ChildProfile()
        {
            CreateMap<Child, ChildDTO>().ReverseMap();
            CreateMap<ChildForCreationDTO, Child>();
            CreateMap<ChildForUpdateDTO, Child>();
        }
    }
}
