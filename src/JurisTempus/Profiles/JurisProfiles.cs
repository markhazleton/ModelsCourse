using AutoMapper;
using JurisTempus.Data.Entities;
using JurisTempus.ViewModels;

namespace JurisTempus.Profiles
{
  public class JurisProfiles : Profile
  {
    public JurisProfiles()
    {
      CreateMap<Client, ClientViewModel>()
        .ForMember(vm=>vm.ContactName,o=>o.MapFrom(s=>s.Contact))
        .ForMember(vm => vm.Address1, o => o.MapFrom(s => s.Address.Address1))
        .ForMember(vm => vm.Address2, o => o.MapFrom(s => s.Address.Address2))
        .ForMember(vm => vm.Address3, o => o.MapFrom(s => s.Address.Address3))
        .ForMember(vm => vm.CityTown, o => o.MapFrom(s => s.Address.CityTown))
        .ForMember(vm => vm.StateProvince, o => o.MapFrom(s => s.Address.StateProvince))
        .ForMember(vm => vm.PostalCode, o => o.MapFrom(s => s.Address.PostalCode))
        .ForMember(vm => vm.Country, o => o.MapFrom(s => s.Address.Country))
        .ReverseMap();

      CreateMap<Case, CaseViewModel>()
        .ReverseMap();

    }

  }
}
