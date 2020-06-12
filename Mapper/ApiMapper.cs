using AutoMapper;
using ST3.Dtos;
using ST3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST3.Mapper
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
        }
    }
}
