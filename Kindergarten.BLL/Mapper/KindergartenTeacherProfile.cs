using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;

namespace Kindergarten.BLL.Mapper
{
    public class KindergartenTeacherProfile : Profile
    {
        public KindergartenTeacherProfile()
        {
            CreateMap<KindergartenTeacher, KindergartenTeacherDTO>().ReverseMap();
            CreateMap<KindergartenTeacherForCreationDTO, KindergartenTeacher>();
            CreateMap<KindergartenTeacherForUpdateDTO, KindergartenTeacher>();
        }
    }
}
