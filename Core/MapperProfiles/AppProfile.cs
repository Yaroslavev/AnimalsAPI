using Core.Models;
using AutoMapper;
using Data.Enteties;

namespace Core.MapperProfiles
{
	public class AppProfile : Profile
	{
		public AppProfile() {
			CreateMap<AddAnimalModel, Animal>();
			CreateMap<Animal, AnimalModel>();
			CreateMap<Animal, EditAnimalModel>();
			CreateMap<EditAnimalModel, Animal>();
		}
	}
}
