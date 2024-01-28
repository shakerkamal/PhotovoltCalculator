using AutoMapper;
using PhotovoltCalculatorAPI.Entities;
using PhotovoltCalculatorAPI.Models.ProductModels;
using PhotovoltCalculatorAPI.Models.ProjectModels;
using PhotovoltCalculatorAPI.Models.UserModels;

namespace PhotovoltCalculatorAPI.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //system user
            CreateMap<SystemUser, UserDetails>();
            CreateMap<RegisterUser, SystemUser>();
            CreateMap<UpdateUser, SystemUser>();

            //projects
            CreateMap<Project, ProjectDetails>();
            CreateMap<CreateProject, Project>();
            CreateMap<UpdateProject, Project>();
            CreateMap<Project, ProjectIndex>();

            //products
            CreateMap<Product, ProductDetails>();
            CreateMap<Product, ProductIndex>();
            CreateMap<CreateProduct, Product>();
            CreateMap<UpdateProduct, Product>();
        }
    }
}
