using AutoMapper;
using FirstDemo.Domain.Entities.Training;
using FirstDemo.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CourseUpdateModel, Course>()
                .ReverseMap();
        }
    }
}
