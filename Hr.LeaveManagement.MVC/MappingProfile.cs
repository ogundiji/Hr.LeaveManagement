using AutoMapper;
using Hr.LeaveManagement.MVC.Models;
using Hr.LeaveManagement.MVC.Services.Base;

namespace Hr.LeaveManagement.MVC
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVm>().ReverseMap();
            CreateMap<RegisterVm, RegistrationRequest>().ReverseMap();
            CreateMap<LeaveRequestDto, LeaveRequestVM>()
              .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
              .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
              .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
              .ReverseMap();
            CreateMap<LeaveRequestListDto, LeaveRequestVM>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVm>().ReverseMap();
            CreateMap<LeaveAllocationDto, LeaveAllocationVm>().ReverseMap();
            CreateMap<RegisterVm, RegistrationRequest>().ReverseMap();
            CreateMap<EmployeeVm, Employee>().ReverseMap();
        }
    }
}
