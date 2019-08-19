using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Models;
using Vidly.Dtos;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //From Web app to Browser
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();

            //From Browser to Web App
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c=> c.Id, opt=>opt.Ignore());            
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m=>m.Id, opt=>opt.Ignore());

        }
    }
}