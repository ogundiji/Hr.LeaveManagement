﻿using AutoMapper;
using Hr.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace Hr.LeaveManagement.MVC
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVm>().ReverseMap();
        }
    }
}
